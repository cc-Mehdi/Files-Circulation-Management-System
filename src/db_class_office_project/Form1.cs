using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer;

namespace db_class_office_project
{
    public partial class Form1 : Form
    {
        int _idForEdit = 0; // for managing edit and add new item
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OpenLoginForm();
            UpdateDatetime();
            BindGrid();

            // load file status dropdown
            var context = new FileCirculationManagementSystem_DBEntities();
            ddlCurrentStatus.DataSource = context.FileStatus.Select(u => new { u.Id, u.Title }).ToList();
            ddlCurrentStatus.DisplayMember = "Title";
            ddlCurrentStatus.ValueMember = "Id";

            CheckAccesses(); // show sections base on the user role
        }

        private void OpenLoginForm()
        {
            this.Hide();
            if (new FrmLogin().ShowDialog() == DialogResult.OK)
            {
                this.Show();
                var context = new FileCirculationManagementSystem_DBEntities();
                var user = context.Users.FirstOrDefault(u => u.Id == Program.UserId);
                string userFullname = user?.Fullname ?? "ناشناس",
                       userRole = user?.Roles ?? "نامشخص";
                lblUserInformation.Text = $"کاربر : {userFullname} | سمت : {userRole}";

                CheckAccesses();
            }
            else
            {
                this.Close();
                return;
            }
        }

        private void CheckAccesses()
        {
            var context = new FileCirculationManagementSystem_DBEntities();
            // switch visibility for manager and normal user sections
            var currentUser = context.Users.FirstOrDefault(u => u.Id == Program.UserId);
            if (currentUser != null && currentUser.Roles.Contains("admin"))
                btnManageUsers.Visible = true;
            else
                btnManageUsers.Visible = false;
        }

        private void BindGrid()
        {
            var context = new FileCirculationManagementSystem_DBEntities();

            // load files in grid view
            var list = context.Files.Select(u => new Files_ViewModel()
            {
                Id = u.Id,
                FileStatus_Id = u.FileStatus_Id,
                FileStatus = u.FileStatus.Title,
                CaseId = u.CaseId,
                FullName = u.FullName,
                SubscriptionCode = u.SubscriptionCode,
                BtnDelete = "حذف",
                BtnEdit = "ویرایش"
            }).ToList();

            if (txtSearch.Text.Length > 0)
            {
                int.TryParse(txtSearch.Text, out int numSearch);
                list = list.Where(u => u.CaseId == numSearch || u.FullName.Contains(txtSearch.Text) || u.SubscriptionCode.Contains(txtSearch.Text)).ToList();
            }

            gvList.DataSource = list;

            // hide some columns
            gvList.Columns["Id"].Visible = false;
            gvList.Columns["FileStatus_Id"].Visible = false;

            // set alignment of cells in gridview to center
            foreach (DataGridViewColumn item in gvList.Columns)
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (isFormValid()) // check the inputs fill correctly
            {
                var context = new FileCirculationManagementSystem_DBEntities();
                var file = new DataLayer.Files();

                if (_idForEdit != 0) // if user try to edit item
                    file = context.Files.FirstOrDefault(u => u.Id == _idForEdit);

                file.CaseId = int.Parse(txtCaseID.Text);
                file.FullName = txtFullName.Text;
                file.SubscriptionCode = txtSubscriptionCode.Text;
                file.FileStatus_Id = int.Parse(ddlCurrentStatus.SelectedValue.ToString());

                if (_idForEdit == 0) // if user try to add new item
                    context.Files.Add(file);

                context.SaveChanges();

                // refresh form
                BindGrid();
                ResetForm();
            }
        }

        private void ResetForm()
        {
            txtCaseID.Text = txtFullName.Text = txtSubscriptionCode.Text = ddlCurrentStatus.Text = "";
        }

        private bool isFormValid()
        {
            if (!string.IsNullOrEmpty(txtCaseID.Text) || !string.IsNullOrEmpty(txtFullName.Text) || !string.IsNullOrEmpty(txtSubscriptionCode.Text) ||
                !string.IsNullOrEmpty(ddlCurrentStatus.Text))
            {
                int.TryParse(txtCaseID.Text, out int caseId);
                int.TryParse(txtSubscriptionCode.Text, out int subscriptionCode);
                if (caseId == 0 || subscriptionCode == 0)
                {
                    MessageBox.Show("شماره کلاسه یا شماره اشتراک باید عددی باشد");
                    return false;
                }

                if (txtSubscriptionCode.Text.Length != 6)
                {
                    MessageBox.Show("شماره اشتراک باید 6 رقمی باشد");
                    return false;
                }

                return true;
            }

            MessageBox.Show("لطفا در ورود اطلاعات دقت کنید");
            return false;
        }

        private void gvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                var col = (DataGridViewColumn)gvList.Columns[e.ColumnIndex];
                if (col.Name == "BtnDelete" || col.Name == "BtnEdit")
                    gvList.Columns[e.ColumnIndex].DefaultCellStyle.ForeColor = Color.Blue;
            }

        }

        private void gvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var col = (DataGridViewColumn)gvList.Columns[e.ColumnIndex];
            if (e.RowIndex > -1 && col.Name == "BtnDelete") // make sure to click on records not headers and delete column
            {
                var id = int.Parse(gvList.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                if (id != 0)
                {
                    var context = new FileCirculationManagementSystem_DBEntities();
                    var file = context.Files.FirstOrDefault(u => u.Id == id);
                    context.Files.Remove(file);
                    context.SaveChanges();
                    BindGrid();
                }
                else
                    MessageBox.Show("فایل انتخاب شده یافت نشد");
            }
            if (e.RowIndex > -1 && col.Name == "BtnEdit") // make sure to click on records not headers and edit column
            {
                _idForEdit = int.Parse(gvList.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                BindForEdit();
            }
        }

        private void BindForEdit()
        {
            var context = new FileCirculationManagementSystem_DBEntities();
            var file = context.Files.FirstOrDefault(u => u.Id == _idForEdit);

            if (file == null)
            {
                MessageBox.Show("فایل انتخاب شده یافت نشد");
                return;
            }

            txtCaseID.Text = file.CaseId.ToString();
            txtFullName.Text = file.FullName;
            txtSubscriptionCode.Text = file.SubscriptionCode;
            ddlCurrentStatus.Text = file.FileStatus.Title;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            OpenLoginForm();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateDatetime();
        }

        private void UpdateDatetime()
        {
            var dateTimeNow = DateTime.Now;
            var currentDate = $"{dateTimeNow.Year.ToString("0000")}/{dateTimeNow.Month.ToString("00")}/{dateTimeNow.Day.ToString("00")}";
            var currentTime = $"{dateTimeNow.Hour.ToString("00")}:{dateTimeNow.Minute.ToString("00")}";
            lblDatetime.Text = $"{currentDate} | {currentTime}";
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            new FrmManageUsers().ShowDialog();
        }
    }

    class Files_ViewModel
    {
        public int Id { get; set; }
        public Nullable<int> FileStatus_Id { get; set; }

        [DisplayName("شماره کلاسه")]
        public Nullable<int> CaseId { get; set; }

        [DisplayName("نام و نام خانوادگی")]
        public string FullName { get; set; }

        [DisplayName("شماره اشتراک")]
        public string SubscriptionCode { get; set; }

        [DisplayName("وضعیت فعلی پرونده")]
        public string FileStatus { get; set; }

        [DisplayName("حذف پرونده")]
        public string BtnDelete { get; set; }

        [DisplayName("ویرایش پرونده")]
        public string BtnEdit { get; set; }
    }
}

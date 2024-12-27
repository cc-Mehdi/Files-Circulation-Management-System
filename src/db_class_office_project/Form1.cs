using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            BindGrid();
        }

        private void BindGrid()
        {
            var context = new FileCirculationManagementSystem_DBEntities();
            
            // load files in grid view
            var list = context.Files.Select(u => new Files_ViewModel()
            {
                Id = u.Id,
                FileStatus_Id = u.FileStatus_Id,
                FileStatus = u.FileStatu.Title,
                CaseId = u.CaseId,
                FullName = u.FullName,
                SubscriptionCode = u.SubscriptionCode,
                BtnDelete = "حذف"
            }).ToList();

            gvList.DataSource = list;

            // hide some columns
            gvList.Columns["Id"].Visible = false;
            gvList.Columns["FileStatus_Id"].Visible = false;

            // set alignment of cells in gridview to center
            foreach (DataGridViewColumn item in gvList.Columns)
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            // load file status dropdown
            ddlCurrentStatus.DataSource = context.FileStatus.Select(u => new {  u.Id, u.Title }).ToList();
            ddlCurrentStatus.DisplayMember = "Title";
            ddlCurrentStatus.ValueMember = "Id";
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (isFormValid()) // check the inputs fill correctly
            {
                var context = new FileCirculationManagementSystem_DBEntities();
                var file = new DataLayer.File();

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

                if(txtSubscriptionCode.Text.Length != 6)
                {
                    MessageBox.Show("شماره اشتراک باید 6 رقمی باشد");
                    return false;
                }

                return true;
            }

            MessageBox.Show("لطفا در ورود اطلاعات دقت کنید");
            return false;
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
        }
    }
}

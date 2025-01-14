﻿using System;
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
    public partial class FrmManageUsers : Form
    {
        int _idForEdit = 0;
        public FrmManageUsers()
        {
            InitializeComponent();
        }

        private void btnResetForm_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            txtFullname.Text = txtEmail.Text = txtPhone.Text = txtUsername.Text = txtPassword.Text = ddlRoles.Text = "";
        }

        private void BindGrid()
        {
            var context = new FileCirculationManagementSystem_DBEntities();
            var list = context.Users.Select(u => new Users_ViewModel()
            {
                Id = u.Id,
                Fullname = u.Fullname,
                Username = u.Username,
                HashPassword = u.HashPassword,
                Phone = u.Phone,
                Email = u.Email,
                PictureAddress = u.PictureAddress,
                Roles = u.Roles,
                BtnDelete = "حذف",
                BtnEdit = "ویرایش"
            }).ToList();

            // search filter
            if (txtSearch.Text.Length > 0 && list.Count > 0)
            {
                string param = txtSearch.Text;
                list = list.ToList().Where(u =>
                       (u?.Fullname?.Contains(param) ?? false) ||
                       (u?.Username?.Contains(param) ?? false) ||
                       (u?.Phone?.Contains(param) ?? false) ||
                       (u?.Email?.Contains(param) ?? false) ||
                       (u?.Roles?.Contains(param) ?? false)
                   ).ToList();
            }

            gvList.DataSource = list;

            // hide some columns
            gvList.Columns["Id"].Visible = false;
            gvList.Columns["HashPassword"].Visible = false;
            gvList.Columns["PictureAddress"].Visible = false;


            // set alignment of cells in gridview to center
            foreach (DataGridViewColumn item in gvList.Columns)
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(isFormValid()) // check the inputs fill correctly
            {
                var context = new FileCirculationManagementSystem_DBEntities();
                var user = new DataLayer.Users();

                // create hash password
                var hashedPassword = Helper.Encryption.HashPassword(txtPassword.Text);

                if (_idForEdit != 0) // if user try to edit item
                {
                    user = context.Users.FirstOrDefault(u => u.Id == _idForEdit);
                    if (string.IsNullOrEmpty(txtPassword.Text)) // if user wasn't changed the password
                        hashedPassword = user.HashPassword;
                }


                user.Fullname = txtFullname.Text;
                user.Email = txtEmail.Text;
                user.Phone = txtPhone.Text;
                user.Username = txtUsername.Text;
                user.Roles = ddlRoles.Text;
                user.HashPassword = hashedPassword;

                if (_idForEdit == 0) // if user try to add new item
                    context.Users.Add(user);

                context.SaveChanges();

                // refresh form
                BindGrid();
                ResetForm();
            }
        }

        private bool isFormValid()
        {
            if (!string.IsNullOrEmpty(txtFullname.Text) || !string.IsNullOrEmpty(txtEmail.Text) || !string.IsNullOrEmpty(txtPhone.Text) ||
                !string.IsNullOrEmpty(txtUsername.Text) || (_idForEdit == 0 && !string.IsNullOrEmpty(txtPassword.Text)))
            {
                if (_idForEdit == 0 && txtPassword.Text.Length < 6)
                {
                    MessageBox.Show("برای امنیت بیشتر ، کلمه عبور باید حداقل 6 کاراکتر باشد");
                    return false;
                }

                return true;
            }

            MessageBox.Show("لطفا در ورود اطلاعات دقت کنید");
            return false;
        }

        private void FrmManageUsers_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindForEdit()
        {
            var context = new FileCirculationManagementSystem_DBEntities();
            var user = context.Users.FirstOrDefault(u => u.Id == _idForEdit);

            if (user == null)
            {
                MessageBox.Show("کاربر انتخاب شده یافت نشد");
                return;
            }

            txtFullname.Text = user.Fullname;
            txtEmail.Text = user.Email;
            txtPhone.Text = user.Phone;
            txtUsername.Text = user.Username;
            ddlRoles.Text = user.Roles;
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
                    var user = context.Users.FirstOrDefault(u => u.Id == id);
                    context.Users.Remove(user);
                    context.SaveChanges();
                    BindGrid();
                }
                else
                    MessageBox.Show("کاربر انتخاب شده یافت نشد");
            }
            if (e.RowIndex > -1 && col.Name == "BtnEdit") // make sure to click on records not headers and edit column
            {
                _idForEdit = int.Parse(gvList.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                BindForEdit();
            }
        }
    }

    class Users_ViewModel
    {
        public int Id { get; set; }

        [DisplayName("نام و نام خانوادگی")]
        public string Fullname { get; set; }

        [DisplayName("نام کاربری")]
        public string Username { get; set; }

        public string HashPassword { get; set; }

        [DisplayName("تلفن")]
        public string Phone { get; set; }

        [DisplayName("ایمیل")]
        public string Email { get; set; }
        public string PictureAddress { get; set; }

        [DisplayName("سمت")]
        public string Roles { get; set; }

        [DisplayName("حذف کاربر")]
        public string BtnDelete { get; set; }

        [DisplayName("ویرایش کاربر")]
        public string BtnEdit { get; set; }
    }
}
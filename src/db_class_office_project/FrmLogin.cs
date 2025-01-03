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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            UpdateDatetime();
            timer1.Start();
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(IsFormValid())
            {
                var context = new FileCirculationManagementSystem_DBEntities();
                var hashedPassword = Helper.Encryption.HashPassword(txtPassword.Text);
                var userId = context.Users.FirstOrDefault(u => u.Username == txtUsername.Text && u.HashPassword == hashedPassword)?.Id ?? 0;
                if (userId != 0)
                {
                    Program.UserId = userId;
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                    lblMessage.Text = "نام کاربری یا کلمه عبور اشتباه است";
            }
        }

        private bool IsFormValid()
        {
            if(string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                lblMessage.Text = "لطفا در ورود اطلاعات دقت کنید";
                return false;
            }

            return true;
        }
    }
}
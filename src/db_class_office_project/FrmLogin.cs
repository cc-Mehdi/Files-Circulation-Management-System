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
    public partial class FrmLogin : Form
    {
        public int _userId = 0; 
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var dateTimeNow = DateTime.Now;
            var currentDate = $"{dateTimeNow.Year}/{dateTimeNow.Month}/{dateTimeNow.Day}";
            var currentTime = $"{dateTimeNow.Hour}:{dateTimeNow.Minute}";
            lblDatetime.Text = $"{currentDate} | {currentTime}";
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(IsFormValid())
            {
                var context = new FileCirculationManagementSystem_DBEntities();
                //var hashedPassword = Helper.HashPassword(txtPassword.Text);
                //var userId = context.Users.FirstOrDefault(u => u.Username == txtUsername.Text && u.HashPassword == hashedPassword)?.Id ?? 0;
                //if (userId != 0)
                //{
                //    _userId = userId
                //    DialogResult = DialogResult.OK;
                //    this.Close();
                //}
            }
        }

        private bool IsFormValid()
        {
            if(string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("لطفا در ورود اطلاعات دقت کنید", "خطا در ورود", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
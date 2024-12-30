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
        int _idForEdit = 0; // for managing edit and add new item
        public FrmLogin()
        {
            InitializeComponent();
        }
}

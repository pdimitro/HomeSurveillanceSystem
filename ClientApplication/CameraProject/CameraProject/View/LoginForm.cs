using CameraProject.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraProject.View
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {   
            this.Dispose();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            LoginValidation lv = new LoginValidation(usernameBox1.Text, passwordBox.Text);
            if (lv.isUserInputValid()) 
            { 
                this.DialogResult = DialogResult.OK; this.Close(); 
            } 
            else 
            { 
                MessageBox.Show(lv.errText); 
            }
        }
    }
}

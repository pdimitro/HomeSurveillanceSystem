using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace CameraProject.View
{
    public partial class userInformationForm : Form
    {
        private string directory;
        public userInformationForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void setFormName( string formName )
        {
            this.Text = formName;
        }

        public void setLabelText( string labelText )
        {
            deleteVideoLabel.Text = labelText;
        }

        public void setErrorPictureBox( )
        {
            pictureBox1.Image = CameraProject.Properties.Resources.Error;
        }

        public void setFileDirectory(string folder)
        {
            this.directory = folder;
        }

        public void setLinkVisible()
        {
            this.linkLabel1.Visible = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = directory,
                FileName = "explorer.exe"
            };

            Process.Start(startInfo);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media; //  write down it at the top of the FORM

namespace CameraProject.View
{
    public partial class PopupNotification : Form
    {
        private static View.Waiting waitingInstance;
        public PopupNotification(string imageName)
        {

            InitializeComponent();
            var localFilePath = @"C:\Users\beroi\OneDrive\Documents\alert.jpg";
            //localFilePath = localFilePath + imageName;
            pictureBox1.Image = Image.FromFile(localFilePath);
            cameraSourceTextBox.Text = "Camera 01";
            timeTextBox.Text = DateTime.Now.ToString("HH:mm:ss tt");
            SoundPlayer my_wave_file = new SoundPlayer("SpeedLimitViolationAlert.wav");

            my_wave_file.PlaySync(); // PlaySync means that once sound start then no other activity if form will occur untill sound goes to finish
        }

        public PopupNotification()
        {
            InitializeComponent();
            cameraSourceTextBox.Text = "Camera 01";
            timeTextBox.Text = DateTime.Now.ToString("HH:mm:ss tt");
            SoundPlayer my_wave_file = new SoundPlayer("SpeedLimitViolationAlert.wav");
            my_wave_file.PlaySync(); // PlaySync means that once sound start then no other activity if form will occur untill sound goes to finish
        }

        private void viewButton_Click(object sender, EventArgs e)
        {
            waitingInstance = new Waiting();
        }

        private void PopupNotification_Load(object sender, EventArgs e)
        {

        }
    }
}

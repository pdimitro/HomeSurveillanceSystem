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
        public PopupNotification(string imageName)
        {

            InitializeComponent();
            var localFilePath = @"C:\Users\beroi\OneDrive\Documents\alert.jpg";
            //localFilePath = localFilePath + imageName;
            pictureBox1.Image = Image.FromFile(localFilePath);
            SoundPlayer my_wave_file = new SoundPlayer("SpeedLimitViolationAlert.wav");

            my_wave_file.PlaySync(); // PlaySync means that once sound start then no other activity if form will occur untill sound goes to finish
        }

        public PopupNotification()
        {
            InitializeComponent();
            SoundPlayer my_wave_file = new SoundPlayer("SpeedLimitViolationAlert.wav");
            my_wave_file.PlaySync(); // PlaySync means that once sound start then no other activity if form will occur untill sound goes to finish
        }
    }
}

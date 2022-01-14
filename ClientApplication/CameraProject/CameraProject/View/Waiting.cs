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
    public partial class Waiting : Form
    {

        private static View.PreviewVideos prevVidInstance;
        public Waiting()
        {
            InitializeComponent();
            this.Show();
            prevVidInstance = new PreviewVideos();
            this.Close();
            prevVidInstance.ShowDialog();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

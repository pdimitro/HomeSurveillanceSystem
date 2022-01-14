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

        public Waiting( bool bEditing )
        {
            InitializeComponent();
            this.Show();
            Cursor.Current = Cursors.WaitCursor;
            prevVidInstance = new PreviewVideos(bEditing);
            this.Close();
            prevVidInstance.ShowDialog();

        }

        public Waiting()
        {
            InitializeComponent();
            this.Show();
            Cursor.Current = Cursors.WaitCursor;
            prevVidInstance = new PreviewVideos();
            this.Close();
            prevVidInstance.ShowDialog();

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using CefSharp;
using CefSharp.WinForms;  

namespace CameraProject.View
{
    public partial class MainMenu : Form
    {
        private static Logic.CameraImageWorker cameraWorkerInstance;
        private static View.PreviewVideos prevVidInstance;
        private static View.Waiting waitingInstance;
        private static Logic.LiveViewer liveViewerInsance;

        public ChromiumWebBrowser browser;
 
        public void InitBrowser()
        {

            //ChromiumWebBrowser browser = new ChromiumWebBrowser("http://google.com/")
            ChromiumWebBrowser browser = new ChromiumWebBrowser("http://192.168.100.5:5000/")
            {
                Location = new Point(200, 200),
                Size = new Size(320, 200),
                Dock = DockStyle.Fill,
            };

            toolStripContainer1.ContentPanel.Controls.Add(browser);
            //.Controls.Add(browser);
            browser.IsBrowserInitializedChanged += ChromeBrowserIsBrowserInitializedChanged;
            //this.Controls.Add(browser);
            //            
        }

        void ChromeBrowserIsBrowserInitializedChanged(object sender, IsBrowserInitializedChangedEventArgs e)
        {
            //browser.ShowDevTools();
        }
        public MainMenu()
        {
            Cef.Initialize(new CefSettings() { CachePath = "Cache" });
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            InitializeComponent();
            textBox1.Text = DateTime.Now.ToString("HH:mm:ss tt");
            cameraWorkerInstance = new Logic.CameraImageWorker(this);
        }

        public void setSystemTime( string time )
        {
            textBox1.Text = time;
        }

        private void cameraActiveCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            //liveViewerInsance = new Logic.LiveViewer(this);

            //liveViewerInsance.livescreen_Clicked();
            if( cameraActiveCheckbox.Checked )
            {
                if (0 == comboBox1.SelectedIndex )
                {
                    InitBrowser();
                }
                else
                {
                    var localFilePath = @"C:\Users\beroi\OneDrive\Documents\Visual Studio 2013\Projects\CameraProject\CameraProject\Resources\cameraNotsupp.jpg";
                    //Add here the logic for the other cameras..
                    toolStripContainer1.ContentPanel.BackgroundImage = Image.FromFile(localFilePath);
                }                     
            }
            else
            {
                if( toolStripContainer1.ContentPanel.Controls.Count > 0 )
                {
                    toolStripContainer1.ContentPanel.Controls.RemoveAt(0);
                    //browser.Dispose();
                }
            }
        }

        private void viewHistoryButton_Click(object sender, EventArgs e)
        {
            waitingInstance = new Waiting();
            //waitingInstance.ShowDialog();
            //prevVidInstance = new PreviewVideos();
            //waitingInstance.Close();
            //prevVidInstance.ShowDialog();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            View.PopupNotification var = new View.PopupNotification("test");
            var.ShowDialog();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void webControl1_Click(object sender, EventArgs e)
        {
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using Dropbox.Api;
using Dropbox.Api.Common;
using Dropbox.Api.Files;
using Dropbox.Api.Team;

namespace CameraProject.View
{
    public partial class PreviewVideos : Form
    {
        static private Logic.DropBoxAPIConnector dropBoxInstance;

        public PreviewVideos( bool bEditing )
        {
            ListFolderResult privateList;

            InitializeComponent();
            dropBoxInstance = new Logic.DropBoxAPIConnector();

            if(bEditing != false )
            {
                deleteButton.Enabled = true;
                DownloadButton.Enabled = true;
            }

            var task = Task.Run((Func<Task<int>>)dropBoxInstance.Run);
            //var task2 = Task.Run((Func<Task<ListFolderResult>>)dropBoxInstance.ListCurrentVideoFiles);

            task.Wait(5000);

            privateList = dropBoxInstance.getListOfVideos();

            foreach (var item in privateList.Entries.Where(i => i.IsFile))
            {
                var file = item.AsFile;

                checkedVideos.Items.Add(item.Name);

                 Console.WriteLine("F{0,8} {1}",
                    file.Size,
                    item.Name);
            }

            axWindowsMediaPlayer1.URL = @"C:\Users\beroi\OneDrive\Documents\Tuesday 17 July 2018 04_28_29PM (1).avi";
            Load_All_Videos(privateList);
        }

        public PreviewVideos()
        {
            ListFolderResult privateList;

            InitializeComponent();
            dropBoxInstance = new Logic.DropBoxAPIConnector();

            var task = Task.Run((Func<Task<int>>)dropBoxInstance.Run);
            //var task2 = Task.Run((Func<Task<ListFolderResult>>)dropBoxInstance.ListCurrentVideoFiles);

            task.Wait(5000);

            privateList = dropBoxInstance.getListOfVideos();

            foreach (var item in privateList.Entries.Where(i => i.IsFile))
            {
                var file = item.AsFile;

                checkedVideos.Items.Add(item.Name);

                Console.WriteLine("F{0,8} {1}",
                   file.Size,
                   item.Name);
            }

            axWindowsMediaPlayer1.URL = @"C:\Users\beroi\OneDrive\Documents\Tuesday 17 July 2018 04_28_29PM (1).avi";
            Load_All_Videos(privateList);
        }

        private void Load_All_Videos( ListFolderResult privateList )
        {
            var localFilePath = @"C:\Users\beroi\OneDrive\Documents\";
            foreach (var item in privateList.Entries.Where(i => i.IsFile))
            {
                var result = Task.Run(async () => { return await dropBoxInstance.loadSelectedVideo(item.Name,null); }).Result;
            }
        }

        private void checkedVideos_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var checkedListBox = (CheckedListBox)sender;
            var checkedItemText = checkedListBox.Items[e.Index].ToString();
            var localFilePath = @"C:\Users\beroi\OneDrive\Documents\";

            Console.WriteLine(checkedItemText);


            for (int ix = 0; ix < checkedListBox.Items.Count; ++ix)
                if (ix != e.Index) checkedListBox.SetItemChecked(ix, false);

            Console.WriteLine(checkedItemText);

            //var result = Task.Run(async () => { return await dropBoxInstance.loadSelectedVideo(checkedItemText); }).Result;*/

            localFilePath = localFilePath + checkedItemText;

            axWindowsMediaPlayer1.URL = localFilePath;
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            ListFolderResult privateListL;
            int count = 0;
            int currentCount = checkedVideos.Items.Count;

            var task = Task.Run((Func<Task<int>>)dropBoxInstance.Run);
            //var task2 = Task.Run((Func<Task<ListFolderResult>>)dropBoxInstance.ListCurrentVideoFiles);

            task.Wait(5000);

            privateListL = dropBoxInstance.getListOfVideos();
            foreach (var item in privateListL.Entries.Where(i => i.IsFile))
            {
                //var file = item.AsFile;

                //checkedVideos.Items.Add(item.Name);
                count++;
            }

            if( count != currentCount )
            {
                if (count > currentCount)
                {
                    foreach (var item in privateListL.Entries.Where(i => i.IsFile))
                    {
                        var file = item.AsFile;

                        if (!isItemInTheMenu(item.Name))
                        {
                            checkedVideos.Items.Add(item.Name);
                        }
                    }
                }
                else
                {
                    checkedVideos.Items.Clear();

                    foreach (var item in privateListL.Entries.Where(i => i.IsFile))
                    {
                        var file = item.AsFile;

                        checkedVideos.Items.Add(item.Name);
                        
                    }
                }
            
                
            }

        }
        private bool isItemInTheMenu( string nameOfItem )
        {
            return checkedVideos.Items.Contains(nameOfItem);     
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            string fileSelected;

            CheckedListBox.CheckedItemCollection checkedListBox = checkedVideos.CheckedItems;
           
            for (int ix = 0; ix < checkedListBox.Count; ++ix)
            {
                fileSelected = checkedListBox[ix].ToString();
                try
                {
                    // your code 
                    var result = Task.Run(async () => { return await dropBoxInstance.deleteSelectedVideo(fileSelected); }).Result;
                    var okInfo = new userInformationForm();
                    okInfo.ShowDialog();
                    refreshButton_Click(sender, e);
                }
                catch (AggregateException except)
                {
                    
                }                
            }
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            string directory = null;
            var okInfo = new userInformationForm();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                directory = folderBrowserDialog1.SelectedPath;
            }
            if (String.IsNullOrEmpty(directory))
            {
                okInfo.setErrorPictureBox();
                okInfo.setFormName("Error loading file");
                okInfo.setLabelText("Wrong directory! Please choose \n valid one!");
                okInfo.ShowDialog();
            }
            else
            {
                string fileSelected;

                CheckedListBox.CheckedItemCollection checkedListBox = checkedVideos.CheckedItems;

                if (checkedListBox.Count > 0)
                {
                    for (int ix = 0; ix < checkedListBox.Count; ++ix)
                    {
                        fileSelected = checkedListBox[ix].ToString();
                        try
                        {
                            // your code 
                            var result = Task.Run(async () => { return await dropBoxInstance.loadSelectedVideo(fileSelected, directory); }).Result;

                            okInfo.setFormName("Loading file operation");
                            okInfo.setLabelText("File " + fileSelected + "\n successfully loaded to\n" + directory);
                            okInfo.setFileDirectory(directory);
                            okInfo.setLinkVisible();
                            okInfo.ShowDialog();
                            refreshButton_Click(sender, e);
                        }
                        catch (AggregateException except)
                        {

                        }
                    }
                }
                else
                {
                    okInfo.setErrorPictureBox();
                    okInfo.setFormName("Error loading file");
                    okInfo.setLabelText("Please select file to download!\n");
                    okInfo.ShowDialog();
                }
            }
        }
    }
}

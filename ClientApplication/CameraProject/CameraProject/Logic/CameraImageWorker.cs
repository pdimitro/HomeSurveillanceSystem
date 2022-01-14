using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;

using Dropbox.Api.Common;
using Dropbox.Api.Files;
using Dropbox.Api.Team;
using Dropbox.Api;

namespace CameraProject.Logic
{
    class CameraImageWorker
    {
        //Worker logic..
        private string currentTime;

        private BackgroundWorker myWorker = new BackgroundWorker();
        private View.MainMenu mainForm;

        static private Logic.DropBoxAPIConnector dropboxConnection;

        private static int holder = 0;

        public CameraImageWorker( View.MainMenu formInstance )
        {
            initizalize_BodyWorker();
            mainForm = formInstance;
        }

        private void initizalize_BodyWorker()
        {
            //called when the body worker should be initialized
            myWorker.DoWork += new DoWorkEventHandler(myWorker_DoWork);
            myWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(myWorker_RunWorkerCompleted);
            myWorker.ProgressChanged += new ProgressChangedEventHandler(myWorker_ProgressChanged);
            myWorker.WorkerReportsProgress = true;
            myWorker.WorkerSupportsCancellation = true;
            myWorker.RunWorkerAsync();
            
        }

        protected void myWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker sendingWorker =
            (BackgroundWorker)sender;//Capture the BackgroundWorker that fired the event
            object[] arrObjects =
            (object[])e.Argument;//Collect the array of objects the we received from the main thread
            //int maxValue = (int)arrObjects[0];//Get the numeric value 
            //from inside the objects array, don't forget to cast
            StringBuilder sb = new StringBuilder();//Declare a new string builder to store the result.
            ListFolderResult privateList;
            int initialCount = 0;
            //Create a new DropBoxInstance

            dropboxConnection = new DropBoxAPIConnector();

            var task = Task.Run((Func<Task<int>>)dropboxConnection.Run);

            task.Wait(5000);

            privateList = dropboxConnection.getListOfVideos();

            foreach (var item in privateList.Entries.Where(i => i.IsFile))
            {
                initialCount++;
            }
            
            //int i = 0;
            while (true)
            {
                if (false != sendingWorker.CancellationPending)
                {
                    e.Cancel = true;//If a cancellation request is pending, assign this flag a value of true
                    break;// If a cancellation request is pending, break to exit the loop
                }
                else
                {
                    if( (holder % 7) == 0 )
                    {
                        ListFolderResult NewList;
                        int newCount = 0;

                        var task2 = Task.Run((Func<Task<int>>)dropboxConnection.Run);

                        task2.Wait(3000);

                        
                        NewList = dropboxConnection.getListOfVideos();
                        string name = "";
                        foreach (var item in NewList.Entries.Where(i => i.IsFile))
                        {
                            //var file = item.AsFile;

                            //checkedVideos.Items.Add(item.Name);
                            newCount++;
                            name = item.Name;
                        }
                        if (newCount > initialCount)
                        {
                            //call the popup..
                            initialCount = newCount;
                            var localFilePath = @"C:\Users\beroi\OneDrive\Documents\";
                            var result = Task.Run(async () => { return await dropboxConnection.loadSelectedVideo(name,null); }).Result;
                            //always the last file in the list..
                            //TODO: call this to show the video...
                            //View.PopupNotification PopupNotif = new View.PopupNotification();
                            View.PopupNotification PopupNotif = new View.PopupNotification(name);
                            PopupNotif.ShowDialog();
                        }
                    }

                    currentTime = DateTime.Now.ToString("HH:mm:ss tt");
                    sendingWorker.ReportProgress(0);
                    System.Threading.Thread.Sleep(1000);

                }
            }
            //while(true)
           // {
                
            //}
            
        }
        protected void myWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled &&
                e.Error == null)//Check if the worker has been canceled or if an error occurred
            {
                string result = (string)e.Result;//Get the result from the background thread
                //textBox1.Text = result;//Display the result to the user
                //lblStaus.Text = "Done";
            }
            else if (e.Cancelled)
            {
                //lblStaus.Text = "User Canceled";
            }
            else
            {
                //lblStaus.Text = "An error has occurred";
            }
            //button1.Enabled = true;//Re enable the start button
        }

        protected void myWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            mainForm.setSystemTime(currentTime);            
        }

        public void Handle_SwithOnOffCameraEvent(bool bClicked)
        {
            int numericValue = 0;
            if (bClicked)
            {
                myWorker.CancelAsync();//Issue a cancellation request to stop the background worker
            }
            else
            {
                if (!myWorker.IsBusy)//Check if the worker is already in progress
                {
                    object[] arrObjects = new object[] { numericValue };//Declare the array of objects
                    myWorker.RunWorkerAsync(arrObjects);//Call the background worker
                }
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;

using Dropbox.Api;
using Dropbox.Api.Common;
using Dropbox.Api.Files;
using Dropbox.Api.Team;


namespace CameraProject.Logic
{
    class DropBoxAPIConnector
    {
                //Public entries
        static bool pictureSwitch = false;

        private const string ApiKey = "r6m0dyw890jttyt";

        private const string LoopbackHost = "http://127.0.0.1:3428/";

        private readonly Uri RedirectUri = new Uri(LoopbackHost + "authorize");

        private readonly Uri JSRedirectUri = new Uri(LoopbackHost + "token");

        private DropboxClient dropBoxClient;

        private ListFolderResult listVideos;

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public void setPictureSwitch( bool value )
        {
            pictureSwitch = value;
        }

        public DropBoxAPIConnector()
        {
            //var task = Task.Run((Func<Task<int>>)Run);

            //task.Wait(5000);
        }

        public ListFolderResult getListOfVideos()
        {
            return listVideos;
        }

        public async Task<int> Run()
        {
            DropboxCertHelper.InitializeCertPinning();

            var accessToken = "HxW6Rk1fjS8AAAAAAAAAiH1xsGsQmLJeJunx2UGYl7TX9ScvRB0RvPMETE-9_CPs";
            if (string.IsNullOrEmpty(accessToken))
            {
                return 1;
            }
            // Specify socket level timeout which decides maximum waiting time when no bytes are
            // received by the socket.
            var httpClient = new HttpClient(new WebRequestHandler { ReadWriteTimeout = 1000 * 1000 })
            {
                // Specify request level timeout which decides maximum time that can be spent on
                // download/upload files.
                Timeout = TimeSpan.FromMinutes(200)
            };

            try
            {
                var config = new DropboxClientConfig("SimpleTestApp")
                {
                    HttpClient = httpClient
                };

                var client = new DropboxClient(accessToken, config);
                dropBoxClient = client;
                //uncomment..
                //await RunUserTests(client);
                await ListCurrentVideoFiles();

                // Tests below are for Dropbox Business endpoints. To run these tests, make sure the ApiKey is for
                // a Dropbox Business app and you have an admin account to log in.

                /*
                var client = new DropboxTeamClient(accessToken, userAgent: "SimpleTeamTestApp", httpClient: httpClient);
                await RunTeamTests(client);
                */
            }
            catch (HttpException e)
            {
                Console.WriteLine("Exception reported from RPC layer");
                Console.WriteLine("    Status code: {0}", e.StatusCode);
                Console.WriteLine("    Message    : {0}", e.Message);
                if (e.RequestUri != null)
                {
                    Console.WriteLine("    Request uri: {0}", e.RequestUri);
                }
            }

            return 0;
        }

        private async Task RunUserTests(DropboxClient client)
        {
            await GetCurrentAccount(client);

            var path = "/DotNetApi";
            // var folder = await CreateFolder(client, path);
            var list = await ListFolder(client, path);

            var firstFile = list.Entries.FirstOrDefault(i => i.IsFile);
            if (firstFile != null)
            {
                //await Download(client, path, firstFile.AsFile);
                Console.WriteLine("File name: ", firstFile.Name);
            }



            //var pathInTeamSpace = "/Test";
            //await ListFolderInTeamSpace(client, pathInTeamSpace);

            //await Upload(client, path, "Test.txt", "This is a text file");

            //await ChunkUpload(client, path, "Binary");
        }

        public async Task<ListFolderResult> ListCurrentVideoFiles( )
        {
            await GetCurrentAccount(dropBoxClient);

            var path = "/home/Apps";
            // var folder = await CreateFolder(client, path);
            var list = await dropBoxClient.Files.ListFolderAsync(path);

            listVideos = list;

            return list;
        } 

        public async Task<bool> loadSelectedVideo( string fileNameP, string loadpath  )
        {
            var path = "/home/Apps";
            //var localFilePath = @"C:\Users\beroi\OneDrive\Documents\";
            var localFilePath = @"..\temp\";
            
            if ( !String.IsNullOrEmpty(loadpath) )
            {
                localFilePath = loadpath;
                localFilePath = localFilePath + @"\";
            }            

            localFilePath = localFilePath + fileNameP;
            path = path + "/" + fileNameP;

            Console.WriteLine("Download file...: ", path);

            using (var response = await dropBoxClient.Files.DownloadAsync(path))
            {
                Console.WriteLine("Response received file...");
                using (var fileStream = File.Create(localFilePath))
                {
                    (await response.GetContentAsStreamAsync()).CopyTo(fileStream);
                }
            }

            return true;
        }

        public async Task<bool> deleteSelectedVideo(string fileNameP)
        {
            var path = "/home/Apps";
            path = path + "/" + fileNameP;

            Console.WriteLine("Deleting file file...: ", path);

            DeleteResult response = await dropBoxClient.Files.DeleteV2Async(path);

            return true;
        }

        private async Task<ListFolderResult> ListFolder(DropboxClient client, string path)
        {
            Console.WriteLine("--- Files ---");
            var list = await client.Files.ListFolderAsync(path);

            // show folders then files
            foreach (var item in list.Entries.Where(i => i.IsFolder))
            {
                Console.WriteLine("D  {0}/", item.Name);
            }

            foreach (var item in list.Entries.Where(i => i.IsFile))
            {
                var file = item.AsFile;

                Console.WriteLine("F{0,8} {1}",
                    file.Size,
                    item.Name);


                //Save
                //var localFilePath = "D:/hikovi.jpg";
                if (false != pictureSwitch)
                {
                    var localFilePath = @"C:\Users\beroi\OneDrive\Documents\hikovi_0.jpg";
                    var fileName = "/DotNetApi/hikovi_0.jpg";
                    Console.WriteLine("Download file...: ", item.Name);

                    using (var response = await client.Files.DownloadAsync(fileName))
                    {
                        Console.WriteLine("Response received file...");
                        using (var fileStream = File.Create(localFilePath))
                        {
                            (await response.GetContentAsStreamAsync()).CopyTo(fileStream);
                        }
                    }
                }
                else
                {
                    var localFilePath = @"C:\Users\beroi\OneDrive\Documents\hikovi_1.jpg";
                    var fileName = "/DotNetApi/hikovi_1.jpg";
                    Console.WriteLine("Download file...: ", item.Name);

                    using (var response = await client.Files.DownloadAsync(fileName))
                    {
                        Console.WriteLine("Response received file...");
                        using (var fileStream = File.Create(localFilePath))
                        {
                            (await response.GetContentAsStreamAsync()).CopyTo(fileStream);
                        }
                    }
                }

                //Application.Run(new View.MainMenu());
                //var view = new View.MainMenu();
                //view.initializePicture("");
                //Application.Run(view);
            }

            if (list.HasMore)
            {
                Console.WriteLine("   ...");
            }
            return list;
        }

        private async Task GetCurrentAccount(DropboxClient client)
        {
            var full = await client.Users.GetCurrentAccountAsync();

            Console.WriteLine("Account id    : {0}", full.AccountId);
            Console.WriteLine("Country       : {0}", full.Country);
            Console.WriteLine("Email         : {0}", full.Email);
            Console.WriteLine("Is paired     : {0}", full.IsPaired ? "Yes" : "No");
            Console.WriteLine("Locale        : {0}", full.Locale);
            Console.WriteLine("Name");
            Console.WriteLine("  Display  : {0}", full.Name.DisplayName);
            Console.WriteLine("  Familiar : {0}", full.Name.FamiliarName);
            Console.WriteLine("  Given    : {0}", full.Name.GivenName);
            Console.WriteLine("  Surname  : {0}", full.Name.Surname);
            Console.WriteLine("Referral link : {0}", full.ReferralLink);

            if (full.Team != null)
            {
                Console.WriteLine("Team");
                Console.WriteLine("  Id   : {0}", full.Team.Id);
                Console.WriteLine("  Name : {0}", full.Team.Name);
            }
            else
            {
                Console.WriteLine("Team - None");
            }
        }




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace CameraProject.Logic
{
    class LiveViewer
    {

        static private View.MainMenu mainMenuInstance;
        Socket hostSocket;
        Thread thread;
        IPEndPoint saveEndPoint;
        string localIP = string.Empty;
        string computrHostName = string.Empty;
        // Size of receive buffer.  
        public const int BufferSize = 1024;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        Process p;
        public LiveViewer(View.MainMenu instance)
        {
            client_Init();
            mainMenuInstance = instance;
        }

        private bool client_Init()
        {
            computrHostName = Dns.GetHostName();
            IPHostEntry hostname = Dns.GetHostEntry(Dns.GetHostName());
            /*foreach (IPAddress ip in hostname.AddressList)
            {
               if (ip.AddressFamily.ToString() == "InterNetwork")
               {
                   localIP = ip.ToString();
               }
            }*/
            localIP = "192.168.0.2";
            //this.Text = this.Text + " | " + localIP;
            return true;
        }

        public void livescreen_Clicked()
        {
            connectSocket();
            //thread = new Thread(run_cmd);
            //thread.Start();
            //run_cmd();
        }

        public void livescreen_Stopped()
        {
            thread.Abort();
        }

        private void run_cmd()
        {

            string fileName = @"C:\client2.py";

            p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\Users\beroi\AppData\Local\Programs\Python\Python37-32\python.exe", fileName)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            Console.WriteLine(output);

            Console.ReadLine();

        }
        
        private void connectSocket()
        {
            Socket receiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint hostIpEndPoint = new IPEndPoint(IPAddress.Parse(localIP), 9999);
            //Connection node
            try
            {
                receiveSocket.Bind(hostIpEndPoint);
            }
            catch (SocketException e) { Console.WriteLine(e.ErrorCode); }
            
            //receiveSocket.Listen(10);
            //MessageBox.Show("start");
            hostSocket = receiveSocket.Accept();

            IPEndPoint ipEndpoint = new IPEndPoint(IPAddress.Parse(localIP), 9999);

            Socket client = new Socket(IPAddress.Parse(localIP).AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            saveEndPoint = ipEndpoint;

            try
            {

                client.Connect(ipEndpoint);
                Console.WriteLine("Socket created to {0}", client.RemoteEndPoint.ToString());

                thread = new Thread(new ThreadStart(trreadimage));

                 thread.IsBackground = true;
                 thread.Start();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


            /*thread = new Thread(new ThreadStart(trreadimage));

            thread.IsBackground = true;
            thread.Start();*/
        }


        private void trreadimage()
        {
            int dataSize;
            string imageName = "Image-" + System.DateTime.Now.Ticks + ".JPG";
            try
            {

                //dataSize = 0;
                byte[] b = new byte[4096];  //Picture of great
                //EndPoint senderRemote = (EndPoint)saveEndPoint;
                //dataSize = hostSocket.ReceiveFrom(b, ref senderRemote);

                Console.WriteLine("Progress changed");
                dataSize = hostSocket.Receive(b);
                /*b = hostSocket.recv(4096);

                //mainMenuInstance.setTextBoxText(dataSize);
                //if (dataSize > 0)
                {
                    MemoryStream ms = new MemoryStream(b);
                    Image img = Image.FromStream(ms);
                    img.Save(imageName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    mainMenuInstance.setPictureImage(img);
                    //videoBox.Image = img;
                    ms.Close();
                }*/
                if( dataSize > 0 )
                {
                    Console.WriteLine("DataReceived");
                }

            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.Message);
                //thread.Abort();
            }
            System.Threading.Thread.Sleep(500);
            trreadimage();
        }   
    }

}

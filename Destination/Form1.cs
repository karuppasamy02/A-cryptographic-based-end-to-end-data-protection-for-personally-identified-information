using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using FTServerCode;



namespace FTServerCode1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FTServerCode1.receivedPath = "";
            FTServerCode2.receivedPath = "";
            FTServerCode3.receivedPath = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (FTServerCode1.receivedPath.Length > 0)
            //    backgroundWorker1.RunWorkerAsync();
            //else
            //    MessageBox.Show("Please select file receiving path");

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label5.Text = FTServerCode1.receivedPath;
            label7.Text = FTServerCode2.receivedPath;
            label8.Text = FTServerCode3.receivedPath;
            label3.Text = FTServerCode1.curMsg;
            label4.Text = FTServerCode2.curMsg;
            label10.Text = FTServerCode3.curMsg;
        }


        FTServerCode1 obj1 = new FTServerCode1();
        FTServerCode2 obj2 = new FTServerCode2();
        FTServerCode3 obj3 = new FTServerCode3();
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            obj1.StartServer1();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog fd = new FolderBrowserDialog();
            //if (fd.ShowDialog() == DialogResult.OK)
            //{
            //    FTServerCode1.receivedPath = fd.SelectedPath;
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {




            FTServerCode1.receivedPath = @"C:\OUT_Cloud\Server_Cloud\micro1";
            FTServerCode2.receivedPath = @"C:\OUT_Cloud\Server_Cloud\micro2";
            FTServerCode3.receivedPath = @"C:\OUT_Cloud\Server_Cloud\micro3";
            backgroundWorker1.RunWorkerAsync();
            backgroundWorker2.RunWorkerAsync();
            backgroundWorker3.RunWorkerAsync();
            //doctorlogin doclog = new doctorlogin();
            //doclog.Show();
        }

 

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            obj2.StartServer2();
          
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            obj3.StartServer3();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            doctorlogin agg1 = new doctorlogin();
            agg1._pathname1 =label5.Text;
            agg1.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            doctorlogin agg2 = new doctorlogin();
            agg2._pathname2 = label7.Text;
            agg2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            doctorlogin agg3 = new doctorlogin();
            agg3._pathname3 = label8.Text;
            agg3.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        
    }
    //FILE TRANSFER 1 USING C#.NET SOCKET - SERVER
    class FTServerCode1
    {
        IPEndPoint ipEnd1;
        Socket sock1;
        public FTServerCode1()
        {

           ipEnd1 = new IPEndPoint(IPAddress.Any, 5656);
           sock1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
           sock1.Bind(ipEnd1);
        }
        public static string receivedPath;
        public static string curMsg = "Stopped";
        public  void StartServer1()
        {
            try
            {
                curMsg = "Starting...";
                sock1.Listen(100);

                curMsg = "Running and waiting to receive file.";
                Socket clientSock = sock1.Accept();

                byte[] clientData = new byte[1024 * 5000];
                
                int receivedBytesLen = clientSock.Receive(clientData);
                curMsg = "Receiving data...";




                int fileNameLen = BitConverter.ToInt32(clientData, 0);
                string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);

                BinaryWriter bWrite = new BinaryWriter(File.Open(receivedPath +"/"+ fileName, FileMode.Append)); ;
                bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);

                curMsg = "Saving file...";

                bWrite.Close();
                clientSock.Close();
                curMsg = "Reeived & Saved file; Server Stopped.";
            }
            catch (Exception ex)
            {
                curMsg = "File Receving error.";
            }
        }
    }




    //FILE TRANSFER 2 USING C#.NET SOCKET - SERVER
    class FTServerCode2
    {
        IPEndPoint ipEnd2;
        Socket sock2;
        public FTServerCode2()
        {
            ipEnd2 = new IPEndPoint(IPAddress.Any, 5657);
            sock2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            sock2.Bind(ipEnd2);
        }
        public static string receivedPath;
        public static string curMsg = "Stopped";
        public void StartServer2()
        {
            try
            {
                curMsg = "Starting...";
                sock2.Listen(100);

                curMsg = "Running and waiting to receive file.";
                Socket clientSock = sock2.Accept();

                byte[] clientData = new byte[1024 * 5000];

                int receivedBytesLen = clientSock.Receive(clientData);
                curMsg = "Receiving data...";

                int fileNameLen = BitConverter.ToInt32(clientData, 0);
                string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);

                BinaryWriter bWrite = new BinaryWriter(File.Open(receivedPath + "/" + fileName, FileMode.Append)); ;
                bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);

                curMsg = "Saving file...";

                bWrite.Close();
                clientSock.Close();
                curMsg = "Reeived & Saved file; Server Stopped.";
            }
            catch (Exception ex)
            {
                curMsg = "File Receving error.";
            }
        }
    }
    //FILE TRANSFER 3 USING C#.NET SOCKET - SERVER
    class FTServerCode3
    {
        IPEndPoint ipEnd3;
        Socket sock3;
        public FTServerCode3()
        {
            ipEnd3 = new IPEndPoint(IPAddress.Any, 5658);
            sock3 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            sock3.Bind(ipEnd3);
        }
        public static string receivedPath;
        public static string curMsg = "Stopped";
        public void StartServer3()
        {
            try
            {
                curMsg = "Starting...";
                sock3.Listen(100);

                curMsg = "Running and waiting to receive file.";
                Socket clientSock = sock3.Accept();

                byte[] clientData = new byte[1024 * 5000];

                int receivedBytesLen = clientSock.Receive(clientData);
                curMsg = "Receiving data...";

                int fileNameLen = BitConverter.ToInt32(clientData, 0);
                string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);

                BinaryWriter bWrite = new BinaryWriter(File.Open(receivedPath + "/" + fileName, FileMode.Append)); ;
                bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);

                curMsg = "Saving file...";

                bWrite.Close();
                clientSock.Close();
                curMsg = "Reeived & Saved file; Server Stopped.";
            }
            catch (Exception ex)
            {
                curMsg = "File Receving error.";
            }
        }
    }
}
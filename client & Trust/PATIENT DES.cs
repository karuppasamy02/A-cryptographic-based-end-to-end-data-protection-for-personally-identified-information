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
using FTClientCode;
using CryptoRC4;
using Ionic.Zip;
using System.Data.SqlClient;


namespace FTClientCode1
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=BLACKGOD\\SQLEXPRESS;Initial Catalog=cvhealthcare;Persist Security Info=True;User ID=sa;Password=admin123");








        string subfolder;
        public string _valuename;
        public string _valuefilename;
        public string _valuefilename1;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileDialog fDg = new OpenFileDialog();
            int selectedIndex = comboBox1.SelectedIndex;
            
            if (selectedIndex == 0)
            {
                
                if (fDg.ShowDialog() == DialogResult.OK)
                {
                    FTClientCode1.SendFile(fDg.FileName);
                }   
            }
            else if (selectedIndex == 1)
            {

                if (fDg.ShowDialog() == DialogResult.OK)
                {
                    FTClientCode2.SendFile(fDg.FileName);
                }
            }
            else if (selectedIndex == 2)
            {
                if (fDg.ShowDialog() == DialogResult.OK)
                {
                    FTClientCode3.SendFile(fDg.FileName);
                }
            
            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = FTClientCode1.curMsg;
            label4.Text = FTClientCode2.curMsg;
            label5.Text = FTClientCode3.curMsg;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

            string selectedIndex = comboBox1.SelectedItem.ToString();
            if (selectedIndex == "micro1")
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = @"C:\SAS\Patient Storage\micro1\";
                string filename = textBox1.Text;

                saveFileDialog1.FileName = filename + ".txt";
                saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 0;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                   
                    System.IO.StreamWriter SW = new System.IO.StreamWriter(saveFileDialog1.FileName, false, Encoding.ASCII);
                    SW.Write(richTextBox2.Text);
                    SW.Close();
                }
                MessageBox.Show("micro1 Saved");
            }
            else if (selectedIndex == "micro2")
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = @"C:\SAS\Patient Storage\micro2";
                string filename = textBox1.Text;

                saveFileDialog1.FileName = filename + ".txt";
                saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 0;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamWriter SW = new System.IO.StreamWriter(saveFileDialog1.FileName, false, Encoding.ASCII);
                    SW.Write(richTextBox2.Text);
                    SW.Close();
                }
                MessageBox.Show("micro2 Saved");
            }
            else if (selectedIndex == "micro3")
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = @"C:\SAS\Patient Storage\micro3";
                string filename = textBox1.Text;

                saveFileDialog1.FileName = filename + ".txt";
                saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 0;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamWriter SW = new System.IO.StreamWriter(saveFileDialog1.FileName, false, Encoding.ASCII);
                    SW.Write(richTextBox2.Text);
                    SW.Close();
                }
                MessageBox.Show("micro3 Saved");

            }

           
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //comboBox1.Text = _valuecombo;
            textBox1.Text = _valuefilename;
           // richTextBox1.Text = _valuefilename1;



            string[] files = Directory.GetFiles(@"C:\OUT_Cloud\Client_storage");
            foreach (string file in files)
            {
                FileInfo f = new FileInfo(file);
                f.Delete();

            }
            string[] files1 = Directory.GetFiles(@"C:\OUT_Cloud\Server_Cloud");
            foreach (string file1 in files)
            {
                FileInfo f = new FileInfo(file1);
                f.Delete();

            }

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //**************Create Client Micro aggregation process***********************
            string mainsubfolder = comboBox1.SelectedItem.ToString();
            string folderName = @"c:\OUT_Cloud\Client_storage";
            string pathString = System.IO.Path.Combine(folderName, mainsubfolder);
            subfolder = textBox1.Text;
            pathString = System.IO.Path.Combine(pathString, subfolder.ToString());
            System.IO.Directory.CreateDirectory(pathString);

            //**************Create Server Micro agg process*********************************************

            string folderName1 = @"c:\OUT_Cloud\Server_Cloud";
            string pathString1 = System.IO.Path.Combine(folderName1, mainsubfolder);
            subfolder = textBox1.Text;
            pathString1 = System.IO.Path.Combine(pathString1, subfolder.ToString());
            System.IO.Directory.CreateDirectory(pathString1);

            MessageBox.Show("Service Space Allocated");
               







        }

        private void button4_Click(object sender, EventArgs e)
        {
            patient_login mainpage = new patient_login();
            mainpage.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string s = File.ReadAllText(ofd.FileName);
                richTextBox1.Text = s;
            }
        }

        private void chkIsCustomKey_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }
        
        
        public string _valuecombo { get; set; }

        private void button4_Click_1(object sender, EventArgs e)
        {
            RC4Engine myRC4Engine = new RC4Engine();
            myRC4Engine.EncryptionKey = this.enkey.Text;
            myRC4Engine.InClearText = this.richTextBox1.Text;
            myRC4Engine.Encrypt();
            //
            // Save also to string the result because some char could
            // be lost into textbox
            //
            this.m_sCrypSave = myRC4Engine.CryptedText;
            this.richTextBox2.Text = this.m_sCrypSave;
        }

        public string m_sCrypSave { get; set; }

        private void button6_Click_1(object sender, EventArgs e)
        {
           //  Create aggregation process **************************
            string mainsubfolder = comboBox1.SelectedItem.ToString();
            string folderName = @"c:\OUT_Cloud\Client_storage";


            string pathString = System.IO.Path.Combine(folderName, mainsubfolder);

            subfolder = textBox1.Text;
            pathString = System.IO.Path.Combine(pathString, subfolder.ToString());
           // System.IO.Directory.CreateDirectory(pathString);
            //*****************************************************

            //  Create microaggregation process **************************

            string folderName1 = @"c:\OUT_Cloud\Server_Cloud";
            string pathString1 = System.IO.Path.Combine(folderName1, mainsubfolder);
            pathString1 = System.IO.Path.Combine(pathString1, subfolder.ToString());
            
            using (ZipFile zip = new ZipFile())
            {

                foreach (string file in Directory.GetFiles(pathString))
                {
                    zip.AddFile(file, Path.GetFileName(file));
                }
                zip.Save(pathString1+"\\"+subfolder+".zip");
                MessageBox.Show("Gzip compression sucessfully");

                con.Open();

                SqlCommand cmd = new SqlCommand("insert into patientdescrip values('" + textBox1.Text + "','" + comboBox1.SelectedItem.ToString() + "','" + richTextBox1.Text +"','"+enkey.Text+ "')", con);
                cmd.ExecuteNonQuery();
                con.Close();
 
            
            }
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            RC4Engine myRC4Engine = new RC4Engine();
            myRC4Engine.EncryptionKey = this.enkey.Text;
            myRC4Engine.InClearText = this.richTextBox1.Text;
            myRC4Engine.Encrypt();
            //
            // Save also to string the result because some char could
            // be lost into textbox
            //
            this.m_sCrypSave = myRC4Engine.CryptedText;
            this.richTextBox2.Text = this.m_sCrypSave;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }


   



    //FILE TRANSFER 1 USING C#.NET SOCKET - CLIENT
    class FTClientCode1
    {
        public static string curMsg = "Idle";
        public static void SendFile(string fileName)
        {
            try
            {
                IPAddress[] ipAddress = Dns.GetHostAddresses("127.0.0.1");
                IPEndPoint ipEnd = new IPEndPoint(ipAddress[0], 5656);
                Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);


                string filePath = "";

                fileName = fileName.Replace("\\", "/");
                while (fileName.IndexOf("/") > -1)
                {
                    filePath += fileName.Substring(0, fileName.IndexOf("/") + 1);
                    fileName = fileName.Substring(fileName.IndexOf("/") + 1);
                }


                byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);
                if (fileNameByte.Length > 850 * 1024)
                {
                    curMsg = "File size is more than 850kb, please try with small file.";
                    return;
                }

                curMsg = "Buffering ...";
                byte[] fileData = File.ReadAllBytes(filePath + fileName);
                byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
                byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);

                fileNameLen.CopyTo(clientData, 0);
                fileNameByte.CopyTo(clientData, 4);
                fileData.CopyTo(clientData, 4 + fileNameByte.Length);

                curMsg = "Connection to server ...";
                clientSock.Connect(ipEnd);

                curMsg = "File sending...";
                clientSock.Send(clientData);

                curMsg = "Disconnecting...";
                clientSock.Close();
                curMsg = "File transferred.";
                MessageBox.Show("f1");
                
            }
            catch (Exception ex)
            {
                if(ex.Message=="No connection could be made because the target machine actively refused it")
                    curMsg="File Sending fail. Because server not running." ;
                else
                    curMsg = "File Sending fail." + ex.Message;
            }

        }
    }



    //FILE TRANSFER 2 USING C#.NET SOCKET - CLIENT
    class FTClientCode2
    {
        public static string curMsg = "Idle";
        public static void SendFile(string fileName)
        {
            try
            {
                IPAddress[] ipAddress = Dns.GetHostAddresses("127.0.0.1");
                IPEndPoint ipEnd = new IPEndPoint(ipAddress[0], 5657);
                Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);


                string filePath = "";

                fileName = fileName.Replace("\\", "/");
                while (fileName.IndexOf("/") > -1)
                {
                    filePath += fileName.Substring(0, fileName.IndexOf("/") + 1);
                    fileName = fileName.Substring(fileName.IndexOf("/") + 1);
                }


                byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);
                if (fileNameByte.Length > 850 * 1024)
                {
                    curMsg = "File size is more than 850kb, please try with small file.";
                    return;
                }

                curMsg = "Buffering ...";
                byte[] fileData = File.ReadAllBytes(filePath + fileName);
                byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
                byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);

                fileNameLen.CopyTo(clientData, 0);
                fileNameByte.CopyTo(clientData, 4);
                fileData.CopyTo(clientData, 4 + fileNameByte.Length);

                curMsg = "Connection to server ...";
                clientSock.Connect(ipEnd);

                curMsg = "File sending...";
                clientSock.Send(clientData);

                curMsg = "Disconnecting...";
                clientSock.Close();
                curMsg = "File transferred.";
                MessageBox.Show("f2");

            }
            catch (Exception ex)
            {
                if (ex.Message == "No connection could be made because the target machine actively refused it")
                    curMsg = "File Sending fail. Because server not running.";
                else
                    curMsg = "File Sending fail." + ex.Message;
            }

        }
    }
    //FILE TRANSFER 3 USING C#.NET SOCKET - CLIENT
    class FTClientCode3
    {
        public static string curMsg = "Idle";
        public static void SendFile(string fileName)
        {
            try
            {
                IPAddress[] ipAddress = Dns.GetHostAddresses("127.0.0.1");
                IPEndPoint ipEnd = new IPEndPoint(ipAddress[0], 5658);
                Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);


                string filePath = "";

                fileName = fileName.Replace("\\", "/");
                while (fileName.IndexOf("/") > -1)
                {
                    filePath += fileName.Substring(0, fileName.IndexOf("/") + 1);
                    fileName = fileName.Substring(fileName.IndexOf("/") + 1);
                }


                byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);
                if (fileNameByte.Length > 850 * 1024)
                {
                    curMsg = "File size is more than 850kb, please try with small file.";
                    return;
                }

                curMsg = "Buffering ...";
                byte[] fileData = File.ReadAllBytes(filePath + fileName);
                byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
                byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);

                fileNameLen.CopyTo(clientData, 0);
                fileNameByte.CopyTo(clientData, 4);
                fileData.CopyTo(clientData, 4 + fileNameByte.Length);

                curMsg = "Connection to server ...";
                clientSock.Connect(ipEnd);

                curMsg = "File sending...";
                clientSock.Send(clientData);

                curMsg = "Disconnecting...";
                clientSock.Close();
                curMsg = "File transferred.";
                MessageBox.Show("f3");

            }
            catch (Exception ex)
            {
                if (ex.Message == "No connection could be made because the target machine actively refused it")
                    curMsg = "File Sending fail. Because server not running.";
                else
                    curMsg = "File Sending fail." + ex.Message;
            }

        }
    }
}
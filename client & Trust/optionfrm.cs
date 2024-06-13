using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters;
using System.IO;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.Management;
using FTClientCode1;

namespace FTClientCode
{
    public partial class optionfrm : Form
    {
        public string coco, destip;
        public string BaseFileName;
        public string FilePath;
        const int count = 3;
        public string FileName;
        public string gatway2;
        public string SourceIP;
        public string DesIP;
        public string thisIP = "";
        public string path3;
        public string path;
        public static int currentBitStrength = 0;
        public delegate void FinishedProcessDelegate();
        public delegate void UpdateBitStrengthDelegate(int bitStrength);
        public delegate void UpdateTextDelegate(string inputText);
        NetworkStream NetWork;
        TcpListener TCPL, TCPL1;
        Socket mysock;
        Thread myth;
        string[] ReceivedItemsBuffer;
        string mergeFolder;

        public optionfrm()
        {
            InitializeComponent();
        }
        List<string> Packets = new List<string>();
        public string routerip, multi, destinationip, length;


        private void button1_Click(object sender, EventArgs e)
        {
            patdetails patde = new patdetails();

            patde._valuefilename = textBox1.Text;
            patde.Show();



            }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            randomkey rankey = new randomkey();
            rankey._valuefilename = textBox1.Text;
            rankey.Show();
        }

        public string _valuefilename { get; set; }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 patdes = new Form1();
            patdes._valuefilename = textBox1.Text;
            
            patdes.Show();
            
               
        }

        private void optionfrm_Load(object sender, EventArgs e)
        {
           
            textBox1.Text = _valuefilename;


            //**************tcp connection*********************
            try
            {

                string thisip = "";
                thisip = System.Net.Dns.GetHostName();
                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(thisip);
                IPAddress[] addr = ipEntry.AddressList;
                thisIP = addr[addr.Length - 1].ToString();

                TCPL = new TcpListener(8087);
                TCPL.Start();

                for (int i = 0; i <= count; i++)
                {
                    myth = new Thread(new System.Threading.ThreadStart(Receving)); // Start Thread Session
                    myth.Start();
                }
                //+++++++++++++++++++++++++++++++++++++++++++++++

            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "Error Message");

            }
        }
        void Receving()
        {

            mysock = TCPL.AcceptSocket();
            try
            {
                NetWork = new NetworkStream(mysock);
                BinaryFormatter bf = new BinaryFormatter();
                object op;
                op = bf.Deserialize(NetWork);
                BinaryReader br = new BinaryReader(NetWork);
                byte[] buffer = br.ReadBytes(6000000);
                string ReceivedItems;
                ReceivedItems = op.ToString();
                ReceivedItemsBuffer = ReceivedItems.Split(',', '*', '#');
                FileName = ReceivedItemsBuffer[0].ToString();
                gatway2 = ReceivedItemsBuffer[1].ToString();
                SourceIP = ReceivedItemsBuffer[3].ToString();
                DesIP = ReceivedItemsBuffer[2].ToString();
                this.BeginInvoke(new MethodInvoker(delegate()
                {
                    this.label1.Text = FileName;
                    MessageBox.Show("Doctor Send the Request ");

                }));
               // MessageBox.Show("Request send to Client", "Server Request", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }


        }
    }

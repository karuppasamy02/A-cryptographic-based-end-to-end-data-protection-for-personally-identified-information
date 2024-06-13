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


using CryptoRC4;

namespace FTServerCode
{
    public partial class m2a3 : Form
    {

        string FileName1;
        byte[] m_DataBuffer = new byte[10];
        public string name;
        string[] packetnames = new string[6];

        public FileStream fs;

        public string coco, destip;
        public string BaseFileName;
        public string FilePath;
        const int count = 3;
        public string FileName;
        public string dekey;
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

        SqlConnection con = new SqlConnection("Data Source=BLACKGOD\\SQLEXPRESS;Initial Catalog=cvhealthcare;Persist Security Info=True;User ID=sa;Password=admin123");
        string path1;

        public m2a3()
        {
            InitializeComponent();
        }
        List<string> Packets = new List<string>();
        public string routerip, multi, destinationip, length;




        public string _pathname3 { get; set; }

        private void button7_Click(object sender, EventArgs e)
        {
            FileName1 ="Doctor requesting" ;
            name = FileName1;
            
            string ip = "127.0.0.1";
            multi = ip;
            routerip = ip;
            destinationip = ip;
            sendpacket(multi, routerip, destinationip, name);

        }

        public int Status = 0;
        public void sendpacket(string multi, string routerip, string destinationip, string filename3)
        {
            try
            {
                if (Status == 0)
                {

                    string thisip = "";
                    thisip = System.Net.Dns.GetHostName();
                    IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(thisip);
                    IPAddress[] addr = ipEntry.AddressList;
                    string thisIP = addr[addr.Length - 1].ToString();

                    string buffer = routerip + "*" + destinationip + "#" + thisIP;
                    TcpClient myclient = new TcpClient(multi, 8087);
                    NetworkStream myns = myclient.GetStream();
                    BinaryFormatter br = new BinaryFormatter();
                    br.Serialize(myns, filename3 + "," + buffer);
                    BinaryWriter mybw = new BinaryWriter(myns);
                    string buffer1 = filename3.ToString();
                    mybw.Write(buffer1);
                    mybw.Close();
                    myns.Close();
                    myclient.Close();
                }
                else
                {
                    MessageBox.Show("The  Is Not Connected So The Packet Forwaded to Another routers", "Serever");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from register where name='" + textBox1.Text + "'and randomkey='" + textBox2.Text + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            path1 = textBox3.Text;
            if (dr.Read() == true)
            {
                MessageBox.Show("sucessfull key matched....!");
                using (var zip = Ionic.Zip.ZipFile.Read(path1 + "\\" + textBox1.Text + ".zip"))
                {
                    zip.ExtractAll("C:\\Extract\\ex3");
                    MessageBox.Show("Sucessfull Decomprocessed");

                    //aggregation1 agg1 = new aggregation1();
                    //agg1.Show();

                }

            }
            else
            {
                MessageBox.Show("error");
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select * from patientdescrip where patname='" + textBox1.Text + "'", con);
            SqlDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                txtEncryption.Text = dr[3].ToString();
                richTextBox2.Text = dr[2].ToString();
            }
            con.Close();

            panel1.Visible = true;
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RC4Engine myRC4Engine = new RC4Engine();
            myRC4Engine.EncryptionKey = this.txtEncryption.Text;
            myRC4Engine.InClearText = this.richTextBox2.Text;
            myRC4Engine.Encrypt();
            this.m_sCrypSave = myRC4Engine.CryptedText;
            this.richTextBox1.Text = this.m_sCrypSave;
        }
        #region User Private Field
        //
        // Used to store Crypted Text
        //
        private string m_sCrypSave = "";
        #endregion



        private void button5_Click(object sender, EventArgs e)
        {
            RC4Engine myRC4Engine = new RC4Engine();
            myRC4Engine.EncryptionKey = this.textBox4.Text;
            myRC4Engine.CryptedText = this.m_sCrypSave;
            myRC4Engine.Decrypt();
            this.richTextBox3.Text = myRC4Engine.InClearText;
            label4.Visible = false;
            textBox4.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RC4Engine myRC4Engine = new RC4Engine();
            myRC4Engine.EncryptionKey = this.txtEncryption.Text;
            myRC4Engine.InClearText = this.richTextBox3.Text;
            myRC4Engine.Encrypt();
            this.m_sCrypSave = myRC4Engine.CryptedText;
            this.richTextBox1.Text = this.m_sCrypSave;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"C:\SAS\SAS\micro3\" + textBox1.Text;
            string filename = textBox1.Text;

            saveFileDialog1.FileName = filename + ".doc";
            saveFileDialog1.Filter = "word files (*.doc)|*.docx|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 0;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                System.IO.StreamWriter SW = new System.IO.StreamWriter(saveFileDialog1.FileName, false, Encoding.ASCII);
                SW.Write(richTextBox1.Text);
                SW.Close();
            }
            MessageBox.Show("File Saved");




            SqlCommand cmd = new SqlCommand("update patientdescrip set descrip ='" + richTextBox3.Text + "' where patname='" + textBox1.Text + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Update Successfull");




            Random rm = new Random();
            int s = rm.Next(100000, 999999);
            string chkey = s.ToString();

            SqlCommand cmd2 = new SqlCommand("update register set randomkey ='" + chkey + "' where name='" + textBox1.Text + "'", con);
            con.Open();
            cmd2.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Project Completed");

        
        }

        private void m2a3_Load(object sender, EventArgs e)
        {
           textBox3.Text = _pathname2;
           
            try
            {

                string thisip = "";
                thisip = System.Net.Dns.GetHostName();
                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(thisip);
                IPAddress[] addr = ipEntry.AddressList;
                thisIP = addr[addr.Length - 1].ToString();

                TCPL = new TcpListener(8086);
                TCPL.Start();

                for (int i = 0; i <= count; i++)
                {
                    myth = new Thread(new System.Threading.ThreadStart(Receving)); // Start Thread Session
                    myth.Start();
                }
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

                dekey = ReceivedItemsBuffer[1].ToString();
                SourceIP = ReceivedItemsBuffer[3].ToString();
                DesIP = ReceivedItemsBuffer[2].ToString();
                this.BeginInvoke(new MethodInvoker(delegate()
                {
                    this.textBox2.Text = FileName;
                    this.textBox4.Text = DesIP;

                }));
                MessageBox.Show("Key Received", "key ACK", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }






        public string _pathname2 { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Security.Cryptography;
using System.Data.SqlClient;


namespace FTClientCode
{
    public partial class randomkey : Form
    {
        public string dekey;
        string FileName;
        byte[] m_DataBuffer = new byte[10];
        public string name;
        string[] packetnames = new string[6];

        public FileStream fs;
        
        SqlConnection con = new SqlConnection("Data Source=BLACKGOD\\SQLEXPRESS;Initial Catalog=cvhealthcare;Persist Security Info=True;User ID=sa;Password=admin123");
        
        public randomkey()
        {
            InitializeComponent();
        }
        List<string> Packets = new List<string>();
        public string routerip, multi, destinationip, length;

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select * from register where name='" + textBox1.Text + "'", con);
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    textBox4.Text = dr[2].ToString();

                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            
            }
        }

        private void randomkey_Load(object sender, EventArgs e)
        {
            textBox1.Text = _valuefilename;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random rm = new Random();
            int s = rm.Next(100000, 999999);
            textBox2.Text = s.ToString();


                con.Open();
                SqlCommand cmd1 = new SqlCommand("select * from patientdescrip where patname='" + textBox1.Text + "'", con);
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    textBox3.Text = dr[3].ToString();

                }
                con.Close();
            



        }

        private void button5_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update register set randomkey ='" + textBox2.Text + "' where name='" + textBox1.Text + "'", con);
            MessageBox.Show(" Update Successful");
            cmd.ExecuteNonQuery();
            con.Close();

            ////file dekey**********************
            //try
            //{
            //    con.Open();
            //    SqlCommand cmd1 = new SqlCommand("select randomkey from register where name='" + textBox1.Text + "'", con);
            //    SqlDataReader dr = cmd1.ExecuteReader();
            //    while (dr.Read())
            //    {
            //        dekey = dr[2].ToString();

            //    }
            //    con.Close();
            //                }
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());

            //}
            ////**************************************


        


            //sending process

            FileName = textBox2.Text;
            name = FileName;

            dekey = textBox3.Text;

            //textbox2 is random key ****************
            string ip = "127.0.0.1";
            multi = ip;
            routerip = ip;
            destinationip = dekey;
            sendpacket(multi, routerip, destinationip, name);

            //==========================================

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
                    TcpClient myclient = new TcpClient(multi, 8086);
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





        public string _valuefilename { get; set; }

        private void button5_Click_1(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update register set randomkey ='" + textBox2.Text + "' where name='" + textBox1.Text + "'", con);
            MessageBox.Show(" Update Successful");
            cmd.ExecuteNonQuery();
            con.Close();



            //sending process

            FileName = textBox2.Text;
            name = FileName;

            dekey = textBox3.Text;

            //textbox2 is random key ****************
            string ip = "127.0.0.1";
            multi = ip;
            routerip = ip;
            destinationip = dekey;
            sendpacket(multi, routerip, destinationip, name);

            //==========================================

            label7.Visible = false;
            label8.Visible = true;
        }
    }
}

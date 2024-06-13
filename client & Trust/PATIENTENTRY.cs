using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using FTClientCode1;

namespace FTClientCode
{
    public partial class PATIENTENTRY : Form
    {
        SqlConnection con = new SqlConnection("Data Source=BLACKGOD\\SQLEXPRESS;Initial Catalog=cvhealthcare;Persist Security Info=True;User ID=sa;Password=admin123");
        public string _valuefilename = "";
        
        public PATIENTENTRY()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 patdes = new Form1();
            patdes._valuecombo = comboBox1.Text;
            patdes._valuefilename = textBox3.Text;
            patdes._valuefilename1 = textBox1.Text;
            patdes.Show();
            this.Hide();
        }

        private void PATIENTENTRY_Load(object sender, EventArgs e)
        {

            textBox3.Text = _valuefilename123;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random rm = new Random();
            int s = rm.Next(100000, 999999);
            textBox2.Text = s.ToString();

           

        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select * from register where name='" + textBox3.Text + "'", con);
            SqlDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                textBox4.Text = dr[2].ToString();

            }
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Enabled=true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update register set randomkey ='" + textBox4.Text + "' where randomkey='" + textBox2.Text + "'", con);
            MessageBox.Show(" Update Successful");
            cmd.ExecuteNonQuery();
            con.Close();
        }



        public string _valuefilename123 { get; set; }
    }
}

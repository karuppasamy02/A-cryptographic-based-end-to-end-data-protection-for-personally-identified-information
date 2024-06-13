using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FTClientCode1;
using System.IO;
using System.Data.SqlClient;


namespace FTClientCode
{
    public partial class patient_login : Form
    {
        SqlConnection con = new SqlConnection("Data Source=BLACKGOD\\SQLEXPRESS;Initial Catalog=cvhealthcare;Persist Security Info=True;User ID=sa;Password=admin123");

        public string _valuefilename = "";
        public patient_login()
        {
            InitializeComponent();
        }

        private void patient_login_Load(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from register where name='" + textBox1.Text + "'and password='" + textBox2.Text+"'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                optionfrm opt = new optionfrm();
                opt._valuefilename = textBox1.Text;
                opt.Show();
                PATIENTENTRY pe = new PATIENTENTRY();
                pe._valuefilename = textBox1.Text;
                
                this.Hide();
            }
            else
            {
                MessageBox.Show ("Pls..!Enter Correct username,Password");
            }

            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label6.Visible = true;
            panel1.Visible = true;
            panel3.Visible = true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into register values('" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text+"')", con);
            cmd.ExecuteNonQuery();
            label8.Visible = true;
            label9.Visible = true;
            con.Close();

            }

        private void button6_Click(object sender, EventArgs e)
        {
            Random rm = new Random();
            int s = rm.Next(100000, 999999);
            textBox5.Text = s.ToString();
        }
    }
}

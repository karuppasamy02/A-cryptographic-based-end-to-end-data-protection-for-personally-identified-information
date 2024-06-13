using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace FTClientCode
{
    public partial class patdetails : Form
    {
        SqlConnection con = new SqlConnection("Data Source=BLACKGOD\\SQLEXPRESS;Initial Catalog=cvhealthcare;Persist Security Info=True;User ID=sa;Password=admin123");

        public patdetails()
        {
            InitializeComponent();
        }

        private void patdetails_Load(object sender, EventArgs e)
        {
            textBox3.Text = _valuefilename;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select * from patientdescrip where patname='" + textBox3.Text + "'", con);
            SqlDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                textBox1.Text = dr[2].ToString();
                
            }
            con.Close();

        }

        public string _valuefilename { get; set; }
    }
}

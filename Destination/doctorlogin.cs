using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FTServerCode
{
    public partial class doctorlogin : Form
    {
        public doctorlogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "doctor1" && textBox2.Text == "doctor1")
            {
                m2a1 a1 = new m2a1();
                a1._pathname1 = label3.Text;
                a1.Show();
                
                this.Hide();
            
            }
            else if (textBox1.Text == "doctor2" && textBox2.Text == "doctor2")
            {
                m2a2 a2 = new m2a2();
                a2._pathname2 = label4.Text;
                a2.Show();

                this.Hide();
            }
            else if (textBox1.Text == "user3" && textBox2.Text == "user3")
            {
                m2a3 a3 = new m2a3();
                a3._pathname3 = label5.Text;
                a3.Show();
                this.Hide();
            
            
            }
            }

        public string _pathname1 { get; set; }

        private void doctorlogin_Load(object sender, EventArgs e)
        {
            label3.Text = _pathname1;
            label4.Text = _pathname2;
            label5.Text = _pathname3;

        }

        public string _pathname2 { get; set; }

        public string _pathname3 { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp17
{
    public partial class Form1 : Form
    {
        private Work works = new Work();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            TableTrue tabletrueForm = new TableTrue();
            works.OpenForm(this,tabletrueForm);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PerfectForm perfectForm = new PerfectForm();
            works.OpenForm(this, perfectForm);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void изменитьВариантToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                works.InputEX();
            }
            catch
            {
                works.reloadConfigFile();
                works.InputEX();
            }
           

        }

        private void проверитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                works.ChangeEX(dataGridView1);
            }
            catch
            {
                works.reloadConfigFile();
                works.ChangeEX(dataGridView1);
            }
           
        }
    }
}

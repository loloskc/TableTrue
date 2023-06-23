using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp17
{
    public partial class PerfectForm : Form
    {
        private Work works = new Work();
        private string infoName = "Программа разработана студентом:\n" +
            " Демиодовом Денисом\n" +
           "Группа: П-386";

        public PerfectForm()
        {
            InitializeComponent();
        }

        private void PerfectForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            works.OpenMainForm(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TheoryForm theory = new TheoryForm();
            works.OpenForm(this,theory);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            works.OpenMainForm(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TestingForms testing = new TestingForms();
            works.OpenForm(this,testing);
        }

        private void PerfectForm_Load(object sender, EventArgs e)
        {
            if (works.ReadingFileConfig(1))
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ControlForms control =new  ControlForms();
            works.OpenForm(this, control);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            works.MessageformShow("Ифнормация", infoName);
            
        }
    }
}

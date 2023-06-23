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
    public partial class TableTrue : Form
    {
        private Work works = new Work();
        private string infoName = "Программа разработана студентом: Лебедевым Никитой\n" +
            "Группа: П-386";

        public TableTrue()
        {
            InitializeComponent();
        }

        private void TableTrue_FormClosing(object sender, FormClosingEventArgs e)
        {
            works.OpenMainForm(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            works.OpenMainForm(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TheoryForTable theory = new TheoryForTable();
            works.OpenForm(this, theory);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TestingForTable test = new TestingForTable();
            works.OpenForm(this, test);
        }

        private void TableTrue_Load(object sender, EventArgs e)
        {
            if (works.ReadingFileConfig(0))
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
            TableControl control = new TableControl();
            works.OpenForm(this, control);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            works.MessageformShow("Информация", infoName); 
            
        }
    }
}

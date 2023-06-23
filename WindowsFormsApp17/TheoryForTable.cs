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
    public partial class TheoryForTable : Form
    {
        private Work works = new Work();

        public TheoryForTable()
        {
            InitializeComponent();
        }

        private PictureBox[] Images()
        {
            var images = new[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4,pictureBox5,pictureBox6,pictureBox7,pictureBox8};
            return images;
        }

        private void TheoryForTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            TableTrue form = new TableTrue();
            works.OpenForm(this, form);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            works.colorFalse = Color.White;
            label1.Text = Convert.ToString(works.ListImagesNext(Images(),8) + 1);
            if (works.ButtonsForTesting(Convert.ToInt32(label1.Text), 8)) button3.BackColor = works.colorTrue;
            else button3.BackColor = works.colorFalse;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            works.colorFalse = Color.White;
            label1.Text = Convert.ToString(works.ListImagesBack(Images(),8) + 1);
            if (works.ButtonsForTesting(Convert.ToInt32(label1.Text), 8)) button3.BackColor = works.colorTrue;
            else button3.BackColor = works.colorFalse;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TestingForTable testingForTable = new TestingForTable();
            works.OpenForm(this, testingForTable);
        }
    }
}

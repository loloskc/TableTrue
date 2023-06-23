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
    public partial class TheoryForm : Form
    {
        private Work works = new Work();
        
        
       
        public TheoryForm()
        {
            
            InitializeComponent();
        }

        private PictureBox[] Images()
        {
            var images = new[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6,pictureBox7 };
            return images;
        }

        private void TheoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            PerfectForm bakcForm = new PerfectForm();
            works.OpenForm(this, bakcForm);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            works.colorFalse = Color.White;
            label1.Text = Convert.ToString(works.ListImagesBack(Images(),7)+1);
            if (works.ButtonsForTesting(Convert.ToInt32(label1.Text), 7)) button3.BackColor = works.colorTrue;
            else button3.BackColor = works.colorFalse;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            works.colorFalse = Color.White;
            label1.Text = Convert.ToString(works.ListImagesNext(Images(),7)+1);
            if (works.ButtonsForTesting(Convert.ToInt32(label1.Text), 7)) button3.BackColor =works.colorTrue;
            else button3.BackColor = works.colorFalse;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TestingForms testing = new TestingForms(); 
            works.OpenForm(this, testing);
        }
    }
}

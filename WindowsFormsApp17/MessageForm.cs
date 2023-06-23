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
    public partial class MessageForm : Form
    {
        private Work works = new Work();
        public MessageForm()
        {
            InitializeComponent();
        }


        public MessageForm(string title, string text)
        {
            InitializeComponent();
            this.Text = title;
            label1.Text = text;
        }

      

        private void MessageForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

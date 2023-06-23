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
    public partial class ControlForms : Form
    {
        private Work works = new Work();
        private Table tables = new Table();
        private FormWork forms = new FormWork();
        private string ex = string.Empty;
        private string title = "Оценка";

        public ControlForms()
        {
            InitializeComponent();
        }

        private void ControlForms_Load(object sender, EventArgs e)
        {

            string exp = works.Choice();

            try
            {
                tables.LoadTables(dataGridView1, exp);
            }
            catch
            {
                works.MessageformShow("Ошибка", "Неверное содержания файла CONFIG\n" +
                    "Файл вернется в режим по sумолчанию");
                works.reloadConfigFile();
                exp = works.Choice();
                tables.LoadTables(dataGridView1, exp);
            }

            ex = exp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (textBox1.Text == forms.ForControlResultSDNF(dataGridView1))
            {
                result++;
            }
            if (textBox2.Text == forms.ForControlResultSKNF(dataGridView1))
            {
                result++;
            }

            switch (result)
            {
                case 1:
                    works.MessageformShow(title, "Вы набрали 1 балл из 2");
                    break;
                case 2:
                    works.MessageformShow(title, "Вы набрали 2 балл из 2");
                    break;
                default:
                    works.MessageformShow(title, "Вы набрали 0 балл из 2");
                    break;
            }

        }

        private void ControlForms_FormClosing(object sender, FormClosingEventArgs e)
        {
            PerfectForm control = new PerfectForm();
            works.OpenForm(this, control);
        }

        private void показатьОтветToolStripMenuItem_Click(object sender, EventArgs e)
        {
            works.MessageformShow("Ответы", forms.ForControlResultSDNF(dataGridView1) + "\n" + forms.ForControlResultSKNF(dataGridView1));
        }
    }
}

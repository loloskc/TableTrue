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
    public partial class TestingForms : Form
    {
        private Work works = new Work();
        private FormWork formWorks = new FormWork();
        private Table tables = new Table();

        public TestingForms()
        {
            InitializeComponent();
        }

        private void TestingForms_FormClosing(object sender, FormClosingEventArgs e)
        {
            PerfectForm forms = new PerfectForm();
            works.OpenForm(this, forms);
        }

        private void TestingForms_Load(object sender, EventArgs e)
        {
            // (A&B)|C 
            string exp = "(A&B)|C";
            tables.MakeTable(dataGridView1, exp);
            tables.LoadTable(dataGridView1, exp);
            tables.ResultTestingTable(dataGridView1, exp);
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                formWorks.MakeSDNF(dataGridView1, i);
                formWorks.MakeSKNF(dataGridView1, i);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Количество ошибок: " + Convert.ToString(formWorks.CheckingResult(dataGridView1));
            tables.NotSelect(dataGridView1);
            if (label1.Text == "Количество ошибок: 0")
            {
                button3.Visible = true;
                works.InputConfig(1);
            }
            else button3.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            works.MessageformShow("Подсказка", "В таблице  должно заполнено 8 ячеек\n" +
                "Неверные ячейки заранее заблокированы для ввода\n" +
                "используй имена столбцов в качестве перменных\n" +
                "для обозначения конъюнкции используй знак - *\n" +
                "для обозначения дезъюнкции используй знак - v\n" +
                "для обозначения отрицанеи используй знак - - (минус) ");
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}

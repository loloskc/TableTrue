using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp17
{
    public partial class TableControl : Form
    {
        private Work works = new Work();
        private Table tables = new Table();
        private string ex = string.Empty;
        public int[,] CellsArray;
        private static char letters = 'A'; //


        public TableControl()
        {
            InitializeComponent();
        }

        private void TableControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            TableTrue tableTrue = new TableTrue();
            works.OpenForm(this, tableTrue);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int countLetter = tables.CountLetter(ex);
            int countRow = Convert.ToInt32(Math.Pow(2, countLetter));
            int columnCount = countLetter + 1;
            if (textBox1.Text.Equals(Convert.ToString(countRow)) && textBox2.Text.Equals(Convert.ToString(columnCount)))
            {
                panel1.Visible = false;
                panel2.Visible = true;
                label4.Text += ex;
                dataGridView2.ColumnCount = dataGridView1.ColumnCount;
                dataGridView2.RowCount = dataGridView1.RowCount;
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    dataGridView2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridView2.Columns[i].HeaderText = Convert.ToChar(letters + i).ToString();
                }
                dataGridView2.Columns[dataGridView2.ColumnCount - 1].HeaderText = "Итог";
                for (int i = 0; i < dataGridView2.RowCount; i++)
                {
                    dataGridView2.Rows[i].Cells[dataGridView2.ColumnCount - 1].Style.BackColor = Color.Gray;
                    dataGridView2.Rows[i].Cells[dataGridView2.ColumnCount - 1].ReadOnly = true;
                }
                button2.Visible = true;
            }
            else if (textBox1.Text.Equals(Convert.ToString(countRow)) && !textBox2.Text.Equals(Convert.ToString(columnCount)))
            {
                button4.Visible = true;
                works.MessageformShow("Ошибка", "Введено неверное количество строк");
            }
            else if (!textBox1.Text.Equals(Convert.ToString(countRow)) && textBox2.Text.Equals(Convert.ToString(columnCount)))
            {
                works.MessageformShow("Ошибка", "Введено неверное количество столбцов");
                button4.Visible = true;
            }
            else if(!textBox1.Text.Equals(Convert.ToString(countRow)) && !textBox2.Text.Equals(Convert.ToString(columnCount)))
            {
                works.MessageformShow("Ошибка", "Введено неверное количество и строк и столбцов");
                button4.Visible = true;
            }
        }

        private void TableControl_Load(object sender, EventArgs e)
        {
            string exp = works.Choice();
            label1.UseMnemonic = false;
            try
            {
                tables.LoadTables(dataGridView1, exp);
                CellsArray = tables.LoadCellsArray(exp);
            }
            catch
            {
                works.MessageformShow("Ошибка", "Неверное содержания файла CONFIG\n" +
                    "Файл вернется в режим по sумолчанию");
                works.reloadConfigFile();
                exp = works.Choice();
                tables.LoadTables(dataGridView1, exp);
                CellsArray = tables.LoadCellsArray(exp);
            }

            ex = exp;
            label1.Text += " ";
            label1.Text += ex;
            tables.LoadTables(dataGridView1, ex);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int error = 0;
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                for (int j = 0; j < dataGridView2.ColumnCount - 1; j++)
                {
                    if (Convert.ToString(dataGridView2.Rows[i].Cells[j].Value) != Convert.ToString(CellsArray[i, j]))
                    {
                        dataGridView2.Rows[i].Cells[j].Style.BackColor = works.colorFalse;
                        error++;
                    }
                    else
                    {
                        dataGridView2.Rows[i].Cells[j].Style.BackColor = works.colorTrue;
                    }
                }
            }
            if (error == 0)
            {
                for (int i = 0; i < dataGridView2.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView2.ColumnCount; j++)
                    {
                        dataGridView2.Rows[i].Cells[dataGridView2.ColumnCount - 1].Style.BackColor = Color.White;
                        dataGridView2.Rows[i].Cells[dataGridView2.ColumnCount - 1].ReadOnly = false;
                    }
                }
                button2.Visible = false;
                button3.Visible = true;
            }
            else
            {
                works.MessageformShow("Ошибка", "Неправильно введены ячейки");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int error = 0;
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    if (!Convert.ToString(dataGridView2.Rows[i].Cells[j].Value).Equals(Convert.ToString(dataGridView1.Rows[i].Cells[j].Value)))
                    {
                        dataGridView2.Rows[i].Cells[j].Style.BackColor = works.colorFalse;
                        error++;
                    }
                    else
                    {
                        dataGridView2.Rows[i].Cells[j].Style.BackColor = works.colorTrue;
                    }
                }
            }
            if (error == 0)
            {
                works.MessageformShow("Круто", "Все правильно");
                works.InputConfig(0);
            }
            else
            {
                works.MessageformShow("Ошибка", "Не правильно");
            }
        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            works.SettingsInputTable(this, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            works.MessageformShow("Подсказка", "Количество строк считается по формуле 2 в степени n\n" +
                "Количество столбцов считается по формуле n + 1\n" +
                "где n - количество перменных");
        }
    }
}

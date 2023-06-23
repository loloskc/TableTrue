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
    public partial class TestingForTable : Form
    {   
        private Work works = new Work();
        private Table table =new Table();
        private int countLetter;

        public TestingForTable()
        {
            InitializeComponent();
        }

        private void TestingForTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            TableTrue tableTrue = new TableTrue();
            works.OpenForm(this, tableTrue);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "8" && textBox2.Text == "5")
            {
                dataGridView1.Visible = true;
                button2.Visible = true;
                button1.Visible = true;
                label1.Visible = true;
                panel1.Visible = false;
                dataGridView1.RowCount = 8;
                dataGridView1.ColumnCount = 5;
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.Columns[0].HeaderText = "A";
                dataGridView1.Columns[1].HeaderText = "B";
                dataGridView1.Columns[2].HeaderText = "C";
                dataGridView1.Columns[3].HeaderText = "(A*B)";
                dataGridView1.Columns[4].HeaderText = "(A*B)|C";
                label1.Text = "Заполните Ячеки A,B,C";
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Gray;
                    dataGridView1.Rows[i].Cells[3].ReadOnly = true;
                    dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.Gray;
                    dataGridView1.Rows[i].Cells[4].ReadOnly = true;
                }
            }
            else if (textBox1.Text != "8" && textBox2.Text == "5")
            {
                works.MessageformShow("Ошибка", "Неправильно введено количество строк");
                button7.Visible = true;
            }
            else if (textBox1.Text == "8" && textBox2.Text != "5")
            {
                works.MessageformShow("Ошибка", "Неправильно введено количество столбцов");
                button7.Visible = true;
            }
            else if (textBox1.Text != "8" && textBox2.Text != "5")
            {
                works.MessageformShow("Ошибка", "Неправильно введено количество столбцов и строк");
                button7.Visible = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            works.SettingKey(e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            works.SettingKey(e);
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            works.SettingsInputTable(this, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            countLetter = 3;
            int count = 0;
            int[,] CellsArray = table.LoadCellsArray("abc");

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount - 2; j++)
                {
                    if (Convert.ToString(dataGridView1.Rows[i].Cells[j].Value) == Convert.ToString(CellsArray[i, j]))
                    {
                        dataGridView1[j, i].Style.BackColor = works.colorTrue;
                        dataGridView1[j, i].ReadOnly = true;
                        count++;
                    }
                    else
                    {
                        dataGridView1[j, i].Style.BackColor = works.colorFalse;
                    }
                }
            }
            if (count >= 24)
            {
                label1.Text = "Введите первое действия";
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.White;
                    dataGridView1[3, i].ReadOnly = false;
                    button1.Visible = false;
                    button5.Visible = true;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int count = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (Convert.ToString(dataGridView1.Rows[i].Cells[3].Value).Length != 0)
                {

                    if ((Convert.ToBoolean(Convert.ToInt32(dataGridView1[0, i].Value)) && Convert.ToBoolean(Convert.ToInt32(dataGridView1[1, i].Value))).Equals(Convert.ToBoolean(Convert.ToInt32(dataGridView1[3, i].Value))))
                    {
                        dataGridView1[3, i].Style.BackColor = works.colorTrue;
                        count++;
                    }
                    else
                    {
                        dataGridView1[3, i].Style.BackColor = works.colorFalse;
                    }
                }
                else
                {
                    dataGridView1[3, i].Style.BackColor = works.colorFalse;
                }
            }
            if (count == 8)
            {
                button5.Visible = false;
                label1.Text = "Введите второе действие";
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[3].ReadOnly = true;
                    dataGridView1.Rows[i].Cells[4].ReadOnly = false;
                    dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.White;
                    button6.Visible = true;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int count = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (Convert.ToString(dataGridView1.Rows[i].Cells[4].Value).Length != 0)
                {
                    if ((Convert.ToBoolean(Convert.ToInt32(dataGridView1[2, i].Value)) || Convert.ToBoolean(Convert.ToInt32(dataGridView1[3, i].Value))).Equals(Convert.ToBoolean(Convert.ToInt32(dataGridView1[4, i].Value))))
                    {

                        dataGridView1[4, i].Style.BackColor = works.colorTrue;
                        count++;
                    }
                    else
                    {
                        dataGridView1[4, i].Style.BackColor = works.colorFalse;
                    }
                }
                else
                {
                    dataGridView1[4, i].Style.BackColor =works.colorFalse;
                }
            }
            if (count == 8)
            {
                works.MessageformShow("Круто", "Все правильно");
                button3.Visible = true;
                works.InputConfig(0);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TableControl control = new TableControl();
            works.OpenForm(this,control);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            works.MessageformShow("Ошибка", "Читай теорию");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            works.MessageformShow("Подсказка", "Количество строк формула: 2 в степени n." + "\n" +
                "Количество столбцов формула: n плюс количество действий." +
                "\n" +
                "Где n - количество перменных.");
           
        }
    }
}

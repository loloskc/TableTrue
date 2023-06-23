using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp17
{
    internal class FormWork
    {
        private Work works = new Work();

        public void MakeSDNF(DataGridView table, int Rows)
        {
            if (table.Rows[Rows].Cells[3].Value == "1")
            {
                table.Rows[Rows].Cells[table.ColumnCount - 1].ReadOnly = true;
                //table.Rows[Rows].Cells[table.ColumnCount - 2].Value = creatSDNF(table, Rows,3);

            }

        }
        public void MakeSKNF(DataGridView table, int Rows)
        {
            if (table.Rows[Rows].Cells[3].Value == "0")
            {
                table.Rows[Rows].Cells[table.ColumnCount - 2].ReadOnly = true;
                //table.Rows[Rows].Cells[table.ColumnCount - 1].Value = creatSKNF(table, Rows,3);
            }

        }

        private string CreatSDNF(DataGridView table, int Rows, int result)
        {
            string resultSDNF = string.Empty;
            for (int i = 0; i < table.ColumnCount - result; i++)
            {
                if (table.Rows[Rows].Cells[i].Value.ToString() == "0")
                {
                    resultSDNF += "-";
                    resultSDNF += table.Columns[i].HeaderText;
                }
                else
                {
                    resultSDNF += table.Columns[i].HeaderText;
                }
                resultSDNF += "*";
            }
            resultSDNF = resultSDNF.TrimEnd('*');
            return resultSDNF;
        }
        public string ForControlResultSDNF(DataGridView table)
        {
            string result = string.Empty;
            for (int i = 0; i < table.RowCount; i++)
            {
                if (table.Rows[i].Cells[table.ColumnCount - 1].Value.ToString() == "1")
                {
                    result += "(";
                    result += CreatSDNF(table, i, 1);
                    result += ")";
                    result += "v";
                }
            }
            result = result.TrimEnd('v');
            return result;
        }
        private string CreatSKNF(DataGridView table, int Rows, int result)
        {
            string resultSKNF = string.Empty;
            for (int i = 0; i < table.ColumnCount - result; i++)
            {
                if (table.Rows[Rows].Cells[i].Value.ToString() == "1")
                {
                    resultSKNF += "-";
                    resultSKNF += table.Columns[i].HeaderText;
                }
                else
                {
                    resultSKNF += table.Columns[i].HeaderText;
                }
                resultSKNF += "v";
            }
            resultSKNF = resultSKNF.TrimEnd('v');
            return resultSKNF;
        }
        public string ForControlResultSKNF(DataGridView table)
        {
            string result = string.Empty;
            for (int i = 0; i < table.RowCount; i++)
            {
                if (table.Rows[i].Cells[table.ColumnCount - 1].Value.ToString() == "0")
                {
                    result += "(";
                    result += CreatSKNF(table, i, 1);
                    result += ")";
                    result += "*";
                }
            }
            result = result.TrimEnd('*');
            return result;
        }
        public int CheckingResult(DataGridView table)
        {
            int errorResult = 0;
            for (int i = 0; i < table.RowCount; i++)
            {

                if (table[table.ColumnCount - 3, i].Value.ToString() == "1")
                {
                    if (Convert.ToString(table.Rows[i].Cells[table.ColumnCount - 2].Value) == CreatSDNF(table, i, 3))
                    {
                        table[table.ColumnCount - 2, i].Style.BackColor = works.colorTrue;
                    }
                    else
                    {
                        table[table.ColumnCount - 2, i].Style.BackColor = works.colorFalse;
                        errorResult++;
                    }
                }
                if (table[table.ColumnCount - 3, i].Value.ToString() == "0")
                {
                    if (Convert.ToString(table.Rows[i].Cells[table.ColumnCount - 1].Value) == CreatSKNF(table, i, 3))
                    {
                        table[table.ColumnCount - 1, i].Style.BackColor = works.colorTrue;
                    }
                    else
                    {
                        table[table.ColumnCount - 1, i].Style.BackColor = works.colorFalse;
                        errorResult++;
                    }
                }
            }
            return errorResult;
        }

        public void ResultOpen(DataGridView table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i].Cells[table.ColumnCount - 1].ReadOnly == false)
                    table.Rows[i].Cells[table.ColumnCount - 1].Value = CreatSKNF(table, i, 3);
                if (table.Rows[i].Cells[table.ColumnCount - 2].ReadOnly == false)
                    table.Rows[i].Cells[table.ColumnCount - 2].Value = CreatSDNF(table, i, 3);
            }
        }
    }
}

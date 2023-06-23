using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp17
{
    internal class Table
    {
        private static char letters = 'A';
        public void MakeTable(DataGridView table, string exp)
        {
            table.Rows.Clear();
            table.Columns.Clear();
            table.RowCount = Convert.ToInt32(Math.Pow(2, Convert.ToDouble(CountLetter(exp))));
            table.ColumnCount = CountLetter(exp) + 3;
            for (int i = 0; i < table.ColumnCount; i++)
            {
                table.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                table.Columns[i].HeaderText = Convert.ToChar(letters + i).ToString();

            }
            table.Columns[table.ColumnCount - 3].HeaderText = "Итог";
            table.Columns[table.ColumnCount - 2].HeaderText = "СДНФ";
            table.Columns[table.ColumnCount - 1].HeaderText = "СКНФ";
        }
        public int CountLetter(string exp)
        {
            char[] letters = exp.ToCharArray();
            int countLetter = 0;
            for (int i = 0; i < letters.Length; i++)
            {
                if (char.IsLetter(letters[i])) countLetter++;
            }
            return countLetter;
        }
        public void LoadTable(DataGridView table, string exp)
        {

            for (int i = 0; i < table.RowCount; i++)
            {
                bool[] result = new bool[CountLetter(exp)];
                for (int j = 0; j < CountLetter(exp); j++)
                {
                    table.Rows[i].Cells[j].Value = Convert.ToInt32(result[j] = (i & (1 << (CountLetter(exp) - j - 1))) > 0);

                    table.Rows[i].Cells[j].ReadOnly = true;
                    table.Rows[i].Cells[j + 1].ReadOnly = true;
                }
            }
        }

        public int[,] LoadCellsArray(string exp)
        {
            int rows = Convert.ToInt32(Math.Pow(2, CountLetter(exp)));
            int[,] CellArray = new int[rows, CountLetter(exp)];
            for (int i = 0; i < rows; i++)
            {
                bool[] result = new bool[CountLetter(exp)];
                for (int j = 0; j < CountLetter(exp); j++)
                {
                    CellArray[i, j] = Convert.ToInt32(result[j] = (i & (1 << (CountLetter(exp) - j - 1))) > 0);
                }
            }
            return CellArray;
        }

        public void ResultTestingTable(DataGridView table, string exp)
        {
            for (int i = 0; i < table.RowCount; i++)
            {
                if (Convert.ToBoolean(table.Rows[i].Cells[0].Value) && Convert.ToBoolean(table.Rows[i].Cells[1].Value) || Convert.ToBoolean(table.Rows[i].Cells[2].Value))
                {
                    table.Rows[i].Cells[3].Value = "1";
                }
                else
                {
                    table.Rows[i].Cells[3].Value = "0";
                }
            }
        }
        public void NotSelect(DataGridView table)
        {
            for (int i = 0; i < table.RowCount; i++)
            {
                for (int j = 0; j < table.ColumnCount; j++)
                {
                    table.Rows[i].Cells[j].Selected = false;
                }
            }
        }

        private int GetPrecedence(char token)
        {
            switch (token)
            {
                case '|':
                    return 1;
                case '&':
                    return 2;
                case '!':
                    return 3;
                default:
                    return 0;
            }
        }

        private List<string> ConvertToRPN(string expression)
        {
            List<string> output = new List<string>();
            Stack<string> stack = new Stack<string>();
            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];
                if (c == 'T' || c == 'F')
                {
                    output.Add(c.ToString());
                }
                else if (c == '(')
                {
                    stack.Push(c.ToString());
                }
                else if (c == ')')
                {
                    while (stack.Peek() != "(")
                    {
                        output.Add(stack.Pop());
                    }
                    stack.Pop();
                }
                else if (c == '!' || c == '&' || c == '|')
                {
                    while (stack.Count > 0 && GetPrecedence(c) <= GetPrecedence(stack.Peek()[0]))
                    {
                        output.Add(stack.Pop());
                    }
                    stack.Push(c.ToString());
                }
            }
            while (stack.Count > 0)
            {
                output.Add(stack.Pop());
            }
            return output;
        }

        private bool EvaluateExpression(string expression)
        {
            expression = expression.Replace(" ", "");
            List<string> tokens = ConvertToRPN(expression);
            Stack<bool> stack = new Stack<bool>();
            foreach (string token in tokens)
            {
                if (token == "T")
                {
                    stack.Push(true);
                }
                else if (token == "F")
                {
                    stack.Push(false);
                }
                else if (token == "!")
                {
                    bool operand = stack.Pop();
                    stack.Push(!operand);
                }
                else if (token == "&")
                {
                    bool operand1 = stack.Pop();
                    bool operand2 = stack.Pop();
                    stack.Push(operand1 && operand2);
                }
                else if (token == "|")
                {
                    bool operand1 = stack.Pop();
                    bool operand2 = stack.Pop();
                    stack.Push(operand1 || operand2);
                }
            }
            return stack.Pop();
        }

        public void PrintExpressionResult(string expression, bool[,] truthTable, string ex, DataGridView table)
        {
            int numRows = truthTable.GetLength(0);
            int numVariables = truthTable.GetLength(1);
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numVariables; j++)
                {
                    char variable = (char)('A' + j);
                    expression = expression.Replace(variable.ToString(), truthTable[i, j] ? "T" : "F");
                }
                bool result = EvaluateExpression(expression);
                table.Rows[i].Cells[table.ColumnCount - 1].Value = Convert.ToInt32(result);
                expression = ex;
            }
        }
        public void MakeTableForControl(DataGridView table, string exp)
        {
            table.Rows.Clear();
            table.Columns.Clear();
            table.RowCount = Convert.ToInt32(Math.Pow(2, Convert.ToDouble(CountLetter(exp))));
            table.ColumnCount = CountLetter(exp) + 1;
            for (int i = 0; i < table.ColumnCount; i++)
            {
                table.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                table.Columns[i].HeaderText = Convert.ToChar(letters + i).ToString();

            }
            table.Columns[table.ColumnCount - 1].HeaderText = "Итог";
        }

        public bool[,] BuildTruthTable(int numVariables)
        {
            int numRows = (int)Math.Pow(2, numVariables);
            bool[,] table = new bool[numRows, numVariables];

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numVariables; j++)
                {
                    int mask = 1 << (numVariables - j - 1);
                    table[i, j] = (i & mask) != 0;
                }
            }

            return table;
        }

        public void PrintTruthTable(DataGridView data, bool[,] table)
        {
            int numRows = table.GetLength(0);
            int numColumns = table.GetLength(1);

            for (int i = 0; i < numRows; i++)
            {

                for (int j = 0; j < numColumns; j++)
                {
                    //table.Rows[i].Cells[j].Value = Convert.ToInt32(result[j] = (i & (1 << (countLetter(exp) - j - 1))) > 0);
                }

            }
        }

        public void LoadTables(DataGridView table, string ex)
        {

            //string ex = "A&B|C";
            //string ex = loadFile();
            MakeTableForControl(table, ex);
            int numVariables = CountLetter(ex);
            bool[,] truthTable = BuildTruthTable(numVariables);
            LoadTable(table, ex);
            PrintExpressionResult(ex, truthTable, ex, table);
        }

        public int[,] ConverTableToArray(DataGridView table)
        {
            int[,] arrayTable = new int[table.RowCount, table.ColumnCount];
            for (int i = 0; i < arrayTable.GetLength(0); i++)
            {

                for (int j = 0; j < arrayTable.GetLength(1); j++)
                {
                    arrayTable[i, j] = Convert.ToInt32(table[j, i].Value);
                }
            }
            return arrayTable;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp17
{
    internal class Work
    {
        private static Form1 mainForm = new Form1();
        public static string path = "config.txt";
        public static string pathControl = "configControl.txt";
        private Color color = Color.LightGreen;
        private Color colord = Color.IndianRed;
        public Color colorTrue
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }
        public Color colorFalse
        {
            get
            {
                return colord;
            }
            set
            {
                colord = value;
            }
        }

        public void OpenMainForm(Form closeForm)
        {
            closeForm.Hide();
            mainForm.Show();
        }

        public void OpenForm(Form closeForm, Form openForm)
        {
            closeForm.Hide();
            openForm.Show();
        }

        public int ListImagesNext(PictureBox[] images,int end)
        {
            
            for (int i = 0; i <end; i++)
            {
                if (images[i].Visible == true && i != end-1)
                {
                    images[i].Visible = false;
                    i++;
                    images[i].Visible = true;
                    return i;
                }

                if (images[i].Visible == true && i == end-1)
                {
                    images[i].Visible = false;
                    i = 0;
                    images[i].Visible = true;
                    return i;

                }
            }
            return 0;
        }

        public int ListImagesBack(PictureBox[] images,int end)
        {
            
            for (int i = 0; i < end; i++)
            {
                if (images[i].Visible == true && i != 0)
                {
                    images[i].Visible = false;
                    i--;
                    images[i].Visible = true;
                    return i;

                }

                if (images[i].Visible == true && i == 0)
                {
                    images[i].Visible = false;
                    i = end-1;
                    images[i].Visible = true;
                    return i;

                }
            }
            return 0;
        }

        public bool ButtonsForTesting(int page,int end)
        {
            bool flag = false;
            if (page == end)
            {
                return flag = true;
            }
            return flag;
        }

        private void WriteFiles()
        {
            StreamWriter writeFile = new StreamWriter(path);
            writeFile.WriteLine("A&B");
            writeFile.WriteLine("!A&B|C");
            writeFile.Close();
        }

        private void LoadingConfigControl()
        {
            StreamWriter writeFile = new StreamWriter(pathControl);
            writeFile.WriteLine("ContolTable_0");
            writeFile.WriteLine("ControlForm_0");
            writeFile.Close();
        }

        private void ReadfileControl()
        {
            var fileInfo = new FileInfo(pathControl);
            if (!fileInfo.Exists)
            {
                LoadingConfigControl();
            }
            else
            {
                if (fileInfo.Length <= 13)
                {
                    LoadingConfigControl();
                }
            }
        }

        private void ReadFile()
        {

            var fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
            {
                WriteFiles();
            }
            else
            {
                if (fileInfo.Length == 0)
                {
                    WriteFiles();
                }

            }
        }

        public bool ReadingFileConfig(int i)
        {
            ReadfileControl();
            string[] config = File.ReadAllLines(pathControl);
            char[] letters = config[i].ToCharArray();
            for(int j = 0; j < letters.Length; j++)
            {
                if (char.IsDigit(letters[j]))
                {
                    int digit = Convert.ToInt32(letters[j]);
                    if (digit-48 >=1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
               
        }

        public void InputConfig(int line)
        { 
            ReadfileControl();
            string[] config = File.ReadAllLines(pathControl);
            char[] letters = config[line].ToCharArray();
            for(int i = 0; i < letters.Length; i++)
            {
                if (char.IsDigit(letters[i]))
                {
                    int digit = Convert.ToInt32(letters[i]);
                    digit++;
                    config[line] = config[line].Replace('0', Convert.ToChar(digit));

                    File.WriteAllText(pathControl, config[0] + "\n" + config[1]);
                }
            }
        }

        public string Choice() //радномный выбор
        {
            string exp;
            ReadFile();
            string[] exps = File.ReadAllLines(path);

            Random random = new Random();

            int s = random.Next(0, exps.Length);


            return exps[s];
        }

        public void reloadConfigFile()
        {
            WriteFiles();
        }

        public void SettingKey(KeyPressEventArgs e) 
        {
            char key = e.KeyChar;
            if (!Char.IsDigit(key) && key != 8)
            {
                e.Handled = true;
            }
        }

        public void SettingsInputTable(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox value = (TextBox)e.Control;
            value.KeyPress += new KeyPressEventHandler(Input);
            value.MaxLength = 1;
        }

        private void Input(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '1' && e.KeyChar != '0' && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        public void InputEX()
        {
            var proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = Work.path;
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
        }
        private void CheckEx(DataGridView table)
        {
            Table tables = new Table();
            string[] exps = File.ReadAllLines(path);
            for(int i = 0; i < exps.Length; i++)
            {
                try 
                {
                    tables.LoadTables(table, exps[i]);
                    MessageformShow("Успех", "Варианты заполнены правильно Строка: " + Convert.ToString(i + 1));
                }
                catch
                {
                    MessageformShow("Ошибка", "неправильно заполнены варианты Строка: " + Convert.ToString(i + 1));
                    reloadConfigFile();
                }
            }
            
            
        }
        public void ChangeEX(DataGridView table)
        {
            CheckEx(table);
        }

        public void MyMessageBox(string text)
        {

        }

        public void MessageformShow(string title,string text)
        {
            MessageForm messageForm = new MessageForm(title, text);
            messageForm.ShowDialog();
        }
        



    }
}

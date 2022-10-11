using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba1
{
    public partial class Form1 : Form
    { 
        public List<string> Chars = new List<string> {"*=","-=","+=", "<=",">=", "&", "&&", "*", "$", ";", "-", "+", "/", "{", "}", "(", ")", "=", "<", ">", ",", "()"}; 
        public class ListWithDuplicates : List<KeyValuePair<string, string>>
        {
            public void Add(string key, string value)
            {
                var element = new KeyValuePair<string, string>(key, value);
                this.Add(element);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

 
           string Code = rText.Text.TrimStart().TrimEnd();
          
            string buffer = "";
            for(int i = 0; i < Code.Length; i++)
            {
                if (buffer != "")
                {
                    if (Convert.ToString(Code[i]).Trim().Equals(""))
                    {
                        addToDict(buffer);
                        buffer = "";
                    }
                    else if (Chars.Contains(Convert.ToString(Code[i])))
                    {
                        if (Chars.Contains(Convert.ToString(buffer) + Convert.ToString(Code[i])))
                        {
                            buffer += Code[i];
                        }
                        else
                        {
                            addToDict(buffer);
                            buffer = Convert.ToString(Code[i]); 
                        }
                    }
                    else
                    {
                        if (Chars.Contains(Convert.ToString(buffer[0]))){
                            addToDict(buffer);
                            buffer = "";
                        }
                        buffer += Code[i];
                    }
                }
                else
                {
                    buffer += Code[i];
                }

                if (i == Code.Length - 1)
                {
                    addToDict(buffer);
                }

            }

            buffer = "";
           
        }

        
        public void PrintLeksem(ListWithDuplicates list)
        {
            foreach (KeyValuePair<string, string> dictItem in list)
            {
                this.dataGridView1.Rows.Add(dictItem.Key, dictItem.Value);

            }
        }

        public void addToDict(string lexem)
        {

            if (int.TryParse(lexem, out int numericValue))
            {
                var list = new ListWithDuplicates();
                list.Add(lexem, "L");
                PrintLeksem(list);
            }
            else
            {
                
                if (Chars.Contains(Convert.ToString(lexem)))
                {
                    var list = new ListWithDuplicates();
                    list.Add(lexem, "R");
                    PrintLeksem(list);
                }
                else
                {
                    if (lexem[0] >= 48 && lexem[0] <= 57)
                    {
                        MessageBox.Show($"Ошибка в лексеме: {lexem}", "Вывод лексем", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        var list = new ListWithDuplicates();
                        list.Add(lexem, "I");
                        PrintLeksem(list);
                    }
                }
                
            }
        }

        private void PrintText()
        {
            StreamReader sr = new StreamReader("C:\\3 курс\\Теория автоматов и формальных языков\\Курсовая работа\\Compilator.txt", Encoding.UTF8);
            string text = sr.ReadToEnd();
            rText.AppendText(text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PrintText();
        }
    }
}

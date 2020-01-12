using System;
using System.Text;
using System.Windows.Forms;

namespace App
{
    public partial class FormAnalyzer : Form
    {
        public static readonly string AboutProgram =
            "Программа синтаксического анализа автоматного языка операторов присоединения языка Turbo Pascal, имеющий вид:\r\n\r\n" +
            "WITH <переменная> DO <оператор присваивания>;\r\n\r\n" +
            "<переменная> :: = <идентификатор>[,<идентификатор>]|<идентификатор>.<идентификатор>\r\n\r\n" +
            "<оператор присваивания> :: = <левая часть> := <правая часть>\r\n\r\n" +
            "<левая часть> :: = <идентификатор>[{ [<идентификатор>] [<целая константа>] }]\r\n\r\n" +
            "<правая часть> :: = <оператор>[<операция><оператор>]\r\n\r\n" +
            "<оператор> :: = <идентификатор>[{ [<идентификатор>] [<целая константа>] }]|<константа любая>\r\n\r\n" +
            "<операция> :: = { + - / * div mod}\r\n\r\n" +
            "<идентификатор> — идентификатор языка Turbo Pascal, начинается с буквы или знака подчеркивания, включает буквы, цифры, не допускает пробелы и специальные символы, ввести ограничение на длину(не более 8 символов) и не может быть зарезервированным словом(WITH, DO, div, mod);\r\n" +
            "<константа целая> — целое число в диапазоне от -32768 до +32767;\r\n" +
            "<константа любая> — целое число, число с фиксированной точкой, число с плавающей точкой.\r\n\r\n" +
            "Семантика:\r\n" +
            "Построить таблицу идентификаторов и констант. Учесть перечисленные выше ограничения на идентификаторы и константы.\r\n" +
            "Сообщать об ошибках при анализе цепочек языка, указывая курсором место ошибки и её содержание.";

        private string str;
        private int currPos;

        private StringBuilder sbForIdents;
        private StringBuilder sbForIntConsts;
        private StringBuilder sbForAnyConsts;

        public FormAnalyzer()
        {
            InitializeComponent();
        }

        private void tbInputStr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)13)
                return;

            if (tbInputStr.Text == "")
            {
                tbOutputMessage.Text = "Введите любую строку.";
                return;
            }

            CheckStr();
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            if (tbInputStr.Text == "")
            {
                tbOutputMessage.Text = "Введите любую строку.";
                return;
            }

            CheckStr();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbInputStr.Clear();
            tbOutputMessage.Clear();
            tbOutputIdentsAndConsts.Clear();
        }

        private void btnAboutProgram_Click(object sender, EventArgs e)
        {
            MessageBox.Show(AboutProgram);
        }

        private void CheckStr()
        {
            tbOutputMessage.Clear();
            tbOutputIdentsAndConsts.Clear();

            string newLine = Environment.NewLine;

            str = tbInputStr.Text;
            try
            {
                if (Analyzer.Analyze(str, out currPos, out sbForIdents, out sbForIntConsts, out sbForAnyConsts))
                {
                    tbOutputIdentsAndConsts.AppendText("Идентификаторы: " + newLine + sbForIdents.ToString() + newLine + "Целые константы: ");
                    if (sbForIntConsts.Length == 0)
                        tbOutputIdentsAndConsts.AppendText("отсутствуют." + newLine);
                    else
                        tbOutputIdentsAndConsts.AppendText(newLine + sbForIntConsts.ToString());

                    tbOutputIdentsAndConsts.AppendText(newLine + "Константы любые: ");
                    if (sbForAnyConsts.Length == 0)
                        tbOutputIdentsAndConsts.AppendText("отсутствуют.");
                    else
                        tbOutputIdentsAndConsts.AppendText(newLine + sbForAnyConsts.ToString());
                }
                else
                {
                    tbOutputMessage.Text = "Неверный оператор. Оператор имеет вид:" + newLine + "WITH <переменная> DO <оператор присваивания>;";
                }
            }
            catch (Exception e)
            {
                tbOutputMessage.Text = e.Message;
                tbInputStr.Focus();
                tbInputStr.SelectionStart = currPos;
            }
        }
    }
}

using System;
using System.Windows.Forms;
using System.IO;
using Morseee;
using System.Runtime.InteropServices;

namespace MorseCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;

                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        richTextBox1.Text = reader.ReadToEnd();
                    }
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            int count = 0;
            if ((!comboBox2.Items.Contains(comboBox2.Text)) || (!comboBox1.Items.Contains(comboBox1.Text)))
            {
                MessageBox.Show("Ошибка","Введите языки для перевода");
                return;
            }
            if ((comboBox2.Text == "RUS"&&comboBox1.Text=="ENG")||(comboBox2.Text=="ENG"&&comboBox1.Text=="RUS")) 
            {
                MessageBox.Show("Ну вообще-то это переводчик из морзы или в морзу","Ошибка");
                return;
            }
            if (comboBox2.Text == comboBox1.Text)
            {
                MessageBox.Show("А смысл?","Действительно");
                return;
            }
            if (comboBox1.Text == "Morse")
            {
                Morse lang = new Morse(comboBox2.Text);
                richTextBox2.Text = lang.Morz(richTextBox1.Text.Replace('ё','е').Replace('Ё','Е'),ref count);
                if (count!=0)
                MessageBox.Show($"Было удалено {count} символов","Некоторых символов нет в языке");
                return;
            }
            if (comboBox2.Text == "Morse")
            {
                Morse lang = new Morse(comboBox1.Text);
                richTextBox2.Text = lang.MorseReverse(richTextBox1.Text,ref count);
                if (count != 0)
                    MessageBox.Show("Присутствуют символы не из азбуки Морза","Ошибка");
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (richTextBox2.Text.Length > 0)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "text file|*.txt";
                saveFileDialog1.Title = "Save an Image File";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    File.WriteAllText(saveFileDialog1.FileName, richTextBox2.Text);
                }
            }
            else MessageBox.Show("Поле не должно быть пустым", "Ошибка");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ещё бы, я вам её 6 недель сдавал","Поставьте пять");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
                OpenFileDialog1.Filter = "text file|*.txt";
                OpenFileDialog1.Title = "Save an Image File";
                OpenFileDialog1.ShowDialog();
                if (OpenFileDialog1.FileName != "")
                {
                richTextBox1.Text=File.ReadAllText(OpenFileDialog1.FileName);
                }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string a;
            a = richTextBox1.Text;
            richTextBox1.Text = richTextBox2.Text;
            richTextBox2.Text = a;
            a = comboBox1.Text;
            comboBox1.Text = comboBox2.Text;
            comboBox2.Text = a;
            if ((comboBox1.Text != "Morse") && (comboBox2.Text != "Morse")) {
                MessageBox.Show("Тебе вообще нечего делать?","Мда");
            }
        }
    }
}

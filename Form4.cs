using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form4 : Form
    {
        String KEY;
        public Form4(String KEY1)
        {
            InitializeComponent();
          KEY=KEY1;
           
        }
        int count_error = 0; //счётчик ошибок
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == ""  ||  textBox3.Text == "") MessageBox.Show("Заполнены не все поля! Попробуйте ещё раз!");
            else
            {
                 String userpass = cryptoAES128.Encrypt(textBox1.Text,KEY);
                 StreamReader stream = new StreamReader(Form1.path, false);

                  String filepass = stream.ReadLine();
                  stream.Close();
             //   сравнение введённого пользователем пароля зашифрованного введённым пользователем ключём с зашифрованным паролем, хранящимся в файле
                 if (userpass != filepass)
               // if ( textBox1 != ((Form3)this.Parent).textBox1)
                {
                    // MessageBox.Show("Введёны неверная пара пароль-ключ! Попробуйте ещё!");
                    MessageBox.Show("Введён неверный пароль!");
                    count_error++;
                    if (count_error == 3)
                    {
                        MessageBox.Show("Закончились попытки ввести пароль! Попробуйте позже!");
                       this.Close();
                    }
                }
                else
                {
                    String key="";
                    Random rand = new Random();
                    for (int i = 0; i < 8; i++)
                    {

                        key += Form1.alphavit[rand.Next(0, 35)];
                    }

                    StreamWriter stream1 = new StreamWriter(Form1.path, false);



                    stream1.WriteLine(cryptoAES128.Encrypt(textBox3.Text, key));
                    stream1.Close();
                    MessageBox.Show("Пароль изменён! Пожалуйста сохраните ключ! Пароль перезашифрован и сохранён!");
                    label2.Hide();
                    label5.Hide();
                    label6.Hide();
                    textBox1.Hide();
                   
                    textBox3.Hide();
                    button1.Hide();

                    label1.Text = "Запомните "+Environment.NewLine+"новый "+ Environment.NewLine + "пароль!";
                    textBox2.Text = key;
                    textBox2.Show();

                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}

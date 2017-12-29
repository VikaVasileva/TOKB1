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
    public partial class Form1 : Form
    {
        public const String path="cryptopass.txt"; //путь к файлу
        public static String alphavit="ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"; //алфавит для генерации ключа
        public Form1()
        {
            InitializeComponent();
        }

        //вход в систему
        int count_error = 0; //счётчик ошибок
        private void button1_Click(object sender, EventArgs e)
        {
            String userpass = cryptoAES128.Encrypt(textBox1.Text, textBox2.Text);
            StreamReader stream = new StreamReader(Form1.path, false);

            String filepass = stream.ReadLine();
            stream.Close();
            //сравнение введённого пользователем пароля зашифрованного введённым пользователем ключём с зашифрованным паролем, хранящимся в файле
            if (userpass != filepass)
            {
                MessageBox.Show("Введёны неверная пара пароль-ключ! Попробуйте ещё!");
                count_error++;
                if (count_error == 3)
                {
                    MessageBox.Show("Закончились попытки ввести пароль! Система будет перезапущена!");
                    Application.Restart(); 
                }
            }
            else
            {
                MessageBox.Show("Вы успешно вошли в систему!");

                //автоматическая смена ключа
                textBox1.Hide();
                textBox2.ReadOnly = true;
                button1.Hide();
                label2.Hide();
                label3.Text = "Обязательно запомните новый ключ для Вашего пароля!";
                button2.Show();
                textBox2.Clear();
                button4.Show();
                Random rand = new Random();
                for (int i = 0; i < 8; i++)
                {

                    textBox2.Text += Form1.alphavit[rand.Next(0, 35)];
                }

                StreamWriter stream1 = new StreamWriter(Form1.path, false);

                stream1.WriteLine(cryptoAES128.Encrypt(textBox1.Text, textBox2.Text));
                stream1.Close();
                MessageBox.Show("Ключ автоматически изменён! Пожалуйста сохраните ключ! Пароль перезашифрован и сохранён!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //вход в систему
            button1.Show();
            button2.Hide();
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            button4.Hide();
          //  autoriz();
        }

        void autoriz()
        {
      //  metka:
            if (!File.Exists("cryptopass.txt"))
            {
           // Form2 f2 = new Form2();
          //  if (f2.ShowDialog() == DialogResult.Cancel)
         //   {
                Form3 f3 = new Form3();
                if (f3.ShowDialog() == DialogResult.Cancel)
                {
                    MessageBox.Show("Работа с программой без регистрации невозможна! Повторите попытку!");
                 //   goto metka;
                    return;
                }

          //  }
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            button1.Show();
            button2.Hide();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox1.ReadOnly = false;
            textBox1.Show();
            textBox2.ReadOnly = false;
            button4.Hide();
            label2.Show();
            label3.Text = "Введите ключ выданый системой!";
            MessageBox.Show("Успешный выход из системы!");
            //Application.Restart(); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4(textBox2.Text);
            Hide();
            f4.ShowDialog();
            Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}

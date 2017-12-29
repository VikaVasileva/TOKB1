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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        String key1;
        private void Form3_Load(object sender, EventArgs e)
        {
            if (File.Exists("cryptopass.txt"))
            {
                
                Form1 form1 = new Form1();
                this.Hide();
                form1.ShowDialog();
               
               
            }

                /* saveFileDialog1.Filter = "(*.txt)|*.txt";*/
                //генерация ключа
                key1 = "";
            Random rand = new Random();
            for (int i=0;i<8;i++)
            {

               key1 += Form1.alphavit[rand.Next(0, 35)];
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (textBox1.Text == "") MessageBox.Show("Пароль не введён! Повторите!");
            else
            {
               /* if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Не указан файл для хранения пароля! Попробуйте ещё раз!");
                }

                else
                {*/
                    
                    //Form1.path = saveFileDialog1.FileName;
                   // File.Delete(Form1.path);
                 //   File.Create(Form1.path);
                    StreamWriter stream = new StreamWriter(Form1.path,false);

                    stream.WriteLine(cryptoAES128.Encrypt(textBox1.Text, key1));
                    stream.Close();
                    MessageBox.Show("Пароль сохранён!");
                    this.DialogResult = DialogResult.OK;
                // }


                Form5 forma5 = new Form5(key1);
                this.Hide();
                forma5.Show();

            }



        }

       
    }
}

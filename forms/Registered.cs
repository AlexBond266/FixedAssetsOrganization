using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PR_TRPO.Scripts;
using MySql.Data.MySqlClient;

namespace PR_TRPO.Forms
{
    public partial class Reg : Form
    {
        public Reg()
        {
            InitializeComponent();
            bunifuTextBox1.Focus();
        }
        private void bunifuButton1_Click(object sender, EventArgs e) //Регистрация
        {
            string Kod_dostypa = "";
            try
            {
                Database db3 = new Database();
                DataTable table3 = new DataTable();
                MySqlDataAdapter adapter3 = new MySqlDataAdapter();
                MySqlCommand command3 = new MySqlCommand("SELECT * FROM `коддоступа`", db3.GetConnection());
                adapter3.SelectCommand = command3;
                adapter3.Fill(table3);
                foreach (DataRow item in table3.Rows)
                {
                    Kod_dostypa = item[1].ToString();
                }
            }
            catch
            {
                MessageBox.Show("Связь с базой данных не установлена. Пожалуйста, проверьте соединение с интернетом и перезапустите программу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (bunifuTextBox1.Text == "" || bunifuTextBox2.Text == "" || bunifuTextBox3.Text == "" || bunifuTextBox4.Text == "" || bunifuTextBox5.Text == "" || bunifuTextBox1.Text == null || bunifuTextBox2.Text == null || bunifuTextBox3.Text == null || bunifuTextBox4.Text == null || bunifuTextBox5.Text == null)
            {
                MessageBox.Show("Пожалуйста, введите все данные.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (bunifuTextBox1.Text == "")
                {
                    bunifuTextBox1.Focus();
                }
                else if (bunifuTextBox2.Text == "")
                {
                    bunifuTextBox2.Focus();
                }
                else if (bunifuTextBox3.Text == "")
                {
                    bunifuTextBox3.Focus();
                }
                else if (bunifuTextBox4.Text == "")
                {
                    bunifuTextBox4.Focus();
                }
                else if (bunifuTextBox5.Text == "")
                {
                    bunifuTextBox5.Focus();
                }
            }
            else if (bunifuTextBox4.TextLength < 4)
            {
                MessageBox.Show("Ваш пароль короче минимального. Пожалуйста, введите минимум 4 символа.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bunifuTextBox4.Focus();
            }
            else if (bunifuTextBox4.TextLength > 8)
            {
                MessageBox.Show("Ваш пароль длиннее максимального. Пожалуйста, введите максимум 8 символов.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bunifuTextBox4.Focus();
            }
            else if (bunifuTextBox5.Text != Kod_dostypa)
            {
                MessageBox.Show("Неверный код доступа!!!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bunifuTextBox5.Focus();
            }
            else
            {
                string name_sot = bunifuTextBox1.Text + " " + bunifuTextBox2.Text + " " + bunifuTextBox3.Text;
                string[] array = null;
                int a = 0;
                array = getUsers().Select(n => n.ToString()).ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    if (name_sot == array[i])
                    {
                        MessageBox.Show("Такой работник уже зарегистрирован.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bunifuTextBox1.Text = "";
                        bunifuTextBox2.Text = "";
                        bunifuTextBox3.Text = "";
                        bunifuTextBox1.Focus();
                        a = 1;
                        break;
                    }
                }
                if (a != 1)
                {
                    try
                    {
                        int stat_admin=0;
                        Database db = new Database();
                        MySqlCommand command = new MySqlCommand("INSERT INTO `авторизация` (`fio`, `password`, `admin`) VALUES('" + name_sot + "', '" + bunifuTextBox4.Text + "', " + stat_admin + ");", db.GetConnection());
                        db.openConnection();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Поздравляем! Вы зарегистрированы!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка регистрации! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);   
                        }
                        bunifuTextBox1.Text = "";
                        bunifuTextBox2.Text = "";
                        bunifuTextBox3.Text = "";
                        bunifuTextBox4.Text = "";
                        bunifuTextBox5.Text = "";
                        bunifuTextBox1.Focus();
                        db.closeConnection();
                    }
                    catch
                    {
                        MessageBox.Show("Связь с базой данных не установлена. Пожалуйста, проверьте соединение с интернетом и перезапустите программу.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        public List<string> getUsers()
        {
            List<string> opers = new List<string>();
            try
            {
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `авторизация`", db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                foreach (DataRow item in table.Rows)
                {
                    opers.Add(item[1].ToString());
                }
            }
            catch
            {
                MessageBox.Show("Связь с базой данных не установлена. Пожалуйста, проверьте соединение с интернетом и перезапустите программу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return opers;
        }
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Login l1 = new Login();
            l1.Show();
            this.Close();
        }
        char key;
        private void bunifuTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            key = e.KeyChar;
            e.Handled = true;
            if (Char.IsLetter(key) || Char.IsControl(key))
            {
                if (bunifuTextBox1.Text.Length == 0)
                {
                    e.KeyChar = char.ToUpper(e.KeyChar);
                }
                else
                {
                    e.KeyChar = char.ToLower(e.KeyChar);
                }
                e.Handled = false;
            }
        }

        private void bunifuTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            key = e.KeyChar;
            e.Handled = true;
            if (Char.IsLetter(key) || Char.IsControl(key))
            {
                if (bunifuTextBox2.Text.Length == 0)
                {
                    e.KeyChar = char.ToUpper(e.KeyChar);
                }
                else
                {
                    e.KeyChar = char.ToLower(e.KeyChar);
                }
                e.Handled = false;
            }
        }

        private void bunifuTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            key = e.KeyChar;
            e.Handled = true;
            if (Char.IsLetter(key) || Char.IsControl(key))
            {
                if (bunifuTextBox3.Text.Length == 0)
                {
                    e.KeyChar = char.ToUpper(e.KeyChar);
                }
                else
                {
                    e.KeyChar = char.ToLower(e.KeyChar);
                }
                e.Handled = false;
            }
        }
        const string Alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        int[] rand = new int[8];
        Random r1 = new Random();

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            bunifuTextBox4.Text = "";
            for (int i = 0; i < rand.Length; i++)
            {
                rand[i] = r1.Next(0, Alphabet.Length);
                bunifuTextBox4.Text += Alphabet[rand[i]];
            }
            bunifuButton1.Focus();
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            bunifuTextBox4.Text = "";
            bunifuTextBox4.Focus();
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            if (bunifuTextBox4.Text == "" || bunifuTextBox4.TextLength < 4 || bunifuTextBox4.TextLength > 8)
            {
                MessageBox.Show("Пароль не введён либо введено недопустимое колличество символов.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Clipboard.SetText("Пароль от учётной записи учёта ОС: "+bunifuTextBox4.Text);
                MessageBox.Show("Пароль скопирован в буфер обмена.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                bunifuButton1.Focus();
            }
        }
    }
}

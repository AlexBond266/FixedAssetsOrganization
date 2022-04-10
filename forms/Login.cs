using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using PR_TRPO.Scripts;
using MySql.Data.MySqlClient;

namespace PR_TRPO.Forms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            string[] array = getUsers().Select(n => n.ToString()).ToArray();
            bunifuDropdown1.Items.AddRange(array);
        }
        private void Login_Load(object sender, EventArgs e)
        {
            bunifuDropdown1.Text = null;
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
                bunifuButton1.Visible = false;
                bunifuButton2.Visible = false;
                MessageBox.Show("Связь с базой данных не установлена. Пожалуйста, проверьте соединение с интернетом и перезапустите программу.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return opers;
        }

        public void setpass(string name, string password) //Проверка пароля и переход на формы Пользователя и Администратора
        {
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM авторизация WHERE fio = '" + name + "' AND password = '" + password + "'", db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            string id = "0";
            int Stat_admin = 0;
            if (table.Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow item in table.Rows)
                    {
                        id = item[0].ToString();
                        Stat_admin = Convert.ToInt32(item[3]);
                    }
                }
                catch
                {
                    MessageBox.Show("Связь с базой данных не установлена. Пожалуйста, проверьте соединение с интернетом и перезапустите программу.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                db.Users = name;
                if (Stat_admin == 1)
                {
                    Form_Work_Admin f2 = new Form_Work_Admin();
                    f2.User = name;
                    f2.Show();
                }
                else
                {
                    Form_Work_User f1 = new Form_Work_User();
                    f1.User = name;
                    f1.Show();
                }
                bunifuDropdown1.Text = null;
            }
            else
            {
                MessageBox.Show("Пароль не верен.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите выйти?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result.ToString() == "Yes")
            {
                Application.Exit();
            }
        }
        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            string pass = "";
            if (bunifuDropdown1.Text == null)
            {
                MessageBox.Show("Пожалуйста, выберите сотрудника.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bunifuTextBox2.Text = "";
            }
            else
            {
                if (bunifuTextBox2.TextLength < 1)
                {
                    MessageBox.Show("Пожалуйста, введите пароль.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    pass = bunifuTextBox2.Text;
                    setpass(bunifuDropdown1.Text, pass);
                }
                bunifuTextBox2.Text = "";
            }
        }
        private void bunifuDropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bunifuTextBox2.Focus();
        }
        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            Reg r1 = new Reg();
            r1.Show();
            this.Close();
        }
    }
}
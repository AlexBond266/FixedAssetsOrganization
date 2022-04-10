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
using System.Data.OleDb;
using PR_TRPO.Forms;
using PR_TRPO.Scripts;
using Word = Microsoft.Office.Interop.Word;
using MySql.Data.MySqlClient;



namespace PR_TRPO.UsersControlers
{

    public partial class Avtorizacia : UserControl
    {
        Database db = new Database();
        public int pr = 0;
        List<string> editItem = new List<string>();
        public Avtorizacia()
        {
            InitializeComponent();
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(db.selectAvtorisaciaVse, db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            bunifuDataGridView1.DataSource = table.DefaultView;
            bunifuDataGridView1.Columns[0].Visible = false;
            bunifuDataGridView1.Columns[3].Visible = false;
        }

        private void bunifuButton1_Click(object sender, EventArgs e) //КНОПКА ПОВЫШЕНИЯ ДО АДМИНИСТРАТОРА
        {
            if (pr == 0 || pr == null)
            {
                MessageBox.Show("Не выбран сотрудник для повышения до администратора!", "Внимание!");
            }
            else
            {
                if (Convert.ToInt32(editItem[3])==1)
                {
                    MessageBox.Show("Сотрудник уже является администратором.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    Database db = new Database();
                    MySqlCommand command = new MySqlCommand("UPDATE `авторизация` SET `admin` = 1 WHERE `авторизация`.`id_эко` = " + editItem[0] + ";", db.GetConnection());
                    db.openConnection();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Сотрудник повышен до администратора.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        pr = 0;
                        if (bunifuCheckBox1.Checked == true)
                        {
                            queri = db.selectAvtorisaciaVse;
                        }
                        else if (bunifuCheckBox2.Checked == true)
                        {
                            queri = db.selectAvtorisaciaEko;
                        }
                        else queri = db.selectAvtorisaciaAdmin;
                        DataTable table2 = new DataTable();
                        MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                        MySqlCommand command2 = new MySqlCommand(queri, db.GetConnection());
                        adapter2.SelectCommand = command2;
                        adapter2.Fill(table2);
                        bunifuDataGridView1.DataSource = table2.DefaultView;
                        bunifuDataGridView1.Columns[0].Visible = false;
                        bunifuDataGridView1.Columns[3].Visible = false;
                        db.closeConnection();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        pr = 0;
                    }
                }
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)    //КНОПКА ИЗМЕНЕНИЯ
        {
            if (pr == 0 || pr == null)
            {
                MessageBox.Show("Не выбран элемент для изменения!", "Внимание!");
            }
            else
            {
                bunifuPages1.SelectedTab = tabPage3;
                groupBox1.Text = "Операция: Изменение данных";
                bunifuTextBox1.Focus();
                string[] mas = editItem[1].Split(' ');
                bunifuTextBox1.Text = mas[0];
                bunifuTextBox2.Text = mas[1];
                bunifuTextBox3.Text = mas[2];
                bunifuTextBox4.Text = editItem[2].ToString();
            }
        }
        private void bunifuButton3_Click(object sender, EventArgs e)  //УДАЛЕНИЕ
        {
            if (pr == 0 || pr == null)
            {
                MessageBox.Show("Не выбран сотрудник для удаления!", "Внимание!");
            }
            else
            {
                DialogResult result = MessageBox.Show($"Вы уверены, что хотите удалить сотрудника '"+ editItem[1] + "'?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Information); ;
                if (result.ToString() == "Yes")
                {
                    Database db = new Database();
                    MySqlCommand command = new MySqlCommand("DELETE FROM `авторизация` WHERE `авторизация`.`id_эко` = "+ editItem[0] + ";", db.GetConnection());
                    db.openConnection();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Сотрудник удалён.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        pr = 0;
                        if (bunifuCheckBox1.Checked == true)
                        {
                            queri = db.selectAvtorisaciaVse;
                        }
                        else if (bunifuCheckBox2.Checked == true)
                        {
                            queri = db.selectAvtorisaciaEko;
                        }
                        else queri = db.selectAvtorisaciaAdmin;
                        DataTable table2 = new DataTable();
                        MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                        MySqlCommand command2 = new MySqlCommand(queri, db.GetConnection());
                        adapter2.SelectCommand = command2;
                        adapter2.Fill(table2);
                        bunifuDataGridView1.DataSource = table2.DefaultView;
                        bunifuDataGridView1.Columns[0].Visible = false;
                        bunifuDataGridView1.Columns[3].Visible = false;
                        db.closeConnection();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка удаления! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        pr = 0;
                    }

                }   
            }
        }

        char key;

        private void bunifuDataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                pr = 1;
                editItem.Clear();
                for (int i = 0; i < bunifuDataGridView1.ColumnCount; i++)
                {
                    editItem.Add(bunifuDataGridView1.Rows[e.RowIndex].Cells[i].Value.ToString());
                }
            }
            catch
            {
                pr = 0;
            }
        }

        string queri = "";
        private void bunifuCheckBox1_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (bunifuCheckBox1.Checked == true)
            {
                bunifuCheckBox2.Checked = false; bunifuCheckBox3.Checked = false;
                queri = db.selectAvtorisaciaVse;
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand(queri, db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                bunifuDataGridView1.DataSource = table.DefaultView;
                bunifuDataGridView1.Columns[0].Visible = false;
                bunifuDataGridView1.Columns[3].Visible = false;
            }
        }

        private void bunifuCheckBox2_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (bunifuCheckBox2.Checked == true)
            {
                bunifuCheckBox1.Checked = false; bunifuCheckBox3.Checked = false;
                queri = db.selectAvtorisaciaEko;
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand(queri, db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                bunifuDataGridView1.DataSource = table.DefaultView;
                bunifuDataGridView1.Columns[0].Visible = false;
                bunifuDataGridView1.Columns[3].Visible = false;
            }
        }

        private void bunifuCheckBox3_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (bunifuCheckBox3.Checked == true)
            {
                bunifuCheckBox1.Checked = false; bunifuCheckBox2.Checked = false;
                queri = db.selectAvtorisaciaAdmin;
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand(queri, db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                bunifuDataGridView1.DataSource = table.DefaultView;
                bunifuDataGridView1.Columns[0].Visible = false;
                bunifuDataGridView1.Columns[3].Visible = false;
            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            if (bunifuTextBox1.Text == "" || bunifuTextBox2.Text == "" || bunifuTextBox3.Text == "" || bunifuTextBox4.Text == "" || bunifuTextBox1.Text == null || bunifuTextBox2.Text == null || bunifuTextBox3.Text == null || bunifuTextBox4.Text == null)
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
                        MessageBox.Show("Работник с такими данными уже зарегистрирован.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        int stat_admin = 0;
                        Database db = new Database();
                        MySqlCommand command = new MySqlCommand("UPDATE `авторизация` SET `fio` = '"+ name_sot + "', `password` = '"+bunifuTextBox4.Text+"' WHERE `авторизация`.`id_эко` = "+editItem[0]+";", db.GetConnection());
                        db.openConnection();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Данные изменены.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            bunifuTextBox1.Text = "";
                            bunifuTextBox2.Text = "";
                            bunifuTextBox3.Text = "";
                            bunifuTextBox4.Text = "";
                            bunifuTextBox1.Focus();
                            pr = 0;
                        }
                        else
                        {
                            MessageBox.Show("Ошибка регистрации! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            bunifuTextBox1.Text = "";
                            bunifuTextBox2.Text = "";
                            bunifuTextBox3.Text = "";
                            bunifuTextBox4.Text = "";
                            bunifuTextBox1.Focus();
                        }
                        
                        if (bunifuCheckBox1.Checked == true)
                        {
                            queri = db.selectAvtorisaciaVse;
                        }
                        else if (bunifuCheckBox2.Checked == true)
                        {
                            queri = db.selectAvtorisaciaEko;
                        }
                        else queri = db.selectAvtorisaciaAdmin;
                        DataTable table2 = new DataTable();
                        MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                        MySqlCommand command2 = new MySqlCommand(queri, db.GetConnection());
                        adapter2.SelectCommand = command2;
                        adapter2.Fill(table2);
                        bunifuDataGridView1.DataSource = table2.DefaultView;
                        bunifuDataGridView1.Columns[0].Visible = false;
                        bunifuDataGridView1.Columns[3].Visible = false;
                        db.closeConnection();
                        bunifuPages1.SelectedTab = tabPage0;
                        groupBox1.Text = "Операция:";
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
                MySqlCommand command = new MySqlCommand("SELECT * FROM `авторизация` WHERE `id_эко`!="+ editItem[0]+ ";", db.GetConnection());
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

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage4;
            groupBox1.Text = "Операция: Изменение кода доступа администратора";
            bunifuTextBox5.Focus();
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `коддоступа`;", db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            foreach (DataRow item in table.Rows)
            {
                bunifuTextBox5.Text=item[1].ToString();
            }
            bunifuButton5.Enabled = false;

        }

        private void bunifuTextBox5_TextChange(object sender, EventArgs e)
        {
            bunifuButton5.Enabled = true;
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            MySqlCommand command = new MySqlCommand("UPDATE `коддоступа` SET `коддоступа` = '" + bunifuTextBox5.Text + "' WHERE `id_коддоступа` = 1;", db.GetConnection());
            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Код доступа изменён.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                bunifuPages1.SelectedTab = tabPage0;
                bunifuTextBox5.Text = "";
                groupBox1.Text = "Операция:";

            }
            else
            {
                MessageBox.Show("Ошибка изменения кода доступа! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}














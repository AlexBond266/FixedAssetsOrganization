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

    public partial class MOL : UserControl
    {
        Database db = new Database();
        public int pr = 0;
        List<string> editItem = new List<string>();
        public MOL()
        {
            InitializeComponent();
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(db.selectMOL, db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            bunifuDataGridView1.DataSource = table.DefaultView;
            bunifuDataGridView1.Columns[0].Visible = false;
            bunifuDataGridView1.Columns[1].Visible = false;

            string[] Dol = getDol().Select(n => n.ToString()).ToArray();
            bunifuDropdown1.Items.AddRange(Dol);
            bunifuDropdown2.Items.AddRange(Dol);
            bunifuDropdown4.Items.AddRange(Dol);
        }
        public List<string> getDol()
        {
            List<string> opers = new List<string>();
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `должности`", db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            foreach (DataRow item in table.Rows)
            {
                opers.Add(item[1].ToString());
            }
            return opers;
        }
        private void bunifuButton1_Click(object sender, EventArgs e) //ПОИСК
        {
            bunifuPages1.SelectedTab = tabPage5;
            groupBox1.Text = "Операция: Поиск";
            bunifuTextBox15.Focus();
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
                bunifuTextBox11.Focus();
                bunifuTextBox11.Text = editItem[2];
                bunifuTextBox12.Text = editItem[3];
                bunifuTextBox13.Text = editItem[4];
                bunifuDropdown2.Text = editItem[5];
                if (bunifuDropdown2.Text=="Директор")
                {
                    bunifuDropdown2.Enabled = false;
                }
                else bunifuDropdown2.Enabled = true;
            }
        }
        private void bunifuButton3_Click(object sender, EventArgs e)  //УДАЛЕНИЕ
        {
            if (pr == 0 || pr == null)
            {
                MessageBox.Show("Не выбрано МОЛ для удаления!", "Внимание!");
            }
            else
            {
                string name_sot = editItem[2] + " " + editItem[3] + " " + editItem[4];
                DialogResult result = MessageBox.Show($"Вы уверены, что хотите удалить сотрудника '" + name_sot + "'?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Information); ;
                if (result.ToString() == "Yes")
                {
                    Database db = new Database();
                    MySqlCommand command = new MySqlCommand("DELETE FROM `мол` WHERE `мол`.`id_мол` = " + editItem[0] + ";", db.GetConnection());
                    db.openConnection();
                    if (command.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Материально ответственное лицо удалёно.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        MessageBox.Show("Ответственность за ОС, закреплённые за материально ответственным лицом '" + name_sot + "' перекладывается на директора организации.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pr = 0;
                        Database db2 = new Database();
                        DataTable table2 = new DataTable();
                        MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                        MySqlCommand command2 = new MySqlCommand(db2.selectMOL, db2.GetConnection());
                        adapter2.SelectCommand = command2;
                        adapter2.Fill(table2);
                        bunifuDataGridView1.DataSource = table2.DefaultView;
                        bunifuDataGridView1.Columns[0].Visible = false;
                        bunifuDataGridView1.Columns[1].Visible = false;
                        db.closeConnection(); db2.closeConnection();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка удаления! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        pr = 0;
                        db.closeConnection();
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

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            if (bunifuTextBox11.Text == "" || bunifuTextBox12.Text == "" || bunifuTextBox13.Text == "" || bunifuDropdown2.Text == "" || bunifuTextBox11.Text == null || bunifuTextBox12.Text == null || bunifuTextBox13.Text == null || bunifuDropdown2.Text == null)
            {
                MessageBox.Show("Пожалуйста, введите все данные.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (bunifuTextBox11.Text == "")
                {
                    bunifuTextBox11.Focus();
                }
                else if (bunifuTextBox12.Text == "")
                {
                    bunifuTextBox12.Focus();
                }
                else if (bunifuTextBox13.Text == "")
                {
                    bunifuTextBox13.Focus();
                }
            }
            else
            {
                string name_sot = bunifuTextBox11.Text + " " + bunifuTextBox12.Text + " " + bunifuTextBox13.Text;
                string[] array = null;
                int a = 0;
                array = getUsers2().Select(n => n.ToString()).ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    if (name_sot == array[i])
                    {
                        MessageBox.Show("Работник с такими данными уже зарегистрирован.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bunifuTextBox11.Text = "";
                        bunifuTextBox12.Text = "";
                        bunifuTextBox13.Text = "";
                        bunifuDropdown2.Text = "";
                        bunifuTextBox11.Focus();
                        a = 1;
                        break;
                    }
                }
                if (a != 1)
                {
                    try
                    {
                        int id_dol = 0;
                        Database db3 = new Database();
                        db3.openConnection();
                        DataTable table3 = new DataTable();
                        MySqlDataAdapter adapter3 = new MySqlDataAdapter();
                        MySqlCommand command3 = new MySqlCommand("SELECT `id_должности` FROM `должности` WHERE `наименование`='" + bunifuDropdown2.Text + "';", db3.GetConnection());
                        adapter3.SelectCommand = command3;
                        adapter3.Fill(table3);
                        foreach (DataRow item in table3.Rows)
                        {
                            id_dol = Convert.ToInt32(item[0]);
                        }

                        int stat_admin = 0;
                        Database db = new Database();
                        MySqlCommand command = new MySqlCommand("UPDATE `мол` SET `id_должности` = '"+id_dol+"', `ф` = '"+bunifuTextBox11.Text+ "', `и` = '" + bunifuTextBox12.Text + "', `о` = '" + bunifuTextBox13.Text + "' WHERE `мол`.`id_мол` = "+editItem[0]+";", db.GetConnection());
                        db.openConnection();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Данные о материально ответственном лице изменены.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            bunifuTextBox11.Text = "";
                            bunifuTextBox12.Text = "";
                            bunifuTextBox13.Text = "";
                            bunifuDropdown2.Text = "";
                            bunifuTextBox11.Focus();
                            pr = 0;
                        }
                        else
                        {
                            MessageBox.Show("Ошибка изменения данных о МОЛ! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            bunifuTextBox11.Text = "";
                            bunifuTextBox12.Text = "";
                            bunifuTextBox13.Text = "";
                            bunifuDropdown2.Text = "";
                            bunifuTextBox11.Focus();
                        }
                        db.closeConnection();
                        Database db2 = new Database();
                        DataTable table2 = new DataTable();
                        MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                        MySqlCommand command2 = new MySqlCommand(db2.selectMOL, db2.GetConnection());
                        adapter2.SelectCommand = command2;
                        adapter2.Fill(table2);
                        bunifuDataGridView1.DataSource = table2.DefaultView;
                        bunifuDataGridView1.Columns[0].Visible = false;
                        bunifuDataGridView1.Columns[1].Visible = false;
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
                MySqlCommand command = new MySqlCommand("SELECT * FROM `мол`", db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                foreach (DataRow item in table.Rows)
                {
                    opers.Add(item[2].ToString()+" "+ item[3].ToString()+" "+ item[4].ToString());
                }
            }
            catch
            {
                MessageBox.Show("Связь с базой данных не установлена. Пожалуйста, проверьте соединение с интернетом и перезапустите программу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return opers;
        }
        public List<string> getUsers2()
        {
            List<string> opers = new List<string>();
            try
            {
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `мол` WHERE `id_мол`!='"+editItem[0]+"';", db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                foreach (DataRow item in table.Rows)
                {
                    opers.Add(item[2].ToString() + " " + item[3].ToString() + " " + item[4].ToString());
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
                if (bunifuTextBox11.Text.Length == 0)
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
                if (bunifuTextBox12.Text.Length == 0)
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
                if (bunifuTextBox13.Text.Length == 0)
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
            groupBox1.Text = "Операция: Фильтрация по должности";
        }

        private void bunifuTextBox5_TextChange(object sender, EventArgs e)
        {
            bunifuButton5.Enabled = true;
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            if (bunifuDropdown4.Text == "" || bunifuDropdown4.Text == null)
            {
                MessageBox.Show("Пожалуйста, выберите должность материально ответственного лица.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Database db3 = new Database();
                db3.openConnection();
                DataTable table3 = new DataTable();
                MySqlDataAdapter adapter3 = new MySqlDataAdapter();
                string queri = queri = "SELECT `id_мол`, `мол`.`id_должности`, `ф` AS `Фамилия`, `и` AS `Имя`, `о` AS `Отчество`, `должности`.`наименование` AS `Должность` FROM `мол` INNER JOIN `должности` on `мол`.`id_должности`=`должности`.`id_должности` WHERE `должности`.`наименование`='" + bunifuDropdown4.Text + "';";
                try
                {
                    MySqlCommand command3 = new MySqlCommand(queri, db3.GetConnection());
                    adapter3.SelectCommand = command3;
                    adapter3.Fill(table3);
                    bunifuDataGridView1.DataSource = table3.DefaultView;
                    bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = false;
                    db3.closeConnection();
                }
                catch
                {
                    MessageBox.Show("Запрос не обработан.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage1;
            groupBox1.Text = "Операция: Добавление МОЛ";
            bunifuTextBox1.Focus();
        }

        private void bunifuTextBox15_KeyUp(object sender, KeyEventArgs e)
        {
            for (int i = 0; i < bunifuDataGridView1.RowCount; i++)
            {
                bunifuDataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < bunifuDataGridView1.ColumnCount; j++)
                    if (bunifuDataGridView1.Rows[i].Cells[j].Value != null)
                        if (bunifuDataGridView1.Rows[i].Cells[j].Value.ToString().ToLower().Contains(bunifuTextBox15.Text.ToLower()))
                        {
                            bunifuDataGridView1.Rows[i].Selected = true;
                            break;
                        }
            }
            if (bunifuTextBox15.Text == "")
            {
                bunifuDataGridView1.ClearSelection();
            }
        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            if (bunifuTextBox1.Text == "" || bunifuTextBox2.Text == "" || bunifuTextBox3.Text == "" || bunifuDropdown1.Text == "" || bunifuTextBox1.Text == null || bunifuTextBox2.Text == null || bunifuTextBox3.Text == null || bunifuDropdown1.Text == null)
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
            }
            else if (bunifuDropdown1.Text == "Директор")
            {
                MessageBox.Show("Нельзя назначить сотрудника на выбранную должность.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bunifuDropdown1.Text = "";
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
                        bunifuDropdown1.Text = "";
                        bunifuTextBox1.Focus();
                        a = 1;
                        break;
                    }
                }
                if (a != 1)
                {
                    try
                    {
                        int id_dol = 0;
                        Database db3 = new Database();
                        db3.openConnection();
                        DataTable table3 = new DataTable();
                        MySqlDataAdapter adapter3 = new MySqlDataAdapter();
                        MySqlCommand command3 = new MySqlCommand("SELECT `id_должности` FROM `должности` WHERE `наименование`='" + bunifuDropdown1.Text + "';", db3.GetConnection());
                        adapter3.SelectCommand = command3;
                        adapter3.Fill(table3);
                        foreach (DataRow item in table3.Rows)
                        {
                            id_dol = Convert.ToInt32(item[0]);
                        }

                        int stat_admin = 0;
                        Database db = new Database();
                        MySqlCommand command = new MySqlCommand("INSERT INTO `мол` (`id_должности`, `ф`, `и`, `о`) VALUES ('"+id_dol+"', '"+ bunifuTextBox1.Text + "', '"+ bunifuTextBox2.Text + "', '"+ bunifuTextBox3.Text + "');", db.GetConnection());
                        db.openConnection();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Материально ответственное лицо добавлено.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            bunifuTextBox1.Text = "";
                            bunifuTextBox2.Text = "";
                            bunifuTextBox3.Text = "";
                            bunifuDropdown1.Text = "";
                            bunifuTextBox1.Focus();
                            pr = 0;
                        }
                        else
                        {
                            MessageBox.Show("Ошибка добавления МОЛ! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            bunifuTextBox1.Text = "";
                            bunifuTextBox2.Text = "";
                            bunifuTextBox3.Text = "";
                            bunifuDropdown1.Text = "";
                            bunifuTextBox1.Focus();
                        }
                        db.closeConnection();
                        Database db2 = new Database();
                        DataTable table2 = new DataTable();
                        MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                        MySqlCommand command2 = new MySqlCommand(db2.selectMOL, db2.GetConnection());
                        adapter2.SelectCommand = command2;
                        adapter2.Fill(table2);
                        bunifuDataGridView1.DataSource = table2.DefaultView;
                        bunifuDataGridView1.Columns[0].Visible = false;
                        bunifuDataGridView1.Columns[1].Visible = false;
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
    }
}














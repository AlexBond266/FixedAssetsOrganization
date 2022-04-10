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
using PR_TRPO.Forms;
using PR_TRPO.Scripts;
using MySql.Data.MySqlClient;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Reflection;

namespace PR_TRPO.UsersControlers
{
    public partial class RemontOS : UserControl
    {
        Database db = new Database();
        List<string> editItem = new List<string>();
        public int pr = 0, pr1 = 0;
        DateTime d = DateTime.Now;
        public RemontOS()
        {
            InitializeComponent();
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(db.selectREM_OS, db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            bunifuDataGridView1.DataSource = table.DefaultView;
            bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = false; bunifuDataGridView1.Columns[2].Visible = false;

            bunifuDatePicker1.Value = d;
            bunifuDatePicker1.MaxDate = d;
            bunifuDatePicker2.Value = d;
            bunifuDatePicker2.MaxDate = d;
            bunifuDatePicker3.Value = d;
            bunifuDatePicker3.MaxDate = d;
            bunifuDatePicker4.Value = d;
            bunifuDatePicker4.MaxDate = d;
            bunifuDatePicker5.Value = d;
            bunifuDatePicker5.MaxDate = d;
            bunifuDatePicker6.Value = d;
            bunifuDatePicker6.MaxDate = d;
            bunifuDatePicker7.Value = d;
            bunifuDatePicker7.MaxDate = d;
            bunifuDatePicker8.Value = d;
            bunifuDatePicker8.MaxDate = d;
            bunifuDatePicker9.Value = d;
            bunifuDatePicker9.MaxDate = d;

            string[] arrayVid_Rem = getVid_Rem().Select(n => n.ToString()).ToArray();
            bunifuDropdown1.Items.AddRange(arrayVid_Rem);
            bunifuDropdown2.Items.AddRange(arrayVid_Rem);
            bunifuDropdown4.Items.AddRange(arrayVid_Rem);
        }

        private List<string> getVid_Rem()
        {
            List<string> opers = new List<string>();
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `вид_р`", db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            foreach (DataRow item in table.Rows)
            {
                opers.Add(item[1].ToString());
            }
            return opers;
        }

        private void bunifuCheckBox1_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (bunifuCheckBox1.Checked == true)
            {
                bunifuCheckBox2.Checked = false; bunifuCheckBox3.Checked = false;
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand(db.selectREM_OS, db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                bunifuDataGridView1.DataSource = table.DefaultView;
                bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = false; bunifuDataGridView1.Columns[2].Visible = false;
            }
        }

        private void bunifuCheckBox2_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (bunifuCheckBox2.Checked == true)
            {
                bunifuCheckBox1.Checked = false; bunifuCheckBox3.Checked = false;
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand(db.selectRemU, db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                bunifuDataGridView1.DataSource = table.DefaultView;
                bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = false; bunifuDataGridView1.Columns[2].Visible = false;
            }
        }

        private void bunifuCheckBox3_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (bunifuCheckBox3.Checked == true)
            {
                bunifuCheckBox1.Checked = false; bunifuCheckBox2.Checked = false;
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand(db.selectRemS, db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                bunifuDataGridView1.DataSource = table.DefaultView;
                bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = false; bunifuDataGridView1.Columns[2].Visible = false;
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage2;
            groupBox1.Text = "Операция: Поиск";
            bunifuTextBox15.Focus();
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

        private void bunifuDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                pr = 1;
                editItem.Clear();
                for (int i = 0; i < bunifuDataGridView1.ColumnCount; i++)
                {
                    editItem.Add(bunifuDataGridView1.Rows[e.RowIndex].Cells[i].Value.ToString());
                }
                if (bunifuPages1.SelectedTab == tabPage4)
                {
                    Database db = new Database();
                    DataTable table = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand command = new MySqlCommand("SELECT `id_р`, `ремонт`.`id_ос`, `ремонт`.`id_вида_р`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_р`.`наименование` AS `Вид ремонта`, `дата_начала` AS `Дата начала ремонта`, `дата_окончания` AS `Дата окончания ремонта`, `сум_затрат` AS `Сумма затрат на ремонт: руб.` FROM `ремонт` INNER JOIN `ос` ON `ремонт`.`id_ос`=`ос`.`id_ос` INNER JOIN `вид_р` ON `ремонт`.`id_вида_р`=`вид_р`.`id_вида_р` WHERE `ремонт`.`id_р`=" + editItem[0] + ";", db.GetConnection());
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                    bunifuDataGridView3.DataSource = table.DefaultView;
                    bunifuDataGridView3.Columns[0].Visible = false; bunifuDataGridView3.Columns[1].Visible = false; bunifuDataGridView3.Columns[2].Visible = false;
                }
            }
            catch
            {
                pr = 0;
            }
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage4;
            groupBox1.Text = "Операция: Акт ремонта";
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage1;
            groupBox1.Text = "Операция: Новый ремонт";
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT `id_ос`, `наименование` as `Наименование ОС`, `ин` AS `Инвентарный номер`, `дата_принятия` FROM `ос` WHERE `статус`=1", db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            bunifuDataGridView2.DataSource = table.DefaultView;
            bunifuDataGridView2.Columns[0].Visible = false; bunifuDataGridView2.Columns[3].Visible = false;

        }

        private void bunifuDataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                pr = 1;
                editItem.Clear();
                for (int i = 0; i < bunifuDataGridView2.ColumnCount; i++)
                {
                    editItem.Add(bunifuDataGridView2.Rows[e.RowIndex].Cells[i].Value.ToString());
                }
                bunifuTextBox1.Text = editItem[1];
                bunifuLabel1.Text = editItem[2].ToString();
                bunifuDatePicker1.MinDate = Convert.ToDateTime(editItem[3]);
            }
            catch
            {
                pr = 0;
            }
        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            if (bunifuTextBox1.Text == "" || bunifuTextBox1.Text == null)
            {
                MessageBox.Show("Не выбрано основное средство для ремонта!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bunifuTextBox1.Focus();
            }
            else if (bunifuDropdown1.Text == "" || bunifuDropdown1.Text == null)
            {
                MessageBox.Show("Не выбран вид ремонта!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Database db1 = new Database();
                DataTable table1 = new DataTable();
                MySqlDataAdapter adapter1 = new MySqlDataAdapter();
                MySqlCommand command1 = new MySqlCommand("SELECT * FROM `вид_р`", db1.GetConnection());
                adapter1.SelectCommand = command1;
                adapter1.Fill(table1);
                string[] vid = new string[1];
                int id_vid = 0;
                foreach (DataRow item in table1.Rows)
                {
                    vid[0] = item[1].ToString();
                    if (bunifuDropdown1.Text == vid[0])
                    {
                        id_vid = Convert.ToInt32(item[0]);
                        break;
                    }
                }
                DateTime d = bunifuDatePicker1.Value;
                string dat = $"{d.Year}-{d.Month}-{d.Day}";
                MySqlCommand command5 = new MySqlCommand("SELECT * FROM `ремонт` WHERE `id_ос`=" + editItem[0] + " AND `дата_окончания` IS null;", db.GetConnection());
                db.openConnection();
                if (Convert.ToInt32(command5.ExecuteScalar()) > 0)
                {
                    MessageBox.Show("Основное средство на данный момент уже подвергнуто ремонту.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bunifuTextBox1.Text = ""; bunifuLabel1.Text = ""; bunifuDatePicker1.Value = d; bunifuDropdown1.Text = "";
                }
                else
                {
                    MySqlCommand command4 = new MySqlCommand("INSERT INTO `ремонт` (`id_ос`, `id_вида_р`, `дата_начала`, `дата_окончания`, `сум_затрат`) VALUES ('" + editItem[0] + "', '" + id_vid + "', '" + dat + "', NULL, NULL);", db.GetConnection());
                    db.openConnection();
                    if (command4.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Основное средство поступило на ремонт.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string queri = "";
                        if (bunifuCheckBox1.Checked == true)
                        {
                            queri = db.selectREM_OS;
                        }
                        else if (bunifuCheckBox2.Checked == true)
                        {
                            queri = db.selectRemU;
                        }
                        else
                        {
                            queri = db.selectRemS;
                        }
                        Database db3 = new Database();
                        DataTable table3 = new DataTable();
                        MySqlDataAdapter adapter3 = new MySqlDataAdapter();
                        MySqlCommand command3 = new MySqlCommand(queri, db3.GetConnection());
                        adapter3.SelectCommand = command3;
                        adapter3.Fill(table3);
                        bunifuDataGridView1.DataSource = table3.DefaultView;
                        bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = false; bunifuDataGridView1.Columns[2].Visible = false;
                        db.closeConnection();
                        bunifuTextBox1.Text = ""; bunifuLabel1.Text = ""; bunifuDatePicker1.Value = d; bunifuDropdown1.Text = "";
                        bunifuPages1.SelectedTab = tabPage0;
                        groupBox1.Text = "Операция:";
                        pr = 0;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка поступления ОС на ремонт! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (pr == 0 || pr == null)
                {
                    MessageBox.Show("Не выбрана запись для окончания ремонта!", "Внимание!");
                }
                else if (editItem[8] != "")
                {
                    MessageBox.Show("Данный ремонт уже окончен!", "Внимание!");
                }
                else
                {
                    bunifuPages1.SelectedTab = tabPage9;
                    groupBox1.Text = "Операция: Окончание ремонта";
                    bunifuTextBox2.Text = editItem[3];
                    bunifuTextBox6.Text = editItem[5];
                    bunifuLabel7.Text = editItem[4].ToString();
                    bunifuDatePicker2.Value = Convert.ToDateTime(editItem[6]);
                    bunifuDatePicker3.MinDate = bunifuDatePicker2.Value;
                }
            }
            catch { int a = 0; }//тупо что-то для работы

        }

        private void bunifuTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;
            e.Handled = true;
            if (Char.IsDigit(key) || Char.IsControl(key) || key == ',')
            {
                if (bunifuTextBox4.Text.Length == 0 && (key == '0' || key == ','))
                {
                    e.Handled = true;
                }
                else if (key == ',' && bunifuTextBox4.Text[bunifuTextBox4.Text.Length - 1] == ',')
                {
                    e.Handled = true;
                }
                else if (Char.IsControl(key))
                {
                    e.Handled = false;
                }
                else if (bunifuTextBox4.Text.Contains(',') && key == ',')
                {
                    e.Handled = false;
                }
                else if (bunifuTextBox4.Text.Length >= 4 && bunifuTextBox4.Text[bunifuTextBox4.Text.Length - 3] == ',')
                {
                    e.Handled = true;
                }
                else e.Handled = false;
            }
        }
        string sum_zat = "";
        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            if (bunifuTextBox4.Text == "" || bunifuTextBox4.Text == null)
            {
                MessageBox.Show("Введите сумму затрат на ремонт.", "Внимание!");
                bunifuTextBox4.Focus();
            }
            else
            {
                try
                {
                    sum_zat = Convert.ToString(Math.Round(Convert.ToDouble(bunifuTextBox4.Text), 2)); sum_zat = sum_zat.Replace(',', '.');
                }
                catch { MessageBox.Show("Проверьте корректность введённой суммы.", "Внимание!"); bunifuTextBox4.Focus(); }
                DateTime d = bunifuDatePicker3.Value;
                string dat = $"{d.Year}-{d.Month}-{d.Day}";
                MySqlCommand command4 = new MySqlCommand("UPDATE `ремонт` SET `дата_окончания` = '" + dat + "', `сум_затрат` = '" + sum_zat + "' WHERE `ремонт`.`id_р` = " + editItem[0] + ";", db.GetConnection());
                db.openConnection();
                if (command4.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Ремонт окончен.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string queri = "";
                    if (bunifuCheckBox1.Checked == true)
                    {
                        queri = db.selectREM_OS;
                    }
                    else if (bunifuCheckBox2.Checked == true)
                    {
                        queri = db.selectRemU;
                    }
                    else
                    {
                        queri = db.selectRemS;
                    }
                    Database db3 = new Database();
                    DataTable table3 = new DataTable();
                    MySqlDataAdapter adapter3 = new MySqlDataAdapter();
                    MySqlCommand command3 = new MySqlCommand(queri, db3.GetConnection());
                    adapter3.SelectCommand = command3;
                    adapter3.Fill(table3);
                    bunifuDataGridView1.DataSource = table3.DefaultView;
                    bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = false; bunifuDataGridView1.Columns[2].Visible = false;
                    db.closeConnection();
                    bunifuTextBox4.Text = ""; bunifuDatePicker3.Value = d;
                    bunifuPages1.SelectedTab = tabPage0;
                    groupBox1.Text = "Операция:";
                }
                else
                {
                    MessageBox.Show("Ошибка поступления ОС на ремонт! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (pr == 0 || pr == null)
                {
                    MessageBox.Show("Не выбрана запись для изменения!", "Внимание!");
                }
                else if (editItem[8] == "")
                {
                    MessageBox.Show("Данный ремонт ещё не окончен! Изменение данных доступно только после окончания ремонта.", "Внимание!");
                }
                else
                {
                    bunifuPages1.SelectedTab = tabPage10;
                    groupBox1.Text = "Операция: Изменение данных";
                    bunifuTextBox5.Text = editItem[3];
                    bunifuDropdown2.Text = editItem[5];
                    bunifuLabel18.Text = editItem[4].ToString();
                    bunifuDatePicker4.Value = Convert.ToDateTime(editItem[6]);
                    bunifuDatePicker5.Value = Convert.ToDateTime(editItem[7]);
                    bunifuDatePicker4.MaxDate = bunifuDatePicker5.Value;
                    bunifuDatePicker5.MinDate = bunifuDatePicker4.Value;
                    bunifuTextBox3.Text = editItem[8];
                    Database db = new Database();
                    DataTable table = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand command = new MySqlCommand("SELECT `дата_принятия` FROM `ос` WHERE `id_ос` = " + editItem[1] + ";", db.GetConnection());
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                    foreach (DataRow item in table.Rows)
                    {
                        bunifuDatePicker4.MinDate = Convert.ToDateTime(item[0]);
                    }
                }
            }
            catch { int a = 0; }//тупо что-то для работы
        }

        private void bunifuTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;
            e.Handled = true;
            if (Char.IsDigit(key) || Char.IsControl(key) || key == ',')
            {
                if (bunifuTextBox3.Text.Length == 0 && (key == '0' || key == ','))
                {
                    e.Handled = true;
                }
                else if (key == ',' && bunifuTextBox3.Text[bunifuTextBox3.Text.Length - 1] == ',')
                {
                    e.Handled = true;
                }
                else if (Char.IsControl(key))
                {
                    e.Handled = false;
                }
                else if (bunifuTextBox3.Text.Contains(',') && key == ',')
                {
                    e.Handled = false;
                }
                else if (bunifuTextBox3.Text.Length >= 4 && bunifuTextBox3.Text[bunifuTextBox3.Text.Length - 3] == ',')
                {
                    e.Handled = true;
                }
                else e.Handled = false;
            }
        }

        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            if (bunifuTextBox3.Text == "" || bunifuTextBox3.Text == null)
            {
                MessageBox.Show("Введите сумму затрат на ремонт.", "Внимание!");
                bunifuTextBox3.Focus();
            }
            else
            {
                string querie = $"SELECT `id_вида_р` FROM `вид_р` WHERE `наименование`='{bunifuDropdown2.Text}';";
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand(querie, db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                int ID_Vid = 0;
                foreach (DataRow item in table.Rows)
                {
                    ID_Vid = Convert.ToInt32(item[0]);
                }
                try
                {
                    sum_zat = Convert.ToString(Math.Round(Convert.ToDouble(bunifuTextBox3.Text), 2)); sum_zat = sum_zat.Replace(',', '.');
                }
                catch { MessageBox.Show("Проверьте корректность введённой суммы.", "Внимание!"); bunifuTextBox4.Focus(); }
                DateTime d = bunifuDatePicker4.Value;
                string dat1 = $"{d.Year}-{d.Month}-{d.Day}";
                d = bunifuDatePicker5.Value;
                string dat2 = $"{d.Year}-{d.Month}-{d.Day}";
                MySqlCommand command4 = new MySqlCommand("UPDATE `ремонт` SET `id_вида_р` = '" + ID_Vid + "', `дата_начала` = '" + dat1 + "', `дата_окончания` = '" + dat2 + "', `сум_затрат` = '" + sum_zat + "' WHERE `ремонт`.`id_р` = " + editItem[0] + ";", db.GetConnection());
                db.openConnection();
                if (command4.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Данные изменены.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string queri = "";
                    if (bunifuCheckBox1.Checked == true)
                    {
                        queri = db.selectREM_OS;
                    }
                    else if (bunifuCheckBox2.Checked == true)
                    {
                        queri = db.selectRemU;
                    }
                    else
                    {
                        queri = db.selectRemS;
                    }
                    Database db3 = new Database();
                    DataTable table3 = new DataTable();
                    MySqlDataAdapter adapter3 = new MySqlDataAdapter();
                    MySqlCommand command3 = new MySqlCommand(queri, db3.GetConnection());
                    adapter3.SelectCommand = command3;
                    adapter3.Fill(table3);
                    bunifuDataGridView1.DataSource = table3.DefaultView;
                    bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = false; bunifuDataGridView1.Columns[2].Visible = false;
                    db.closeConnection();
                    bunifuTextBox4.Text = ""; bunifuDatePicker3.Value = d;
                    bunifuPages1.SelectedTab = tabPage0;
                    groupBox1.Text = "Операция:";
                    pr = 0;
                }
                else
                {
                    MessageBox.Show("Ошибка изменения данных! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage3;
            groupBox1.Text = "Операция: Фильтрация";
        }

        private void bunifuDropdown12_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (bunifuDropdown12.Text)
            {
                case "основному средству": {
                        bunifuPages2.SelectedTab = tabPage6;
                        Database db = new Database();
                        DataTable table = new DataTable();
                        MySqlDataAdapter adapter = new MySqlDataAdapter();
                        MySqlCommand command = new MySqlCommand("SELECT `id_ос`, `наименование` as `Наименование ОС`, `ин` AS `Инвентарный номер`, `дата_принятия` AS `Дата принятия` FROM `ос`", db.GetConnection());
                        adapter.SelectCommand = command;
                        adapter.Fill(table);
                        bunifuDataGridView4.DataSource = table.DefaultView;
                        bunifuDataGridView4.Columns[0].Visible = false;
                        break; }
                case "виду ремонта": { bunifuPages2.SelectedTab = tabPage7; break; }
                case "дате начала ремонта": { bunifuPages2.SelectedTab = tabPage8; break; }
                case "дате окончания ремонта": { bunifuPages2.SelectedTab = tabPage11; break; }
            }
        }

        private void bunifuButton10_Click(object sender, EventArgs e)
        {
            int id_vid_os = 0;
            if (bunifuDropdown12.Text == "" || bunifuDropdown12.Text == null)
            {
                MessageBox.Show("Пожалуйста, выберите критерий фильтрации.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (bunifuDropdown12.Text == "основному средству" && pr1 == 0)
            {
                MessageBox.Show("Пожалуйста, выберите основное средство.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (bunifuDropdown12.Text == "виду ремонта" && (bunifuDropdown4.Text == "" || bunifuDropdown4.Text == null))
            {
                MessageBox.Show("Пожалуйста, выберите вид ремонта.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                try
                {
                    int ID_OS = Convert.ToInt32(editItem[0]);
                }
                catch { int a = 0; }

                DateTime d = bunifuDatePicker6.Value;
                string dat_start1 = $"{d.Year}-{d.Month}-{d.Day}";
                d = bunifuDatePicker7.Value;
                string dat_end1 = $"{d.Year}-{d.Month}-{d.Day}";
                d = bunifuDatePicker8.Value;
                string dat_start2 = $"{d.Year}-{d.Month}-{d.Day}";
                d = bunifuDatePicker9.Value;
                string dat_end2 = $"{d.Year}-{d.Month}-{d.Day}";
                string queri = "";
                Database db3 = new Database();
                DataTable table3 = new DataTable();
                MySqlDataAdapter adapter3 = new MySqlDataAdapter();
                if (bunifuCheckBox1.Checked == true)
                {
                    if (bunifuDropdown12.Text == "основному средству")
                    {
                        queri = "SELECT `id_р`, `ремонт`.`id_ос`, `ремонт`.`id_вида_р`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_р`.`наименование` AS `Вид ремонта`, `дата_начала` AS `Дата начала ремонта`, `дата_окончания` AS `Дата окончания ремонта`, `сум_затрат` AS `Сумма затрат на ремонт: руб.` FROM `ремонт` INNER JOIN `ос` ON `ремонт`.`id_ос`=`ос`.`id_ос` INNER JOIN `вид_р` ON `ремонт`.`id_вида_р`=`вид_р`.`id_вида_р` WHERE `ремонт`.`id_ос`=" + editItem[0] + ";";
                    }
                    if (bunifuDropdown12.Text == "виду ремонта")
                    {
                        queri = "SELECT `id_р`, `ремонт`.`id_ос`, `ремонт`.`id_вида_р`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_р`.`наименование` AS `Вид ремонта`, `дата_начала` AS `Дата начала ремонта`, `дата_окончания` AS `Дата окончания ремонта`, `сум_затрат` AS `Сумма затрат на ремонт: руб.` FROM `ремонт` INNER JOIN `ос` ON `ремонт`.`id_ос`=`ос`.`id_ос` INNER JOIN `вид_р` ON `ремонт`.`id_вида_р`=`вид_р`.`id_вида_р` WHERE `вид_р`.`наименование`='" + bunifuDropdown4.Text + "';";
                    }
                    if (bunifuDropdown12.Text == "дате начала ремонта")
                    {
                        queri = "SELECT `id_р`, `ремонт`.`id_ос`, `ремонт`.`id_вида_р`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_р`.`наименование` AS `Вид ремонта`, `дата_начала` AS `Дата начала ремонта`, `дата_окончания` AS `Дата окончания ремонта`, `сум_затрат` AS `Сумма затрат на ремонт: руб.` FROM `ремонт` INNER JOIN `ос` ON `ремонт`.`id_ос`=`ос`.`id_ос` INNER JOIN `вид_р` ON `ремонт`.`id_вида_р`=`вид_р`.`id_вида_р` WHERE `дата_начала` BETWEEN '" + dat_start1 + "' and '" + dat_end1 + "';";
                    }
                    if (bunifuDropdown12.Text == "дате окончания ремонта")
                    {
                        queri = "SELECT `id_р`, `ремонт`.`id_ос`, `ремонт`.`id_вида_р`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_р`.`наименование` AS `Вид ремонта`, `дата_начала` AS `Дата начала ремонта`, `дата_окончания` AS `Дата окончания ремонта`, `сум_затрат` AS `Сумма затрат на ремонт: руб.` FROM `ремонт` INNER JOIN `ос` ON `ремонт`.`id_ос`=`ос`.`id_ос` INNER JOIN `вид_р` ON `ремонт`.`id_вида_р`=`вид_р`.`id_вида_р` WHERE `дата_окончания` BETWEEN '" + dat_start2 + "' and '" + dat_end2 + "';";
                    }
                }
                else if (bunifuCheckBox2.Checked == true)
                {
                    if (bunifuDropdown12.Text == "основному средству")
                    {
                        queri = "SELECT `id_р`, `ремонт`.`id_ос`, `ремонт`.`id_вида_р`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_р`.`наименование` AS `Вид ремонта`, `дата_начала` AS `Дата начала ремонта`, `дата_окончания` AS `Дата окончания ремонта`, `сум_затрат` AS `Сумма затрат на ремонт: руб.` FROM `ремонт` INNER JOIN `ос` ON `ремонт`.`id_ос`=`ос`.`id_ос` INNER JOIN `вид_р` ON `ремонт`.`id_вида_р`=`вид_р`.`id_вида_р` WHERE `ос`.`статус`=1 AND `ремонт`.`id_ос`=" + editItem[0] + ";";
                    }
                    if (bunifuDropdown12.Text == "виду ремонта")
                    {
                        queri = "SELECT `id_р`, `ремонт`.`id_ос`, `ремонт`.`id_вида_р`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_р`.`наименование` AS `Вид ремонта`, `дата_начала` AS `Дата начала ремонта`, `дата_окончания` AS `Дата окончания ремонта`, `сум_затрат` AS `Сумма затрат на ремонт: руб.` FROM `ремонт` INNER JOIN `ос` ON `ремонт`.`id_ос`=`ос`.`id_ос` INNER JOIN `вид_р` ON `ремонт`.`id_вида_р`=`вид_р`.`id_вида_р` WHERE `ос`.`статус`=1 AND `вид_р`.`наименование`='" + bunifuDropdown4.Text + "';";
                    }
                    if (bunifuDropdown12.Text == "дате начала ремонта")
                    {
                        queri = "SELECT `id_р`, `ремонт`.`id_ос`, `ремонт`.`id_вида_р`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_р`.`наименование` AS `Вид ремонта`, `дата_начала` AS `Дата начала ремонта`, `дата_окончания` AS `Дата окончания ремонта`, `сум_затрат` AS `Сумма затрат на ремонт: руб.` FROM `ремонт` INNER JOIN `ос` ON `ремонт`.`id_ос`=`ос`.`id_ос` INNER JOIN `вид_р` ON `ремонт`.`id_вида_р`=`вид_р`.`id_вида_р` WHERE `ос`.`статус`=1 and `дата_начала` BETWEEN '" + dat_start1 + "' and '" + dat_end1 + "';";
                    }
                    if (bunifuDropdown12.Text == "дате окончания ремонта")
                    {
                        queri = "SELECT `id_р`, `ремонт`.`id_ос`, `ремонт`.`id_вида_р`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_р`.`наименование` AS `Вид ремонта`, `дата_начала` AS `Дата начала ремонта`, `дата_окончания` AS `Дата окончания ремонта`, `сум_затрат` AS `Сумма затрат на ремонт: руб.` FROM `ремонт` INNER JOIN `ос` ON `ремонт`.`id_ос`=`ос`.`id_ос` INNER JOIN `вид_р` ON `ремонт`.`id_вида_р`=`вид_р`.`id_вида_р` WHERE `ос`.`статус`=1 and `дата_окончания` BETWEEN '" + dat_start2 + "' and '" + dat_end2 + "';";
                    }
                }
                else
                {
                    if (bunifuDropdown12.Text == "основному средству")
                    {
                        queri = "SELECT `id_р`, `ремонт`.`id_ос`, `ремонт`.`id_вида_р`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_р`.`наименование` AS `Вид ремонта`, `дата_начала` AS `Дата начала ремонта`, `дата_окончания` AS `Дата окончания ремонта`, `сум_затрат` AS `Сумма затрат на ремонт: руб.` FROM `ремонт` INNER JOIN `ос` ON `ремонт`.`id_ос`=`ос`.`id_ос` INNER JOIN `вид_р` ON `ремонт`.`id_вида_р`=`вид_р`.`id_вида_р` WHERE `ос`.`статус`=0 AND `ремонт`.`id_ос`=" + editItem[0] + ";";
                    }
                    if (bunifuDropdown12.Text == "виду ремонта")
                    {
                        queri = "SELECT `id_р`, `ремонт`.`id_ос`, `ремонт`.`id_вида_р`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_р`.`наименование` AS `Вид ремонта`, `дата_начала` AS `Дата начала ремонта`, `дата_окончания` AS `Дата окончания ремонта`, `сум_затрат` AS `Сумма затрат на ремонт: руб.` FROM `ремонт` INNER JOIN `ос` ON `ремонт`.`id_ос`=`ос`.`id_ос` INNER JOIN `вид_р` ON `ремонт`.`id_вида_р`=`вид_р`.`id_вида_р` WHERE `ос`.`статус`=0 AND `вид_р`.`наименование`='" + bunifuDropdown4.Text + "';";
                    }
                    if (bunifuDropdown12.Text == "дате начала ремонта")
                    {
                        queri = "SELECT `id_р`, `ремонт`.`id_ос`, `ремонт`.`id_вида_р`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_р`.`наименование` AS `Вид ремонта`, `дата_начала` AS `Дата начала ремонта`, `дата_окончания` AS `Дата окончания ремонта`, `сум_затрат` AS `Сумма затрат на ремонт: руб.` FROM `ремонт` INNER JOIN `ос` ON `ремонт`.`id_ос`=`ос`.`id_ос` INNER JOIN `вид_р` ON `ремонт`.`id_вида_р`=`вид_р`.`id_вида_р` WHERE `ос`.`статус`=0 and `дата_начала` BETWEEN '" + dat_start1 + "' and '" + dat_end1 + "';";
                    }
                    if (bunifuDropdown12.Text == "дате окончания ремонта")
                    {
                        queri = "SELECT `id_р`, `ремонт`.`id_ос`, `ремонт`.`id_вида_р`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_р`.`наименование` AS `Вид ремонта`, `дата_начала` AS `Дата начала ремонта`, `дата_окончания` AS `Дата окончания ремонта`, `сум_затрат` AS `Сумма затрат на ремонт: руб.` FROM `ремонт` INNER JOIN `ос` ON `ремонт`.`id_ос`=`ос`.`id_ос` INNER JOIN `вид_р` ON `ремонт`.`id_вида_р`=`вид_р`.`id_вида_р` WHERE `ос`.`статус`=0 and `дата_окончания` BETWEEN '" + dat_start2 + "' and '" + dat_end2 + "';";
                    }
                }
                try
                {
                    MySqlCommand command3 = new MySqlCommand(queri, db3.GetConnection());
                    adapter3.SelectCommand = command3;
                    adapter3.Fill(table3);
                    bunifuDataGridView1.DataSource = table3.DefaultView;
                    bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = false; bunifuDataGridView1.Columns[2].Visible = false;
                    db.closeConnection();
                    bunifuTextBox1.Text = ""; bunifuLabel1.Text = ""; bunifuDatePicker1.Value = d; bunifuDropdown1.Text = "";
                }
                catch
                {
                    MessageBox.Show("Запрос не обработан.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void bunifuDatePicker6_ValueChanged(object sender, EventArgs e)
        {
            bunifuDatePicker7.Enabled = true;
            bunifuDatePicker7.MinDate = bunifuDatePicker6.Value;
        }

        private void bunifuDataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                pr1 = 1;
                editItem.Clear();
                for (int i = 0; i < bunifuDataGridView4.ColumnCount; i++)
                {
                    editItem.Add(bunifuDataGridView4.Rows[e.RowIndex].Cells[i].Value.ToString());
                }
            }
            catch
            {
                pr1 = 0;
            }
        }

        private void bunifuDatePicker8_ValueChanged(object sender, EventArgs e)
        {
            bunifuDatePicker9.Enabled = true;
            bunifuDatePicker9.MinDate = bunifuDatePicker8.Value;
        }

        private void bunifuButton11_Click(object sender, EventArgs e)
        {
            if (pr == 0 || pr == null)
            {
                MessageBox.Show("Не выбрана запись для формирования документа!", "Внимание!");
            }
            else if (editItem[8]=="")
            {
                MessageBox.Show("Акт ремонта составляется по завершении процесса ремонта!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Начало процесса формирования документа!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var helper = new WordHelper(Application.StartupPath + @"\\Word\AktRemont.docx");
                DateTime d1 = d;
                string d_s = $"{d1.Day}-{d1.Month}-{d1.Year}";
                d1 = Convert.ToDateTime(editItem[6]);
                string dat1 = $"{d1.Day}-{d1.Month}-{d1.Year}";
                d1 = Convert.ToDateTime(editItem[7]);
                string dat2 = $"{d1.Day}-{d1.Month}-{d1.Year}";
                var items = new Dictionary<string, string>
                    {
                        {"$n_d$", editItem[0]},
                        {"$d_s$", d_s},
                        {"$dat1$", dat1},
                        {"$dat2$", dat2},
                        {"$name$", editItem[3]},
                        {"$in_n$", editItem[4]},
                        {"$vid_r$", editItem[5]},
                        {"$fin_st$", editItem[8]},
                    };
                string path = $@"C:\Users\ALEKS\Desktop\Акт ремонта №{editItem[0]}.docx";
                helper.Proccess(items, path);
                pr = 0;
            }
        }
    }
}



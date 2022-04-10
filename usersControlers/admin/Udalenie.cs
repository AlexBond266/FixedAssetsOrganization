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

    public partial class Udalenie : UserControl
    {
        Database db = new Database();
        public int pr = 0;
        List<string> editItem = new List<string>();
        public Udalenie()
        {
            InitializeComponent();
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(db.selectOS_so_spis, db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            bunifuDataGridView1.DataSource = table.DefaultView;
            bunifuDataGridView1.Columns[0].Visible = false;

        }

        private void bunifuButton2_Click(object sender, EventArgs e)    //КНОПКА УДАЛЕНИЯ
        {
            if (pr == 0 || pr == null)
            {
                MessageBox.Show("Не выбрана запись для удаления!", "Внимание!");
            }
            else
            {
                if (pr == 0 || pr == null)
                {
                    MessageBox.Show("Не выбран сотрудник для удаления!", "Внимание!");
                }
                else
                {
                    DialogResult result = MessageBox.Show($"Вы уверены, что хотите удалить запись?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Information); ;
                    if (result.ToString() == "Yes")
                    {
                        Database db = new Database();
                        DataTable table = new DataTable();
                        MySqlDataAdapter adapter = new MySqlDataAdapter();
                        MySqlCommand command = new MySqlCommand();
                        db.openConnection();
                        if (bunifuCheckBox1.Checked == true)
                        {
                            MySqlCommand command2 = new MySqlCommand("DELETE FROM `ос` WHERE `ос`.`id_ос` = " + editItem[0] + ";", db.GetConnection());
                            if (command2.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Запись о поступлении ОС удалена.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                pr = 0;
                                queri = db.selectAvtorisaciaVse;
                                command = new MySqlCommand(db.selectOS_so_spis, db.GetConnection());
                                adapter.SelectCommand = command;
                                adapter.Fill(table);
                                bunifuDataGridView1.DataSource = table.DefaultView;
                                bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = true; bunifuDataGridView1.Columns[2].Visible = true;
                                bunifuDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                                db.closeConnection(); bunifuDataGridView2.Visible = false;
                            }
                            else
                            {
                                MessageBox.Show("Ошибка удаления! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                pr = 0;
                            }   
                        }
                        else if (bunifuCheckBox2.Checked == true)
                        {
                            MySqlCommand command2 = new MySqlCommand("DELETE FROM `ремонт` WHERE `ремонт`.`id_р` = " + editItem[0] + ";", db.GetConnection());
                            if (command2.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Запись о ремонте ОС удалена.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                pr = 0;
                                queri = db.selectAvtorisaciaEko;
                                command = new MySqlCommand(db.selectREM_OS, db.GetConnection());
                                adapter.SelectCommand = command;
                                adapter.Fill(table);
                                bunifuDataGridView1.DataSource = table.DefaultView;
                                bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = false; bunifuDataGridView1.Columns[2].Visible = false;
                                bunifuDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                                db.closeConnection(); bunifuDataGridView2.Visible = false;
                            }
                            else
                            {
                                MessageBox.Show("Ошибка удаления! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                pr = 0;
                            }   
                        }
                        else if (bunifuCheckBox3.Checked == true)
                        {
                            MySqlCommand command2 = new MySqlCommand("DELETE FROM `списание` WHERE `списание`.`id_спис` = " + editItem[0] + ";", db.GetConnection());
                            if (command2.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Запись о списании ОС удалена.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                pr = 0;
                                queri = db.selectAvtorisaciaAdmin;
                                command = new MySqlCommand(db.selectSPIS_OS, db.GetConnection());
                                adapter.SelectCommand = command;
                                adapter.Fill(table);
                                bunifuDataGridView1.DataSource = table.DefaultView;
                                bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = false; bunifuDataGridView1.Columns[2].Visible = false;
                                bunifuDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                                db.closeConnection(); bunifuDataGridView2.Visible = false;

                                Database db2 = new Database();
                                MySqlCommand command5 = new MySqlCommand("UPDATE `ос` SET `статус` = '1' WHERE `ос`.`id_ос` = " + editItem[1] + ";", db2.GetConnection());
                                db2.openConnection();
                                if (command5.ExecuteNonQuery() == 1)
                                {
                                    MessageBox.Show("Статус учёта ОС обновлён.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Ошибка обновления статуса! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                db2.closeConnection();

                                Database db4 = new Database();
                                MySqlCommand command6 = new MySqlCommand("INSERT INTO `закрепление` (`id_ос`, `id_мол`) VALUES ('" + editItem[1] + "', '1');", db4.GetConnection());
                                db4.openConnection();
                                if (command6.ExecuteNonQuery() == 1)
                                {
                                    MessageBox.Show("Запись о закреплении ОС за МОЛ добавлена. Основное средство закреплено за директором.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Ошибка добавления записи о закреплении! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                db4.closeConnection();
                            }
                            else
                            {
                                MessageBox.Show("Ошибка удаления! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                pr = 0;
                            } 
                        }
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
                if (bunifuCheckBox1.Checked == true)
                {
                    Database db = new Database();
                    DataTable table = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand command = new MySqlCommand("SELECT `ос`.`id_ос`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_ос`.`наименование` AS `Вид ОС`, `дата_принятия` AS `Дата принятия`, `пер_стоимость` AS `Первоначальная стоимость: руб.`, `спи` AS `СПИ: лет`, `норма_ндс` AS `Норма НДС: %`, `сумма_ндс` AS `Сумма НДС: руб.`, `год_сум_а` AS `ГСА: руб.`, `списание`.`дата_списания` AS `Дата списания` , `фин_стоимость` AS `Финальная стоимость: руб.`FROM `вид_ос` INNER JOIN (`ос` LEFT JOIN `списание` ON `ос`.`id_ос`=`списание`.`id_ос`) ON `вид_ос`.`id_вида_ос`=`ос`.`id_вида_ос` WHERE `ос`.`id_ос`=" + editItem[0] + ";", db.GetConnection());
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                    bunifuDataGridView2.DataSource = table.DefaultView;
                    bunifuDataGridView2.Columns[0].Visible = false; bunifuDataGridView2.Columns[1].Visible = true; bunifuDataGridView2.Columns[2].Visible = true;
                    bunifuDataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
                else if (bunifuCheckBox2.Checked == true)
                {
                    Database db = new Database();
                    DataTable table = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand command = new MySqlCommand("SELECT `id_р`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_р`.`наименование` AS `Вид ремонта`, `дата_начала` AS `Дата начала ремонта`, `дата_окончания` AS `Дата окончания ремонта`, `сум_затрат` AS `Сумма затрат на ремонт: руб.` FROM `ремонт` INNER JOIN `ос` ON `ремонт`.`id_ос`=`ос`.`id_ос` INNER JOIN `вид_р` ON `ремонт`.`id_вида_р`=`вид_р`.`id_вида_р` WHERE `ремонт`.`id_р`=" + editItem[0] + ";", db.GetConnection());
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                    bunifuDataGridView2.DataSource = table.DefaultView;
                    bunifuDataGridView2.Columns[0].Visible = false; bunifuDataGridView2.Columns[1].Visible = true; bunifuDataGridView2.Columns[2].Visible = true;
                    bunifuDataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
                else if(bunifuCheckBox3.Checked == true)
                {
                    Database db = new Database();
                    DataTable table = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand command = new MySqlCommand("SELECT `id_спис`, `ос`.`id_ос`, `причины`.`id_причины`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `причины`.`наименование` AS `Причина списания`, `дата_списания` AS `Дата списания` FROM `списание` INNER JOIN `ос` ON `списание`.`id_ос`=`ос`.`id_ос` INNER JOIN `причины` ON `списание`.`id_причины`=`причины`.`id_причины` WHERE `списание`.`id_спис`=" + editItem[0] + ";", db.GetConnection());
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                    bunifuDataGridView2.DataSource = table.DefaultView;
                    bunifuDataGridView2.Columns[0].Visible = false; bunifuDataGridView2.Columns[1].Visible = false; bunifuDataGridView2.Columns[2].Visible = false;
                    bunifuDataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                bunifuDataGridView2.Visible = true;
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
                MySqlCommand command = new MySqlCommand(db.selectOS_so_spis, db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                bunifuDataGridView1.DataSource = table.DefaultView;
                bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = true; bunifuDataGridView1.Columns[2].Visible = true;
                bunifuDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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
                MySqlCommand command = new MySqlCommand(db.selectREM_OS, db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                bunifuDataGridView1.DataSource = table.DefaultView;
                bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = false; bunifuDataGridView1.Columns[2].Visible = false;
                bunifuDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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
                MySqlCommand command = new MySqlCommand(db.selectSPIS_OS, db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                bunifuDataGridView1.DataSource = table.DefaultView;
                bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = false; bunifuDataGridView1.Columns[2].Visible = false;
                bunifuDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
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
    }
}














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

namespace PR_TRPO.UsersControlers
{
    public partial class SpisanieOS : UserControl
    {
        Database db = new Database();
        List<string> editItem = new List<string>();
        public int pr = 0, pr1 = 0; DateTime d = DateTime.Now;

        public SpisanieOS()
        {
            InitializeComponent();
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(db.selectSPIS_OS, db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            bunifuDataGridView1.DataSource = table.DefaultView;
            bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = false; bunifuDataGridView1.Columns[2].Visible = false;

            DateTime d = DateTime.Now;
            bunifuDatePicker1.Value = d;
            bunifuDatePicker1.MaxDate = d;
            bunifuDatePicker2.Value = d;
            bunifuDatePicker2.MaxDate = d;
            bunifuDatePicker6.Value = d;
            bunifuDatePicker6.MaxDate = d;
            bunifuDatePicker7.Value = d;
            bunifuDatePicker7.MaxDate = d;


            string[] arrayVid_Spis = getVid_Spis().Select(n => n.ToString()).ToArray();
            bunifuDropdown1.Items.AddRange(arrayVid_Spis);
            bunifuDropdown2.Items.AddRange(arrayVid_Spis);
            bunifuDropdown4.Items.AddRange(arrayVid_Spis);
        }
        private List<string> getVid_Spis()
        {
            List<string> opers = new List<string>();
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `причины`", db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            foreach (DataRow item in table.Rows)
            {
                opers.Add(item[1].ToString());
            }
            return opers;
        }
        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (pr == 0 || pr == null)
                {
                    MessageBox.Show("Не выбрана запись для изменения!", "Внимание!");
                }
                else
                {
                    bunifuPages1.SelectedTab = tabPage9;
                    groupBox1.Text = "Операция: Изменение данных";
                    bunifuTextBox2.Text = editItem[3];
                    bunifuLabel9.Text = editItem[4];
                    bunifuDropdown2.Text = editItem[5];
                    bunifuDatePicker2.Value = Convert.ToDateTime(editItem[6]);

                    Database db = new Database();
                    DataTable table = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand command = new MySqlCommand("SELECT MAX(`дата_окончания`) AS `Дата окончания ремонта` FROM `ремонт` INNER JOIN `ос` ON `ремонт`.`id_ос`=`ос`.`id_ос` WHERE `ос`.`id_ос`= " + editItem[1] + ";", db.GetConnection());
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                    foreach (DataRow item in table.Rows)
                    {
                        bunifuDatePicker2.MinDate = Convert.ToDateTime(item[0]);
                    }
                }
            }
            catch
            {
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT `дата_принятия` FROM `ос` WHERE `id_ос` = " + editItem[1] + ";", db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                foreach (DataRow item in table.Rows)
                {
                    bunifuDatePicker2.MinDate = Convert.ToDateTime(item[0]);
                }
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage2;
            groupBox1.Text = "Операция: Поиск";
            bunifuTextBox15.Focus();
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage3;
            groupBox1.Text = "Операция: Фильтрация";
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage4;
            groupBox1.Text = "Операция: Акт списания";
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
                    MySqlCommand command = new MySqlCommand("SELECT `id_спис`, `ос`.`id_ос`, `причины`.`id_причины`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `причины`.`наименование` AS `Причина списания`, `дата_списания` AS `Дата списания` FROM `списание` INNER JOIN `ос` ON `списание`.`id_ос`=`ос`.`id_ос` INNER JOIN `причины` ON `списание`.`id_причины`=`причины`.`id_причины` WHERE `списание`.`id_спис`=" + editItem[0] + ";", db.GetConnection());
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

        private void bunifuDropdown12_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (bunifuDropdown12.Text)
            {
                case "причине списания": { bunifuPages2.SelectedTab = tabPage7; break; }
                case "дате списания": { bunifuPages2.SelectedTab = tabPage8; break; }
            }
        }

        private void bunifuButton10_Click(object sender, EventArgs e)
        {
            if (bunifuDropdown12.Text == "" || bunifuDropdown12.Text == null)
            {
                MessageBox.Show("Пожалуйста, выберите критерий фильтрации.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (bunifuDropdown12.Text == "причине списания" && (bunifuDropdown4.Text == "" || bunifuDropdown4.Text == null))
            {
                MessageBox.Show("Пожалуйста, выберите причину списания.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DateTime d = bunifuDatePicker6.Value;
                string dat_start1 = $"{d.Year}-{d.Month}-{d.Day}";
                d = bunifuDatePicker7.Value;
                string dat_end1 = $"{d.Year}-{d.Month}-{d.Day}";

                string queri = "";
                Database db3 = new Database();
                DataTable table3 = new DataTable();
                MySqlDataAdapter adapter3 = new MySqlDataAdapter();
                if (bunifuDropdown12.Text == "причине списания")
                {

                    queri = "SELECT `id_спис`, `ос`.`id_ос`, `причины`.`id_причины`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `причины`.`наименование` AS `Причина списания`, `дата_списания` AS `Дата списания` FROM `списание` INNER JOIN `ос` ON `списание`.`id_ос`=`ос`.`id_ос` INNER JOIN `причины` ON `списание`.`id_причины`=`причины`.`id_причины` WHERE `причины`.`наименование`='" + bunifuDropdown4.Text + "';";
                }
                if (bunifuDropdown12.Text == "дате списания")
                {
                    queri = "SELECT `id_спис`, `ос`.`id_ос`, `причины`.`id_причины`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `причины`.`наименование` AS `Причина списания`, `дата_списания` AS `Дата списания` FROM `списание` INNER JOIN `ос` ON `списание`.`id_ос`=`ос`.`id_ос` INNER JOIN `причины` ON `списание`.`id_причины`=`причины`.`id_причины` WHERE `дата_списания` BETWEEN '" + dat_start1 + "' AND '" + dat_end1 + "';";
                }
                try
                {
                    MySqlCommand command3 = new MySqlCommand(queri, db3.GetConnection());
                    adapter3.SelectCommand = command3;
                    adapter3.Fill(table3);
                    bunifuDataGridView1.DataSource = table3.DefaultView;
                    bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = false; bunifuDataGridView1.Columns[2].Visible = false;
                    db.closeConnection();
                }
                catch
                {
                    MessageBox.Show("Запрос не обработан.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private int getRem()
        {
            List<string> opers = new List<string>();
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT `id_р` FROM `ремонт` WHERE `id_ос`="+editItem[0]+" and `дата_окончания` is null", db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            int a = 0;
            foreach (DataRow item in table.Rows)
            {
                a=Convert.ToInt32(item[0]);
            }
            return a;
        }

        private void bunifuDatePicker6_ValueChanged(object sender, EventArgs e)
        {
            bunifuDatePicker7.Enabled = true;
            bunifuDatePicker7.MinDate = bunifuDatePicker6.Value;
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            string querie = $"SELECT `id_причины` FROM `причины` WHERE `наименование`='{bunifuDropdown2.Text}';";
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
            DateTime d = bunifuDatePicker2.Value;
            string dat1 = $"{d.Year}-{d.Month}-{d.Day}";
            MySqlCommand command4 = new MySqlCommand("UPDATE `списание` SET `id_причины` = '" + ID_Vid + "', `дата_списания` = '" + dat1 + "' WHERE `списание`.`id_спис` = " + editItem[0] + ";", db.GetConnection());
            db.openConnection();
            if (command4.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Данные изменены.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Database db3 = new Database();
                DataTable table3 = new DataTable();
                MySqlDataAdapter adapter3 = new MySqlDataAdapter();
                MySqlCommand command3 = new MySqlCommand(db3.selectSPIS_OS, db3.GetConnection());
                adapter3.SelectCommand = command3;
                adapter3.Fill(table3);
                bunifuDataGridView1.DataSource = table3.DefaultView;
                bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = false; bunifuDataGridView1.Columns[2].Visible = false;
                db.closeConnection();
                bunifuTextBox2.Text = ""; bunifuDatePicker2.Value = d;
                bunifuPages1.SelectedTab = tabPage0;
                groupBox1.Text = "Операция:";
                pr = 0;
            }
            else
            {
                MessageBox.Show("Ошибка изменения данных! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                try
                {
                    Database db = new Database();
                    DataTable table = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand command = new MySqlCommand("SELECT MAX(`дата_окончания`) AS `Дата окончания ремонта` FROM `ремонт` INNER JOIN `ос` ON `ремонт`.`id_ос`=`ос`.`id_ос` WHERE `ос`.`id_ос`= " + editItem[0] + ";", db.GetConnection());
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                    foreach (DataRow item in table.Rows)
                    {
                        bunifuDatePicker1.MinDate = Convert.ToDateTime(item[0]);
                    }
                }
                catch
                {
                    bunifuDatePicker1.MinDate = Convert.ToDateTime(editItem[3]);
                }
            }
            catch
            {
                pr = 0;
            }
        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            if (bunifuDropdown1.Text == "" || bunifuDropdown1.Text == null)
            {
                MessageBox.Show("Выберите причину списания.", "Внимание!");
            }
            else
            {
                string querie = $"SELECT `id_причины` FROM `причины` WHERE `наименование`='{bunifuDropdown1.Text}';";
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
                DateTime d = bunifuDatePicker1.Value;
                string dat1 = $"{d.Year}-{d.Month}-{d.Day}";

                MySqlCommand command4 = new MySqlCommand("INSERT INTO `списание` (`id_ос`, `id_причины`, `дата_списания`) VALUES ('" + editItem[0] + "', '" + ID_Vid + "', '" + dat1 + "');", db.GetConnection());
                db.openConnection();
                if (command4.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Основное средство списано.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Database db2 = new Database();
                    MySqlCommand command5 = new MySqlCommand("UPDATE `ос` SET `статус` = '0' WHERE `ос`.`id_ос` = " + editItem[0] + ";", db2.GetConnection());
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
                    MySqlCommand command6 = new MySqlCommand("DELETE FROM `закрепление` WHERE `закрепление`.`id_ос` = " + editItem[0] + ";", db4.GetConnection());
                    db4.openConnection();
                    if (command6.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Запись о закреплении ОС за МОЛ удалена.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка удаления записи о закреплении! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    db4.closeConnection();

                    int id_rem = 0;
                    id_rem = getRem();
                    if (id_rem != 0)
                    {
                        Database db10 = new Database();
                        MySqlCommand command10 = new MySqlCommand("UPDATE `ремонт` SET `дата_окончания` = '" + dat1 + "', `сум_затрат` = '250' WHERE `ремонт`.`id_р` = " + id_rem + ";", db10.GetConnection());
                        db10.openConnection();
                        if (command10.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Выбранное основное средство находится в незаконченном ремонте. Ремонт закрывается датой списания ОС, в сумму затрат выставляется значение в 250 рублей.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка закрытия незаконченного ремонта ОС! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        db10.closeConnection();
                    }
                    else
                    {
                        MessageBox.Show("Записей о незоконченном ремонте не обнаружено.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    Database db3 = new Database();
                    DataTable table3 = new DataTable();
                    MySqlDataAdapter adapter3 = new MySqlDataAdapter();
                    MySqlCommand command3 = new MySqlCommand(db3.selectSPIS_OS, db3.GetConnection());
                    adapter3.SelectCommand = command3;
                    adapter3.Fill(table3);
                    bunifuDataGridView1.DataSource = table3.DefaultView;
                    bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = false; bunifuDataGridView1.Columns[2].Visible = false;
                    db.closeConnection();
                    bunifuTextBox2.Text = ""; bunifuDatePicker2.Value = d;
                    bunifuPages1.SelectedTab = tabPage0;
                    groupBox1.Text = "Операция:";
                    pr = 0;
                }
                else
                {
                    MessageBox.Show("Ошибка списания основного средства! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bunifuButton11_Click(object sender, EventArgs e)
        {
            if (pr == 0 || pr == null)
            {
                MessageBox.Show("Не выбрана запись для формирования документа!", "Внимание!");
            }
            else
            {
                MessageBox.Show("Начало процесса формирования документа!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var helper = new WordHelper(Application.StartupPath + @"\\Word\AktSpisanie.docx");
                DateTime d1 = d;
                string d_s = $"{d1.Day}-{d1.Month}-{d1.Year}";
                d1 = Convert.ToDateTime(editItem[6]);
                string dat1 = $"{d1.Day}-{d1.Month}-{d1.Year}";
                var items = new Dictionary<string, string>
                    {
                        {"$n_d$", editItem[0]},
                        {"$d_s$", d_s},
                        {"$name$", editItem[3]},
                        {"$in_n$", editItem[4]},
                        {"$spis$", editItem[5]},
                        {"$dat1$", dat1},
                    };
                string path = $@"C:\Users\ALEKS\Desktop\Акт списания №{editItem[0]}.docx";
                helper.Proccess(items, path);
                pr = 0;
            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage1;
            groupBox1.Text = "Операция: Списание ОС";
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT `id_ос`, `наименование` as `Наименование ОС`, `ин` AS `Инвентарный номер`, `дата_принятия` FROM `ос` WHERE `статус`=1", db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            bunifuDataGridView2.DataSource = table.DefaultView;
            bunifuDataGridView2.Columns[0].Visible = false;
        }
    }
}


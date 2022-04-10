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
using System.Reflection;

namespace PR_TRPO.UsersControlers
{

    public partial class PostyplenieOS : UserControl
    {
        Database db = new Database();
        int PODSCHIT = 0;
        DateTime d = DateTime.Now;
        public PostyplenieOS()
        {
            InitializeComponent();
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(db.selectOS_bez_spis, db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            bunifuDataGridView1.DataSource = table.DefaultView;
            bunifuDataGridView1.Columns[0].Visible = false;

            string[] semestr = getVid_OS().Select(n => n.ToString()).ToArray();
            bunifuDropdown4.Items.AddRange(semestr);
            bunifuDropdown7.Items.AddRange(semestr);
            bunifuDropdown8.Items.AddRange(semestr);

            string[] arrayMOL = getMOL().Select(n => n.ToString()).ToArray();
            bunifuDropdown6.Items.AddRange(arrayMOL);
            bunifuDropdown1.Items.AddRange(arrayMOL);

            DateTime d = DateTime.Now;
            bunifuDatePicker1.Value = d;
            bunifuDatePicker1.MaxDate = d;
            bunifuDatePicker2.Value = d;
            bunifuDatePicker2.MaxDate = d;
            bunifuDatePicker3.Value = d;
            bunifuDatePicker3.MaxDate = d;
            bunifuDatePicker4.Value = d;
            bunifuDatePicker4.MaxDate = d;
        }

        private void bunifuCheckBox2_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            string queri = "";
            if (bunifuCheckBox2.Checked == true)
            {
                queri = db.selectOS_so_spis;
            }
            else
            {
                queri = db.selectOS_bez_spis;
            }
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(queri, db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            bunifuDataGridView1.DataSource = table.DefaultView;
            bunifuDataGridView1.Columns[0].Visible = false;
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(bunifuTextBox6.Text) < 25)
            {
                bunifuTextBox6.Text = Convert.ToString(Convert.ToInt32(bunifuTextBox6.Text) + 1);
            }
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(bunifuTextBox6.Text) > 1)
            {
                bunifuTextBox6.Text = Convert.ToString(Convert.ToInt32(bunifuTextBox6.Text) - 1);
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e) //КНОПКА ДОБАВЛЕНИЯ
        {
            bunifuPages1.SelectedTab = tabPage1;
            groupBox1.Text = "Операция: Принятие основного средства";
            bunifuTextBox1.Focus();
        }

        public int pr = 0;
        List<string> editItem = new List<string>();
        int Id_oS = 0;
        int ID_MOL_IZM = 0;
        private void bunifuButton2_Click(object sender, EventArgs e)    //КНОПКА ИЗМЕНЕНИЯ
        {
            if (pr == 0 || pr == null)
            {
                MessageBox.Show("Не выбран элемент для изменения!", "Внимание!");
            }
            else
            {
                bunifuPages1.SelectedTab = tabPage2;
                groupBox1.Text = "Операция: Изменение записи об основном средстве";
                bunifuTextBox14.Focus();
                Id_oS = Convert.ToInt32(editItem[0]);
                bunifuTextBox14.Text = editItem[1];
                bunifuDropdown8.Text = editItem[3];
                bunifuLabel24.Text = editItem[2][0].ToString() + editItem[2][1].ToString() + editItem[2][2].ToString();
                bunifuTextBox13.Text = editItem[2][3].ToString() + editItem[2][4].ToString() + editItem[2][5].ToString();
                bunifuTextBox12.Text = editItem[5];
                bunifuTextBox11.Text = editItem[6];
                bunifuDropdown2.Text = editItem[7];
                bunifuDatePicker2.Value = Convert.ToDateTime(editItem[4]);
                bunifuTextBox9.Text = editItem[8];
                bunifuTextBox4.Text = editItem[9];
                bunifuTextBox10.Text = editItem[10];

                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT `закрепление`.`id_мол`,`ф`,`и`,`о` FROM `мол` INNER JOIN (`закрепление` INNER JOIN `ос` ON `закрепление`.`id_ос`=`ос`.`id_ос`) on `мол`.`id_мол`=`закрепление`.`id_мол` WHERE `ос`.`id_ос`=" + Id_oS + ";", db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                string st = "";
                foreach (DataRow item in table.Rows)
                {
                    st = item[1].ToString() + " " + item[2].ToString() + " " + item[3].ToString();
                    ID_MOL_IZM = Convert.ToInt32(item[0]);
                }
                bunifuDropdown1.Text = st;

            }
        }
        private void bunifuButton3_Click(object sender, EventArgs e)  //КНОПКА ПОИСКА
        {
            bunifuPages1.SelectedTab = tabPage3;
            groupBox1.Text = "Операция: Поиск";
            bunifuTextBox15.Focus();
        }

        private void bunifuButton6_Click(object sender, EventArgs e) //КНОПКА ФИЛЬТРАЦИИ
        {
            bunifuPages1.SelectedTab = tabPage4;
            groupBox1.Text = "Операция: Фильтрация";
        }

        private void bunifuButton7_Click(object sender, EventArgs e) //КНОПКА ДОКУМЕНТОВ
        {
            bunifuPages1.SelectedTab = tabPage5;
            groupBox1.Text = "Операция: Документы";
            //bunifuLabel29.Focus();
            //Database db = new Database();
            //DataTable table = new DataTable();
            //MySqlDataAdapter adapter = new MySqlDataAdapter();
            //MySqlCommand command = new MySqlCommand(db.selectOS_bez_spis, db.GetConnection());
            //adapter.SelectCommand = command;
            //adapter.Fill(table);
            //bunifuDataGridView2.DataSource = table.DefaultView;
            //bunifuDataGridView2.Columns[0].Visible = false;
        }
        int stat_ne_spis = 1;
        int IN = 0;
        string per_st = "", summa_nds = "", fin_summa = "", god_summa_a = "";

        private void bunifuButton8_Click(object sender, EventArgs e) //КНОПКА ПРИНЯТИЯ ОС
        {
            int[] number = get_id_vid_os().Select(n => Convert.ToInt32(n)).ToArray();
            DateTime d = bunifuDatePicker1.Value;
            string dat = $"{d.Year}-{d.Month}-{d.Day}";

            per_st = Convert.ToString(Math.Round(Convert.ToDouble(bunifuTextBox3.Text), 2)); per_st = per_st.Replace(',', '.');
            summa_nds = Convert.ToString(Math.Round(Convert.ToDouble(bunifuTextBox5.Text), 2)); summa_nds = summa_nds.Replace(',', '.');
            fin_summa = Convert.ToString(Math.Round(Convert.ToDouble(bunifuTextBox8.Text), 2)); fin_summa = fin_summa.Replace(',', '.');
            god_summa_a = Convert.ToString(Math.Round(Convert.ToDouble(bunifuTextBox7.Text), 2)); god_summa_a = god_summa_a.Replace(',', '.');

            try
            {
                Database db = new Database();
                MySqlCommand command = new MySqlCommand("INSERT INTO `ос` (`наименование`, `ин`, `id_вида_ос`, `дата_принятия`, `пер_стоимость`, `спи`, `норма_ндс`, `сумма_ндс`, `фин_стоимость`, `год_сум_а`, `статус`) VALUES ('" + bunifuTextBox1.Text + "', '" + Convert.ToInt32(bunifuLabel9.Text + bunifuTextBox2.Text) + "', '" + number[0] + "', '" + dat + "', '" + per_st + "', '" + bunifuTextBox6.Text + "', '" + Convert.ToInt32(bunifuDropdown5.Text) + "', '" + summa_nds + "', '" + fin_summa + "', '" + god_summa_a + "', '" + stat_ne_spis + "');", db.GetConnection());
                db.openConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Основное средство принято!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка принятия основного средства! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Database db1 = new Database();
                DataTable table1 = new DataTable();
                MySqlDataAdapter adapter1 = new MySqlDataAdapter();
                MySqlCommand command1 = new MySqlCommand("SELECT * FROM `мол`", db1.GetConnection());
                adapter1.SelectCommand = command1;
                adapter1.Fill(table1);
                string[] fio = new string[1];
                int id_mol = 0;
                foreach (DataRow item in table1.Rows)
                {
                    fio[0] = $"{item[2].ToString()} {item[3].ToString()} {item[4].ToString()}";
                    if (bunifuDropdown6.Text == fio[0])
                    {
                        id_mol = Convert.ToInt32(item[0]);
                        break;
                    }
                }
                int id_os = 0;
                Database db2 = new Database();
                DataTable table2 = new DataTable();
                MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                MySqlCommand command2 = new MySqlCommand("SELECT `id_ос` FROM `ос` WHERE `ин`='" + IN + "' and `статус`=1;", db2.GetConnection());
                adapter2.SelectCommand = command2;
                adapter2.Fill(table2);
                foreach (DataRow item in table2.Rows)
                {
                    id_os = Convert.ToInt32(item[0]);
                }
                command2 = new MySqlCommand("INSERT INTO `закрепление` (`id_ос`, `id_мол`) VALUES ('" + id_os + "', '" + id_mol + "');", db2.GetConnection());
                db2.openConnection();
                if (command2.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Основное средство закреплено за материально ответственным лицом.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка закрепления основного средства! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                db2.closeConnection();
                string queri = "";
                if (bunifuCheckBox2.Checked == true)
                {
                    queri = db.selectOS_so_spis;
                }
                else
                {
                    queri = db.selectOS_bez_spis;
                }
                Database db3 = new Database();
                DataTable table3 = new DataTable();
                MySqlDataAdapter adapter3 = new MySqlDataAdapter();
                MySqlCommand command3 = new MySqlCommand(queri, db3.GetConnection());
                adapter3.SelectCommand = command3;
                adapter3.Fill(table3);
                bunifuDataGridView1.DataSource = table3.DefaultView;
                bunifuDataGridView1.Columns[0].Visible = false;
                db.closeConnection();
            }
            catch
            {
                MessageBox.Show("Связь с базой данных не установлена. Пожалуйста, проверьте соединение с интернетом и перезапустите программу.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            bunifuTextBox1.Text = null;
            bunifuTextBox1.Focus();
            bunifuDropdown4.Text = null;
            bunifuLabel9.Text = "000";
            bunifuTextBox2.Text = null;
            bunifuTextBox3.Text = null;
            bunifuDropdown6.Text = null;
            bunifuTextBox6.Text = "1";
            bunifuDropdown5.Text = null;
            bunifuTextBox5.Text = null;
            bunifuTextBox7.Text = null;
            bunifuTextBox8.Text = null;
        }
        private List<string> getVid_OS()
        {
            List<string> opers = new List<string>();
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `вид_ос`", db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            foreach (DataRow item in table.Rows)
            {
                opers.Add(item[1].ToString());
            }
            return opers;
        }
        public List<string> getMOL()
        {
            List<string> opers = new List<string>();
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `мол`", db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            foreach (DataRow item in table.Rows)
            {
                opers.Add(item[2].ToString() + " " + item[3].ToString() + " " + item[4].ToString());
            }
            return opers;
        }
        private void bunifuDropdown4_SelectedIndexChanged(object sender, EventArgs e)
        {
            bunifuTextBox2.Visible = true;
            int[] number = get_id_vid_os().Select(n => Convert.ToInt32(n)).ToArray();
            bunifuLabel9.Text = "10" + number[0].ToString();
            bunifuTextBox2.Focus();
            PODSCHIT = 0;
            bunifuButton8.Enabled = false;
            bunifuImageButton1.Enabled = true;
        }
        public List<string> get_id_vid_os()
        {
            string querie = $"SELECT `id_вида_ос` FROM `вид_ос` WHERE `наименование`='{bunifuDropdown4.Text}';";
            List<string> opers = new List<string>();
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(querie, db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            foreach (DataRow item in table.Rows)
            {
                opers.Add(item[0].ToString());
            }
            return opers;
        }


        private void bunifuButton4_Click(object sender, EventArgs e) //Ввыыод в Exel
        {
            //if (bunifuDataGridView1.Rows.Count > 0)
            //{
            //    MessageBox.Show("Данные экспортированы в Excel");
            //    Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
            //    xcelApp.Application.Workbooks.Add(Type.Missing);
            //    for (int i = 2; i < bunifuDataGridView1.Columns.Count; i++)
            //    {
            //        xcelApp.Cells[1, i] = bunifuDataGridView1.Columns[i - 1].HeaderText;
            //    }
            //    for (int i = 1; i < bunifuDataGridView1.Rows.Count; i++)
            //    {
            //        for (int j = 1; j < bunifuDataGridView1.Columns.Count-1; j++)
            //        {
            //            try
            //            {
            //                xcelApp.Cells[i + 2, j + 1] = bunifuDataGridView1.Rows[i].Cells[j].Value;
            //            }
            //            catch { }
            //        }
            //    }
            //    xcelApp.Columns.AutoFit();
            //    xcelApp.Visible = true;
            //}
        }
        char key;
        private void bunifuTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            key = e.KeyChar;
            e.Handled = true;
            if (Char.IsLetter(key) || Char.IsControl(key) || key == ' ')
            {
                if (bunifuTextBox1.Text.Length == 0)
                {
                    e.KeyChar = char.ToUpper(e.KeyChar);
                }
                else
                {
                    e.KeyChar = char.ToLower(e.KeyChar);
                }
                if (key == ' ' && bunifuTextBox1.Text[bunifuTextBox1.Text.Length - 1] == ' ')
                {
                    e.Handled = true;
                }
                else e.Handled = false;
            }
        }

        private void bunifuTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;
            e.Handled = true;
            if (bunifuTextBox2.Text.Length <= 2 || Char.IsControl(key))
            {
                if (Char.IsDigit(key) || Char.IsControl(key))
                {
                    e.Handled = false;
                }
            }
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
        public List<string> getIN()
        {
            List<string> opers = new List<string>();
            try
            {
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `ос` WHERE `статус`=1;", db.GetConnection());
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

        private void bunifuDropdown3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (bunifuDropdown3.Text)
            {
                case "виду основного средства": { bunifuPages2.SelectedTab = tabPage6; break; }
                case "дате принятия": { bunifuPages2.SelectedTab = tabPage7; break; }
                case "первоначальной стоимости": { bunifuPages2.SelectedTab = tabPage8; bunifuTextBox16.Focus(); break; }
                case "финальной стоимости": { bunifuPages2.SelectedTab = tabPage8; bunifuTextBox16.Focus(); break; }
            }
        }

        private void bunifuButton10_Click(object sender, EventArgs e) // КНОПКА ВЫПОЛНЕНИЯ ФИЛЬТРАЦИИ
        {
            int id_vid_os = 0;
            if (bunifuDropdown3.Text == "" || bunifuDropdown3.Text == null)
            {
                MessageBox.Show("Пожалуйста, выберите критерий фильтрации.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (bunifuDropdown3.Text == "виду основного средства" && (bunifuDropdown7.Text == "" || bunifuDropdown7.Text == null))
            {
                MessageBox.Show("Пожалуйста, выберите вид основного средства.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if ((bunifuDropdown3.Text == "первоначальной стоимости" || bunifuDropdown3.Text == "финальной стоимости") && (bunifuTextBox16.Text == "" || bunifuTextBox16.Text == null))
            {
                MessageBox.Show("Пожалуйста, введите сумму для фильтрации.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bunifuTextBox16.Focus();
            }
            else
            {
                int[] number = get_id_vid_os_for_fil().Select(n => Convert.ToInt32(n)).ToArray();
                DateTime d = bunifuDatePicker3.Value;
                string dat_start = $"{d.Year}-{d.Month}-{d.Day}";
                d = bunifuDatePicker4.Value;
                string dat_end = $"{d.Year}-{d.Month}-{d.Day}";
                string queri = "";
                Database db3 = new Database();
                DataTable table3 = new DataTable();
                MySqlDataAdapter adapter3 = new MySqlDataAdapter();
                if (bunifuCheckBox2.Checked == false)
                {
                    if (bunifuDropdown3.Text == "виду основного средства")
                    {
                        queri = "SELECT `ос`.`id_ос`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_ос`.`наименование` AS `Вид ОС`, `дата_принятия` AS `Дата принятия`, `пер_стоимость` AS `Первоначальная стоимость: руб.`, `спи` AS `СПИ: лет`, `норма_ндс` AS `Норма НДС: %`, `сумма_ндс` AS `Сумма НДС: руб.`, `год_сум_а` AS `ГСА: руб.`, `фин_стоимость` AS `Финальная стоимость: руб.`FROM `вид_ос` INNER JOIN `ос` ON `вид_ос`.`id_вида_ос`=`ос`.`id_вида_ос` WHERE `статус`=1 AND `ос`.`id_вида_ос`=" + number[0] + ";";
                    }
                    if (bunifuDropdown3.Text == "дате принятия")
                    {
                        queri = "SELECT `ос`.`id_ос`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_ос`.`наименование` AS `Вид ОС`, `дата_принятия` AS `Дата принятия`, `пер_стоимость` AS `Первоначальная стоимость: руб.`, `спи` AS `СПИ: лет`, `норма_ндс` AS `Норма НДС: %`, `сумма_ндс` AS `Сумма НДС: руб.`, `год_сум_а` AS `ГСА: руб.`, `фин_стоимость` AS `Финальная стоимость: руб.`FROM `вид_ос` INNER JOIN `ос` ON `вид_ос`.`id_вида_ос`=`ос`.`id_вида_ос` WHERE `статус`=1 AND `дата_принятия` BETWEEN '" + dat_start + "' AND '" + dat_end + "';";
                    }
                    try
                    {
                        if (bunifuDropdown3.Text == "первоначальной стоимости")
                        {
                            per_st = Convert.ToString(Math.Round(Convert.ToDouble(bunifuTextBox16.Text), 2)); per_st = per_st.Replace(',', '.');
                            queri = "SELECT `ос`.`id_ос`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_ос`.`наименование` AS `Вид ОС`, `дата_принятия` AS `Дата принятия`, `пер_стоимость` AS `Первоначальная стоимость: руб.`, `спи` AS `СПИ: лет`, `норма_ндс` AS `Норма НДС: %`, `сумма_ндс` AS `Сумма НДС: руб.`, `год_сум_а` AS `ГСА: руб.`, `фин_стоимость` AS `Финальная стоимость: руб.`FROM `вид_ос` INNER JOIN `ос` ON `вид_ос`.`id_вида_ос`=`ос`.`id_вида_ос` WHERE `статус`=1 AND `пер_стоимость`=" + per_st + ";";
                        }
                        if (bunifuDropdown3.Text == "финальной стоимости")
                        {
                            fin_summa = Convert.ToString(Math.Round(Convert.ToDouble(bunifuTextBox16.Text), 2)); fin_summa = fin_summa.Replace(',', '.');
                            queri = "SELECT `ос`.`id_ос`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_ос`.`наименование` AS `Вид ОС`, `дата_принятия` AS `Дата принятия`, `пер_стоимость` AS `Первоначальная стоимость: руб.`, `спи` AS `СПИ: лет`, `норма_ндс` AS `Норма НДС: %`, `сумма_ндс` AS `Сумма НДС: руб.`, `год_сум_а` AS `ГСА: руб.`, `фин_стоимость` AS `Финальная стоимость: руб.`FROM `вид_ос` INNER JOIN `ос` ON `вид_ос`.`id_вида_ос`=`ос`.`id_вида_ос` WHERE `статус`=1 AND `фин_стоимость`=" + fin_summa + ";";
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Пожалуйста, проверьте корректность введённой суммы.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bunifuTextBox16.Focus();
                    }
                }
                else
                {
                    if (bunifuDropdown3.Text == "виду основного средства")
                    {
                        queri = "SELECT `ос`.`id_ос`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_ос`.`наименование` AS `Вид ОС`, `дата_принятия` AS `Дата принятия`, `пер_стоимость` AS `Первоначальная стоимость: руб.`, `спи` AS `СПИ: лет`, `норма_ндс` AS `Норма НДС: %`, `сумма_ндс` AS `Сумма НДС: руб.`, `год_сум_а` AS `ГСА: руб.`, `списание`.`дата_списания` AS `Дата списания` , `фин_стоимость` AS `Финальная стоимость: руб.`FROM `вид_ос` INNER JOIN (`ос` LEFT JOIN `списание` ON `ос`.`id_ос`=`списание`.`id_ос`) ON `вид_ос`.`id_вида_ос`=`ос`.`id_вида_ос` WHERE `ос`.`id_вида_ос`=" + number[0] + ";";
                    }
                    if (bunifuDropdown3.Text == "дате принятия")
                    {
                        queri = "SELECT `ос`.`id_ос`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_ос`.`наименование` AS `Вид ОС`, `дата_принятия` AS `Дата принятия`, `пер_стоимость` AS `Первоначальная стоимость: руб.`, `спи` AS `СПИ: лет`, `норма_ндс` AS `Норма НДС: %`, `сумма_ндс` AS `Сумма НДС: руб.`, `год_сум_а` AS `ГСА: руб.`, `списание`.`дата_списания` AS `Дата списания` , `фин_стоимость` AS `Финальная стоимость: руб.`FROM `вид_ос` INNER JOIN (`ос` LEFT JOIN `списание` ON `ос`.`id_ос`=`списание`.`id_ос`) ON `вид_ос`.`id_вида_ос`=`ос`.`id_вида_ос` WHERE `дата_принятия` BETWEEN '" + dat_start + "' AND '" + dat_end + "';";
                    }
                    try
                    {
                        if (bunifuDropdown3.Text == "первоначальной стоимости")
                        {
                            per_st = Convert.ToString(Math.Round(Convert.ToDouble(bunifuTextBox16.Text), 2)); per_st = per_st.Replace(',', '.');
                            queri = "SELECT `ос`.`id_ос`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_ос`.`наименование` AS `Вид ОС`, `дата_принятия` AS `Дата принятия`, `пер_стоимость` AS `Первоначальная стоимость: руб.`, `спи` AS `СПИ: лет`, `норма_ндс` AS `Норма НДС: %`, `сумма_ндс` AS `Сумма НДС: руб.`, `год_сум_а` AS `ГСА: руб.`, `списание`.`дата_списания` AS `Дата списания` , `фин_стоимость` AS `Финальная стоимость: руб.`FROM `вид_ос` INNER JOIN (`ос` LEFT JOIN `списание` ON `ос`.`id_ос`=`списание`.`id_ос`) ON `вид_ос`.`id_вида_ос`=`ос`.`id_вида_ос` WHERE `пер_стоимость`=" + per_st + ";";
                        }
                        if (bunifuDropdown3.Text == "финальной стоимости")
                        {
                            fin_summa = Convert.ToString(Math.Round(Convert.ToDouble(bunifuTextBox16.Text), 2)); fin_summa = fin_summa.Replace(',', '.');
                            queri = "SELECT `ос`.`id_ос`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_ос`.`наименование` AS `Вид ОС`, `дата_принятия` AS `Дата принятия`, `пер_стоимость` AS `Первоначальная стоимость: руб.`, `спи` AS `СПИ: лет`, `норма_ндс` AS `Норма НДС: %`, `сумма_ндс` AS `Сумма НДС: руб.`, `год_сум_а` AS `ГСА: руб.`, `списание`.`дата_списания` AS `Дата списания` , `фин_стоимость` AS `Финальная стоимость: руб.`FROM `вид_ос` INNER JOIN (`ос` LEFT JOIN `списание` ON `ос`.`id_ос`=`списание`.`id_ос`) ON `вид_ос`.`id_вида_ос`=`ос`.`id_вида_ос` WHERE `фин_стоимость`=" + fin_summa + ";";
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Пожалуйста, проверьте корректность введённой суммы.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bunifuTextBox16.Focus();
                    }
                }
                try
                {
                    MySqlCommand command3 = new MySqlCommand(queri, db3.GetConnection());
                    adapter3.SelectCommand = command3;
                    adapter3.Fill(table3);
                    bunifuDataGridView1.DataSource = table3.DefaultView;
                    bunifuDataGridView1.Columns[0].Visible = false;
                    db.closeConnection();
                }
                catch
                {
                    MessageBox.Show("Запрос не обработан.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }
        public List<string> get_id_vid_os_for_fil()
        {
            string querie = $"SELECT `id_вида_ос` FROM `вид_ос` WHERE `наименование`='{bunifuDropdown7.Text}';";
            List<string> opers = new List<string>();
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(querie, db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            foreach (DataRow item in table.Rows)
            {
                opers.Add(item[0].ToString());
            }
            return opers;
        }
        public List<string> get_id_vid_os_for_izm()
        {
            string querie = $"SELECT `id_вида_ос` FROM `вид_ос` WHERE `наименование`='{bunifuDropdown8.Text}';";
            List<string> opers = new List<string>();
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(querie, db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            foreach (DataRow item in table.Rows)
            {
                opers.Add(item[0].ToString());
            }
            return opers;
        }

        private void bunifuDatePicker3_ValueChanged(object sender, EventArgs e)
        {
            bunifuDatePicker4.MinDate = bunifuDatePicker3.Value;
        }

        private void bunifuDatePicker4_ValueChanged(object sender, EventArgs e)
        {
            bunifuDatePicker3.MaxDate = bunifuDatePicker4.Value;
        }

        private void bunifuTextBox16_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;
            e.Handled = true;
            if (Char.IsDigit(key) || Char.IsControl(key) || key == ',')
            {
                if (bunifuTextBox16.Text.Length == 0 && (key == '0' || key == ','))
                {
                    e.Handled = true;
                }
                else if (key == ',' && bunifuTextBox16.Text[bunifuTextBox16.Text.Length - 1] == ',')
                {
                    e.Handled = true;
                }
                else if (Char.IsControl(key))
                {
                    e.Handled = false;
                }
                else if (bunifuTextBox16.Text.Contains(',') && key == ',')
                {
                    e.Handled = false;
                }
                else if (bunifuTextBox16.Text.Length >= 4 && bunifuTextBox16.Text[bunifuTextBox16.Text.Length - 3] == ',')
                {
                    e.Handled = true;
                }
                else e.Handled = false;
            }
        }

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
                if (bunifuPages1.SelectedTab == tabPage5)
                {
                    Database db = new Database();
                    DataTable table = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand command = new MySqlCommand("SELECT `ос`.`id_ос`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_ос`.`наименование` AS `Вид ОС`, `дата_принятия` AS `Дата принятия`, `пер_стоимость` AS `Первоначальная стоимость: руб.`, `спи` AS `СПИ: лет`, `норма_ндс` AS `Норма НДС: %`, `сумма_ндс` AS `Сумма НДС: руб.`, `год_сум_а` AS `ГСА: руб.`, `списание`.`дата_списания` AS `Дата списания` , `фин_стоимость` AS `Финальная стоимость: руб.`FROM `вид_ос` INNER JOIN (`ос` LEFT JOIN `списание` ON `ос`.`id_ос`=`списание`.`id_ос`) ON `вид_ос`.`id_вида_ос`=`ос`.`id_вида_ос` WHERE `ос`.`id_ос`=" + editItem[0] + ";", db.GetConnection());
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                    bunifuDataGridView2.DataSource = table.DefaultView;
                    bunifuDataGridView2.Columns[0].Visible = false;
                }
            }
            catch
            {
                pr = 0;
            }
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(bunifuTextBox11.Text) < 25)
            {
                bunifuTextBox11.Text = Convert.ToString(Convert.ToInt32(bunifuTextBox11.Text) + 1);
            }
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(bunifuTextBox11.Text) > 1)
            {
                bunifuTextBox11.Text = Convert.ToString(Convert.ToInt32(bunifuTextBox11.Text) - 1);
            }
        }

        private void bunifuTextBox14_TextChange(object sender, EventArgs e)
        {
            PODSCHIT = 0;
            bunifuButton9.Enabled = false;
            bunifuImageButton4.Enabled = true;
        }

        private void bunifuDropdown8_SelectedIndexChanged(object sender, EventArgs e)
        {
            int[] number = get_id_vid_os_for_izm().Select(n => Convert.ToInt32(n)).ToArray();
            bunifuLabel24.Text = "10" + number[0].ToString();
            bunifuTextBox13.Focus();
            PODSCHIT = 0;
            bunifuButton9.Enabled = false;
            bunifuImageButton4.Enabled = true;
        }

        private void bunifuTextBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            key = e.KeyChar;
            e.Handled = true;
            if (Char.IsLetter(key) || Char.IsControl(key) || key == ' ')
            {
                if (bunifuTextBox14.Text.Length == 0)
                {
                    e.KeyChar = char.ToUpper(e.KeyChar);
                }
                else
                {
                    e.KeyChar = char.ToLower(e.KeyChar);
                }
                if (key == ' ' && bunifuTextBox14.Text[bunifuTextBox14.Text.Length - 1] == ' ')
                {
                    e.Handled = true;
                }
                else e.Handled = false;
            }
        }

        private void bunifuTextBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;
            e.Handled = true;
            if (bunifuTextBox13.Text.Length <= 2 || Char.IsControl(key))
            {
                if (Char.IsDigit(key) || Char.IsControl(key))
                {
                    e.Handled = false;
                }
            }
        }

        private void bunifuTextBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;
            e.Handled = true;
            if (Char.IsDigit(key) || Char.IsControl(key) || key == ',')
            {
                if (bunifuTextBox12.Text.Length == 0 && (key == '0' || key == ','))
                {
                    e.Handled = true;
                }
                else if (key == ',' && bunifuTextBox12.Text[bunifuTextBox12.Text.Length - 1] == ',')
                {
                    e.Handled = true;
                }
                else if (Char.IsControl(key))
                {
                    e.Handled = false;
                }
                else if (bunifuTextBox12.Text.Contains(',') && key == ',')
                {
                    e.Handled = false;
                }
                else if (bunifuTextBox12.Text.Length >= 4 && bunifuTextBox12.Text[bunifuTextBox12.Text.Length - 3] == ',')
                {
                    e.Handled = true;
                }
                else e.Handled = false;
            }
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e) //КАЛЬКУЛЯЦИЯ ИЗМЕНЕНИЕ
        {
            if (bunifuTextBox14.Text == "" || bunifuDropdown8.Text == "" || bunifuTextBox13.Text == "" || bunifuTextBox12.Text == "" || bunifuDropdown1.Text == "" || bunifuDropdown2.Text == "" || bunifuTextBox14.Text == null || bunifuDropdown8.Text == null || bunifuTextBox13.Text == null || bunifuTextBox12.Text == null || bunifuDropdown1.Text == null || bunifuDropdown2.Text == null)
            {
                MessageBox.Show("Пожалуйста, введите все данные.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (bunifuTextBox14.Text == "")
                {
                    bunifuTextBox14.Focus();
                }
                else if (bunifuDropdown8.Text == "")
                {
                    bunifuDropdown8.Focus();
                }
                else if (bunifuTextBox13.Text == "")
                {
                    bunifuTextBox13.Focus();
                }
                else if (bunifuTextBox12.Text == "")
                {
                    bunifuTextBox12.Focus();
                }
                else if (bunifuDropdown1.Text == "")
                {
                    bunifuDropdown1.Focus();
                }
                else if (bunifuDropdown2.Text == "")
                {
                    bunifuDropdown2.Focus();
                }
            }
            else if (bunifuTextBox14.TextLength < 3)
            {
                MessageBox.Show("Наименование ОС короче минимального. Пожалуйста, введите минимум 3 символа.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bunifuTextBox14.Focus();
            }
            else if (bunifuTextBox13.TextLength < 3)
            {
                MessageBox.Show("Инвентарный номар ОС короче 3 символов.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bunifuTextBox13.Focus();
            }
            else
            {
                IN = Convert.ToInt32(bunifuLabel24.Text + bunifuTextBox13.Text);
                string[] array = null;
                int a = 0;
                array = getIN_UP().Select(n => n.ToString()).ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    if (IN.ToString() == array[i])
                    {
                        MessageBox.Show("ОС с таким инвентарным номером уже состоит на учёте.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bunifuTextBox13.Focus();
                        a = 1;
                        break;
                    }
                }
                if (a != 1)
                {
                    try
                    {
                        PODSCHIT = 1;
                        bunifuButton9.Enabled = true;
                        bunifuImageButton4.Enabled = false;
                        bunifuTextBox9.Text = $"{Math.Round(Convert.ToDouble(bunifuTextBox12.Text) * Convert.ToDouble(bunifuDropdown2.Text) / 100, 2)}";
                        bunifuTextBox10.Text = $"{Math.Round(Convert.ToDouble(bunifuTextBox12.Text) + Convert.ToDouble(bunifuTextBox9.Text), 2)}";
                        bunifuTextBox4.Text = $"{Math.Round(Convert.ToDouble(bunifuTextBox10.Text) / Convert.ToDouble(bunifuTextBox11.Text), 2)}";
                    }
                    catch
                    {
                        MessageBox.Show("Проверьте корректность введённых данных.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public List<string> getIN_UP()
        {
            List<string> opers = new List<string>();
            try
            {
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `ос` WHERE `статус`=1 and `id_ос`!=" + Id_oS + ";", db.GetConnection());
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
        private void bunifuButton9_Click(object sender, EventArgs e) //ИЗМЕНЕНИЕ ОС
        {
            int[] number = get_id_vid_os_for_izm().Select(n => Convert.ToInt32(n)).ToArray();
            DateTime d = bunifuDatePicker2.Value;
            string dat = $"{d.Year}-{d.Month}-{d.Day}";

            per_st = Convert.ToString(Math.Round(Convert.ToDouble(bunifuTextBox12.Text), 2)); per_st = per_st.Replace(',', '.');
            summa_nds = Convert.ToString(Math.Round(Convert.ToDouble(bunifuTextBox9.Text), 2)); summa_nds = summa_nds.Replace(',', '.');
            fin_summa = Convert.ToString(Math.Round(Convert.ToDouble(bunifuTextBox10.Text), 2)); fin_summa = fin_summa.Replace(',', '.');
            god_summa_a = Convert.ToString(Math.Round(Convert.ToDouble(bunifuTextBox4.Text), 2)); god_summa_a = god_summa_a.Replace(',', '.');

            try
            {
                Database db = new Database();
                MySqlCommand command = new MySqlCommand("UPDATE `ос` SET `ин` = '" + Convert.ToInt32(bunifuLabel24.Text + bunifuTextBox13.Text) + "', `id_вида_ос` = '" + number[0] + "', `дата_принятия` = '" + dat + "', `пер_стоимость` = '" + per_st + "', `год_сум_а` = '" + god_summa_a + "', `наименование` = '" + bunifuTextBox14.Text + "', `норма_ндс` = '" + bunifuDropdown2.Text + "', `спи` = '" + bunifuTextBox11.Text + "', `сумма_ндс` = '" + summa_nds + "', `фин_стоимость` = '" + fin_summa + "' WHERE `ос`.`id_ос` = " + Id_oS + ";", db.GetConnection());
                db.openConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Данные изменены!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка изменения данных! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //--------------------------
                Database db1 = new Database();
                DataTable table1 = new DataTable();
                MySqlDataAdapter adapter1 = new MySqlDataAdapter();
                MySqlCommand command1 = new MySqlCommand("SELECT * FROM `мол`", db1.GetConnection());
                adapter1.SelectCommand = command1;
                adapter1.Fill(table1);
                string[] fio = new string[1];
                int id_mol = 0;
                foreach (DataRow item in table1.Rows)
                {
                    fio[0] = $"{item[2].ToString()} {item[3].ToString()} {item[4].ToString()}";
                    if (bunifuDropdown1.Text == fio[0])
                    {
                        id_mol = Convert.ToInt32(item[0]);
                        break;
                    }
                }
                int id_os = 0;
                Database db2 = new Database();
                DataTable table2 = new DataTable();
                MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                MySqlCommand command2 = new MySqlCommand("SELECT `id_ос` FROM `ос` WHERE `ин`='" + IN + "' and `статус`=1;", db2.GetConnection());
                adapter2.SelectCommand = command2;
                adapter2.Fill(table2);
                foreach (DataRow item in table2.Rows)
                {
                    id_os = Convert.ToInt32(item[0]);
                }

                int iD_zak = 0;
                Database db3 = new Database();
                DataTable table3 = new DataTable();
                MySqlDataAdapter adapter3 = new MySqlDataAdapter();
                MySqlCommand command3 = new MySqlCommand("SELECT `id_закреп` FROM `закрепление` WHERE `id_ос` = '" + id_os + "' and `id_мол` = '" + ID_MOL_IZM + "';", db3.GetConnection());
                adapter3.SelectCommand = command3;
                adapter3.Fill(table3);
                foreach (DataRow item in table3.Rows)
                {
                    iD_zak = Convert.ToInt32(item[0]);
                }
                MySqlCommand command4 = new MySqlCommand("UPDATE `закрепление` SET `id_ос` = '" + id_os + "', `id_мол` = '" + id_mol + "' WHERE `закрепление`.`id_закреп` = " + iD_zak + ";", db3.GetConnection());
                db3.openConnection();
                if (command4.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Основное средство закреплено за материально ответственным лицом.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка закрепления основного средства! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                string queri = "";
                if (bunifuCheckBox2.Checked == true)
                {
                    queri = db.selectOS_so_spis;
                }
                else
                {
                    queri = db.selectOS_bez_spis;
                }
                Database db4 = new Database();
                DataTable table4 = new DataTable();
                MySqlDataAdapter adapter4 = new MySqlDataAdapter();
                MySqlCommand command5 = new MySqlCommand(queri, db4.GetConnection());
                adapter4.SelectCommand = command5;
                adapter4.Fill(table4);
                bunifuDataGridView1.DataSource = table4.DefaultView;
                bunifuDataGridView1.Columns[0].Visible = false;
                db.closeConnection(); db1.closeConnection(); db2.closeConnection(); db3.closeConnection(); db4.closeConnection();
            }
            catch
            {
                MessageBox.Show("Связь с базой данных не установлена. Пожалуйста, проверьте соединение с интернетом и перезапустите программу.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e) //КАЛЬКУЛЯЦИЯ ДОБАВЛЕНИЕ
        {
            if (bunifuTextBox1.Text == "" || bunifuDropdown4.Text == "" || bunifuTextBox2.Text == "" || bunifuTextBox3.Text == "" || bunifuDropdown6.Text == "" || bunifuDropdown5.Text == "" || bunifuTextBox1.Text == null || bunifuDropdown4.Text == null || bunifuTextBox2.Text == null || bunifuTextBox3.Text == null || bunifuDropdown6.Text == null || bunifuDropdown5.Text == null)
            {
                MessageBox.Show("Пожалуйста, введите все данные.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (bunifuTextBox1.Text == "")
                {
                    bunifuTextBox1.Focus();
                }
                else if (bunifuDropdown4.Text == "")
                {
                    bunifuDropdown4.Focus();
                }
                else if (bunifuTextBox2.Text == "")
                {
                    bunifuTextBox2.Focus();
                }
                else if (bunifuTextBox3.Text == "")
                {
                    bunifuTextBox3.Focus();
                }
                else if (bunifuDropdown6.Text == "")
                {
                    bunifuDropdown6.Focus();
                }
                else if (bunifuDropdown5.Text == "")
                {
                    bunifuDropdown5.Focus();
                }
            }
            else if (bunifuTextBox1.TextLength < 3)
            {
                MessageBox.Show("Наименование ОС короче минимального. Пожалуйста, введите минимум 3 символа.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bunifuTextBox1.Focus();
            }
            else if (bunifuTextBox2.TextLength < 3)
            {
                MessageBox.Show("Инвентарный номар ОС короче 3 символов.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bunifuTextBox2.Focus();
            }
            else
            {
                IN = Convert.ToInt32(bunifuLabel9.Text + bunifuTextBox2.Text);
                string[] array = null;
                int a = 0;
                array = getIN().Select(n => n.ToString()).ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    if (IN.ToString() == array[i])
                    {
                        MessageBox.Show("ОС с таким инвентарным номером уже состоит на учёте.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bunifuTextBox2.Focus();
                        a = 1;
                        break;
                    }
                }
                if (a != 1)
                {
                    try
                    {
                        PODSCHIT = 1;
                        bunifuButton8.Enabled = true;
                        bunifuImageButton1.Enabled = false;
                        bunifuTextBox5.Text = $"{Math.Round(Convert.ToDouble(bunifuTextBox3.Text) * Convert.ToDouble(bunifuDropdown5.Text) / 100, 2)}";
                        bunifuTextBox8.Text = $"{Math.Round(Convert.ToDouble(bunifuTextBox3.Text) + Convert.ToDouble(bunifuTextBox5.Text), 2)}";
                        bunifuTextBox7.Text = $"{Math.Round(Convert.ToDouble(bunifuTextBox8.Text) / Convert.ToDouble(bunifuTextBox6.Text), 2)}";

                    }
                    catch
                    {
                        MessageBox.Show("Проверьте корректность введённых данных.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void bunifuButton4_Click_1(object sender, EventArgs e)
        {
            if (pr == 0 || pr == null)
            {
                MessageBox.Show("Не выбрана запись для формирования документа!", "Внимание!");
            }
            else
            {
                MessageBox.Show("Начало процесса формирования документа!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getMark();
                DateTime d1 = d;
                string d_s = $"{d1.Day}-{d1.Month}-{d1.Year}";
                d1 = Convert.ToDateTime(editItem[4]);
                string dat1 = $"{d1.Day}-{d1.Month}-{d1.Year}";
                string fin = "";
                if (bunifuCheckBox2.Checked == true)
                {
                    fin = editItem[11];
                }
                else
                {
                    fin = editItem[10];
                }
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT `мол`.* FROM `ос` INNER JOIN (`закрепление` INNER JOIN `мол` on `закрепление`.`id_мол`=`мол`.`id_мол`) on `ос`.`id_ос`=`закрепление`.`id_ос` WHERE `ос`.`id_ос`=" + editItem[0] + "", db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                string id_mol = "";
                foreach (DataRow item in table.Rows)
                {
                    id_mol = item[2].ToString() + " " + item[3].ToString() + " " + item[4].ToString();
                }

                string spis = ""; 
                Database db2 = new Database();
                DataTable table2 = new DataTable();
                MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                MySqlCommand command2 = new MySqlCommand("SELECT `причины`.`наименование` AS `Причина списания`, `дата_списания` AS `Дата списания` FROM `списание` INNER JOIN `ос` ON `списание`.`id_ос`=`ос`.`id_ос` INNER JOIN `причины` ON `списание`.`id_причины`=`причины`.`id_причины` WHERE `ос`.`id_ос`=" + editItem[0] + "", db2.GetConnection());
                adapter2.SelectCommand = command2;
                adapter2.Fill(table2);
                foreach (DataRow item in table2.Rows)
                {
                    DateTime d2 = Convert.ToDateTime(item[1]);
                    string d_njja = $"{d2.Day}-{d2.Month}-{d2.Year}";
                    spis = "Объект основных средств в наименовании '" + editItem[1] + "' проходит процедуру списания и снимается с учёта " + d_njja + " по причине '" + item[0].ToString() + "'.";
                }
                if (spis== "")
                {
                    spis = "На момент составления документа объект основных средств состоит на учёте.";
                }
                if (id_mol == "" || id_mol ==null)
                {
                    id_mol = "Отсутствует";
                }

                string path = $@"C:\Users\ALEKS\Desktop\Инвентарная карточка объекта ОС №{editItem[0]}.docx";
                Object newFileName = path;
                var newpathdoc = newFileName;

                //TODO
                var wordAPP = new Word.Application();
                wordAPP.Visible = false;

                var worddocument = wordAPP.Documents.Open(Application.StartupPath + @"\\Word\InKarOS.docx");

                //ПЕРЕДАЧА ЗНАЧЕНИЙ
                Object missing = Type.Missing;
                var items = new Dictionary<string, string>
                    {
                        {"$n_d$", editItem[0]},
                        {"$d_s$", d_s},
                        {"$name$", editItem[1].ToString()},
                        {"$in_n$", editItem[2].ToString()},
                        {"$vid$", editItem[3].ToString()},
                        {"$mol$", id_mol},
                        {"$dat1$", dat1},
                        {"$ps$", editItem[5].ToString()},
                        {"$nn$", editItem[7].ToString()},
                        {"$sn$", editItem[8].ToString()},
                        {"$spi$", editItem[6].ToString()},
                        {"$gsa$", editItem[9].ToString()},
                        {"$fin$", fin},
                        {"$spis$", spis}
                        ,
                    };
                foreach (var item in items)
                {
                    Word.Find find = wordAPP.Selection.Find;
                    find.Text = item.Key;
                    find.Replacement.Text = item.Value;

                    Object wrap = Word.WdFindWrap.wdFindContinue;
                    Object replace = Word.WdReplace.wdReplaceAll;

                    find.Execute(
                    FindText: Type.Missing,
                    MatchCase: false,
                    MatchWholeWord: false,
                    MatchWildcards: false,
                    MatchSoundsLike: missing,
                    MatchAllWordForms: false,
                    Forward: true,
                    Wrap: wrap,
                    Format: false,
                    ReplaceWith: missing, Replace: replace);
                }

                //Таблица 1
                var n = 0;

                Word.Table tab = worddocument.Tables[3];
                for (int i = 1; i <= vid.Count; i++)
                {
                    tab.Rows.Add(Missing.Value);
                    tab.Cell(i, 1).Range.Text = vid[n];
                    tab.Cell(i, 2).Range.Text = d_n[n];
                    tab.Cell(i, 3).Range.Text = d_O[n];
                    tab.Cell(i, 4).Range.Text = stoimost[n];
                    n++;
                }
                MessageBox.Show("Отчёт сформирован!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                worddocument.SaveAs(newpathdoc);
                wordAPP.Visible = true;
            }
        }
        List<string> vid = new List<string>();
        List<string> d_n = new List<string>();
        List<string> d_O = new List<string>();
        List<string> stoimost = new List<string>();


        private void getMark()
        {
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT `вид_р`.`наименование` AS `Вид ремонта`, `дата_начала`, `дата_окончания`, `сум_затрат` FROM `ремонт` INNER JOIN `ос` ON `ремонт`.`id_ос`=`ос`.`id_ос` INNER JOIN `вид_р` ON `ремонт`.`id_вида_р`=`вид_р`.`id_вида_р` WHERE `ос`.`id_ос`=" + editItem[0] + ";", db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            foreach (DataRow item in table.Rows)
            {
                vid.Add(item[0].ToString());
                DateTime d1 = Convert.ToDateTime(item[1]);
                string d_na = $"{d1.Day}-{d1.Month}-{d1.Year}";
                d_n.Add(d_na);
                d1 = Convert.ToDateTime(item[2]);
                d_na = $"{d1.Day}-{d1.Month}-{d1.Year}";
                d_O.Add(d_na);
                stoimost.Add(item[3].ToString());
                
            }

        }

        private void bunifuTextBox1_TextChange(object sender, EventArgs e)
        {
            PODSCHIT = 0;
            bunifuButton8.Enabled = false;
            bunifuImageButton1.Enabled = true;
        }

        private void bunifuDropdown6_SelectedIndexChanged(object sender, EventArgs e)
        {
            PODSCHIT = 0;
            bunifuButton8.Enabled = false;
            bunifuImageButton1.Enabled = true;
        }
        private void bunifuButton5_Click(object sender, EventArgs e)
        {

            if (pr == 0 || pr == null)
            {
                MessageBox.Show("Не выбрана запись для формирования документа!", "Внимание!");
            }
            else
            {
                MessageBox.Show("Начало процесса формирования документа!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var helper = new WordHelper(Application.StartupPath + @"\\Word\AktPostyplenie.docx");
                DateTime d1 = d;
                string d_s = $"{d1.Day}-{d1.Month}-{d1.Year}";
                d1 = Convert.ToDateTime(editItem[4]);
                string dat1 = $"{d1.Day}-{d1.Month}-{d1.Year}";
                string fin = "";
                if (bunifuCheckBox2.Checked == true)
                {
                    fin = editItem[11];
                }
                else
                {
                    fin = editItem[10];
                }
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT `мол`.* FROM `ос` INNER JOIN (`закрепление` INNER JOIN `мол` on `закрепление`.`id_мол`=`мол`.`id_мол`) on `ос`.`id_ос`=`закрепление`.`id_ос` WHERE `ос`.`id_ос`="+editItem[0]+"", db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                string id_mol = "";
                foreach (DataRow item in table.Rows)
                {
                    id_mol=item[2].ToString() + " " + item[3].ToString() + " " + item[4].ToString();
                }
                var items = new Dictionary<string, string>
                    {
                        {"$n_d$", editItem[0]},
                        {"$d_s$", d_s},
                        {"$name$", editItem[1]},
                        {"$in_n$", editItem[2]},
                        {"$vid$", editItem[3]},
                        {"$dat1$", dat1},
                        {"$ps$", editItem[5]},
                        {"$nn$", editItem[7]},
                        {"$sn$", editItem[8]},
                        {"$spi$", editItem[6]},
                        {"$gsa$", editItem[9]},
                        {"$fin$", fin},
                        {"$mol$", id_mol},
                    };
                string path = $@"C:\Users\ALEKS\Desktop\Акт поступления №{editItem[0]}.docx";
                helper.Proccess(items, path);
                pr = 0;
            }
        }
    }
}














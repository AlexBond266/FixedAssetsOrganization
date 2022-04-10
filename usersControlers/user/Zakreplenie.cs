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
using System.Reflection;

namespace PR_TRPO.UsersControlers
{
    public partial class Zakreplenie : UserControl
    {
        Database db = new Database();
        DateTime d = DateTime.Now;
        public int pr = 0;
        List<string> editItem = new List<string>();
        public Zakreplenie()
        {
            InitializeComponent();
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(db.selectMOL_Zakrep, db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            bunifuDataGridView1.DataSource = table.DefaultView;
            bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = false; bunifuDataGridView1.Columns[2].Visible = false;

            string[] arrayMOL = getMOL().Select(n => n.ToString()).ToArray();
            bunifuDropdown1.Items.AddRange(arrayMOL);
            bunifuDropdown3.Items.AddRange(arrayMOL);
            bunifuDropdown5.Items.AddRange(arrayMOL);

            string[] Dol = getDol().Select(n => n.ToString()).ToArray();
            bunifuDropdown4.Items.AddRange(Dol);
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

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage2;
            groupBox1.Text = "Операция: Поиск";
            bunifuTextBox15.Focus();
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
            }
            catch
            {
                pr = 0;
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (pr == 0 || pr == null)
            {
                MessageBox.Show("Не выбран элемент для изменения!", "Внимание!");
            }
            else
            {
                bunifuPages1.SelectedTab = tabPage1;
                groupBox1.Text = "Операция: Изменение данных";
                bunifuTextBox1.Text = editItem[3].ToString();
                bunifuLabel1.Text = editItem[4].ToString();
                bunifuDropdown1.Text = editItem[5].ToString();
            }
        }

        private void bunifuDropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bunifuButton8.Enabled = true;
        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
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
            MySqlCommand command4 = new MySqlCommand("UPDATE `закрепление` SET `id_ос` = '" + editItem[1] + "', `id_мол` = '" + id_mol + "' WHERE `закрепление`.`id_закреп` = " + editItem[0] + ";", db.GetConnection());
            db.openConnection();
            if (command4.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Основное средство закреплено за материально ответственным лицом.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand(db.selectMOL_Zakrep, db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                bunifuDataGridView1.DataSource = table.DefaultView;
                bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = false; bunifuDataGridView1.Columns[2].Visible = false;
                bunifuPages1.SelectedTab = tabPage0;
                groupBox1.Text = "Операция:";
            }
            else
            {
                MessageBox.Show("Ошибка закрепления основного средства! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage3;
            groupBox1.Text = "Операция: Фильтрация";
        }

        private void bunifuButton10_Click(object sender, EventArgs e) // КНОПКА ВЫПОЛНЕНИЯ ФИЛЬТРАЦИИ
        {
            int id_vid_os = 0;
            if (bunifuDropdown2.Text == "" || bunifuDropdown2.Text == null)
            {
                MessageBox.Show("Пожалуйста, выберите критерий фильтрации.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (bunifuDropdown2.Text == "материально ответственному лицу" && (bunifuDropdown3.Text == "" || bunifuDropdown3.Text == null))
            {
                MessageBox.Show("Пожалуйста, выберите материально ответственное лицо.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (bunifuDropdown2.Text == "должности" && (bunifuDropdown4.Text == "" || bunifuDropdown4.Text == null))
            {
                MessageBox.Show("Пожалуйста, выберите должность материально ответственного лица.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string queri = "";
                Database db3 = new Database();
                db3.openConnection();
                DataTable table3 = new DataTable();
                MySqlDataAdapter adapter3 = new MySqlDataAdapter();
                if (bunifuDropdown2.Text == "материально ответственному лицу")
                {
                    string[] st = bunifuDropdown3.Text.Split(' ');
                    queri = "SELECT `id_закреп`, `ос`.`id_ос`, `мол`.`id_мол`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, CONCAT(`мол`.`ф`,' ',`мол`.`и`,' ',`мол`.`о`) AS `Материально ответственное лицо`, `должности`.`наименование` AS `Должность` FROM `ос` INNER JOIN (`закрепление` INNER JOIN (`мол` INNER JOIN `должности` ON `мол`.`id_должности`=`должности`.`id_должности`) ON `закрепление`.`id_мол`=`мол`.`id_мол` ) ON `ос`.`id_ос`=`закрепление`.`id_ос` WHERE `ф`='"+st[0]+"' and `и`='"+st[1]+"' and `о`='"+st[2]+"';";
                }
                else
                {
                    queri = "SELECT `id_закреп`, `ос`.`id_ос`, `мол`.`id_мол`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, CONCAT(`мол`.`ф`,' ',`мол`.`и`,' ',`мол`.`о`) AS `Материально ответственное лицо`, `должности`.`наименование` AS `Должность` FROM `ос` INNER JOIN (`закрепление` INNER JOIN (`мол` INNER JOIN `должности` ON `мол`.`id_должности`=`должности`.`id_должности`) ON `закрепление`.`id_мол`=`мол`.`id_мол` ) ON `ос`.`id_ос`=`закрепление`.`id_ос` WHERE `должности`.`наименование`='"+bunifuDropdown4.Text+"';";
                }
                try
                {
                    MySqlCommand command3 = new MySqlCommand(queri, db3.GetConnection());
                    adapter3.SelectCommand = command3;
                    adapter3.Fill(table3);
                    bunifuDataGridView1.DataSource = table3.DefaultView;
                    bunifuDataGridView1.Columns[0].Visible = false; bunifuDataGridView1.Columns[1].Visible = false; bunifuDataGridView1.Columns[2].Visible = false;
                    db3.closeConnection();
                }
                catch
                {
                    MessageBox.Show("Запрос не обработан.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void bunifuDropdown2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (bunifuDropdown2.Text)
            {
                case "материально ответственному лицу": { bunifuPages2.SelectedTab = tabPage6; break; }
                case "должности": { bunifuPages2.SelectedTab = tabPage7; break; }
            }
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage4;
            groupBox1.Text = "Операция: Отчёт на МОЛ";
        }
        int id_mol = 0;
        private void bunifuDropdown5_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            
            string[] st = bunifuDropdown5.Text.Split(' ');
            Database db3 = new Database();
            db3.openConnection();
            DataTable table3 = new DataTable();
            MySqlDataAdapter adapter3 = new MySqlDataAdapter();
            MySqlCommand command3 = new MySqlCommand("SELECT `мол`.`id_мол` FROM `мол` WHERE `ф`='" + st[0] + "' and `и`='" + st[1] + "' and `о`='" + st[2] + "';", db3.GetConnection());
            adapter3.SelectCommand = command3;
            adapter3.Fill(table3);
            foreach (DataRow item in table3.Rows)
            {
                id_mol = Convert.ToInt32(item[0]);
            }
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `ос`.`дата_принятия` AS `Дата принятия` FROM `ос` INNER JOIN (`закрепление` INNER JOIN `мол` ON `закрепление`.`id_мол`=`мол`.`id_мол` ) ON `ос`.`id_ос`=`закрепление`.`id_ос` WHERE `мол`.`id_мол`="+id_mol+";", db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            bunifuDataGridView2.DataSource = table.DefaultView;

            db3.closeConnection();
        }



        List<string> name = new List<string>();
        List<string> in_n = new List<string>();
        List<string> d_p = new List<string>();


        private void getMark()
        {
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `ос`.`дата_принятия` AS `Дата принятия` FROM `ос` INNER JOIN (`закрепление` INNER JOIN `мол` ON `закрепление`.`id_мол`=`мол`.`id_мол` ) ON `ос`.`id_ос`=`закрепление`.`id_ос` WHERE `мол`.`id_мол`=" + id_mol + ";", db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            foreach (DataRow item in table.Rows)
            {
                name.Add(item[0].ToString());
                in_n.Add(item[1].ToString());
                DateTime d1 = Convert.ToDateTime(item[2]);
                string d_s = $"{d1.Day}-{d1.Month}-{d1.Year}";
                d_p.Add(d_s);
            }

        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            if (bunifuDropdown5.Text=="" || bunifuDropdown5.Text == null)
            {
                MessageBox.Show("Пожалуйста, выберите материально ответственное лицо.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Начало процесса формирования документа!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getMark();
                DateTime d1 = d;
                string d_s = $"{d1.Day}-{d1.Month}-{d1.Year}";
                string id = id_mol.ToString();

                string path = $@"C:\Users\ALEKS\Desktop\Список ОС на МОЛ №{id}.docx";
                Object newFileName = path;
                var newpathdoc = newFileName;

                    //TODO
                    var wordAPP = new Word.Application();
                    wordAPP.Visible = false;

                    var worddocument = wordAPP.Documents.Open(Application.StartupPath + @"\\Word\OSnaMOL.docx");

                    //ПЕРЕДАЧА ЗНАЧЕНИЙ
                    Object missing = Type.Missing;
                    var items = new Dictionary<string, string>
                    {
                        {"$n_d$", id},
                        {"$d_s$", d_s},
                        {"$mol$", bunifuDropdown5.Text},
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

                    Word.Table tab = worddocument.Tables[2];
                    for (int i = 1; i <= name.Count; i++)
                    {
                        tab.Rows.Add(Missing.Value);
                        tab.Cell(i, 1).Range.Text = name[n];
                        tab.Cell(i, 2).Range.Text = in_n[n];
                        tab.Cell(i, 3).Range.Text = d_p[n];
                        n++;
                    }
                MessageBox.Show("Отчёт сформирован!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                worddocument.SaveAs(newpathdoc);
                wordAPP.Visible = true;
            }
        }
    }
}

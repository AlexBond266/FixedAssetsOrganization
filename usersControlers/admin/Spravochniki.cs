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

    public partial class Spravochniki : UserControl
    {
        Database db = new Database();
        public int pr = 0;
        List<string> editItem = new List<string>();
        public Spravochniki()
        {
            InitializeComponent();
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(db.selectSvidOS, db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            bunifuDataGridView1.DataSource = table.DefaultView;
            bunifuDataGridView1.Columns[0].Visible = false;

        }

        private void bunifuButton1_Click(object sender, EventArgs e) //КНОПКА ПОВЫШЕНИЯ ДО АДМИНИСТРАТОРА
        {
            if (pr == 0 || pr == null)
            {
                MessageBox.Show("Не выбран сотрудник для повышения до администратора!", "Внимание!");
            }
            else
            {
                if (Convert.ToInt32(editItem[3]) == 1)
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
                        //if (bunifuCheckBox1.Checked == true)
                        //{
                        //    queri = db.selectAvtorisaciaVse;
                        //}
                        //else if (bunifuCheckBox2.Checked == true)
                        //{
                        //    queri = db.selectAvtorisaciaEko;
                        //}
                        //else queri = db.selectAvtorisaciaAdmin;
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

        char key;

        string queri = "";

        private void bunifuDropdown3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database db = new Database();
            switch (bunifuDropdown1.Text)
            {
                case "Виды основных средств": { bunifuPages1.SelectedTab = tabPage1; queri = db.selectSvidOS; break; }
                case "Должности": { bunifuPages1.SelectedTab = tabPage2; queri = db.selectSdolgnosti; break; }
                case "Виды ремонта": { bunifuPages1.SelectedTab = tabPage3; queri = db.selectSvid_r; break; }
                case "Причины списания": { bunifuPages1.SelectedTab = tabPage4; queri = db.selectSprichini; break; }
            }
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(queri, db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            bunifuDataGridView1.DataSource = table.DefaultView;
            bunifuDataGridView1.Columns[0].Visible = false;
        }

        public List<string> getVidOs()
        {
            List<string> opers = new List<string>();
            try
            {
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand(db.selectSvidOS, db.GetConnection());
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
        public List<string> getDolgnost()
        {
            List<string> opers = new List<string>();
            try
            {
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand(db.selectSdolgnosti, db.GetConnection());
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
        public List<string> getVid_r()
        {
            List<string> opers = new List<string>();
            try
            {
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand(db.selectSvid_r, db.GetConnection());
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
        public List<string> getPrichin()
        {
            List<string> opers = new List<string>();
            try
            {
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand(db.selectSprichini, db.GetConnection());
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
        private void bunifuButton7_Click(object sender, EventArgs e)  //ДОБАВЛЕНИЕ ВИДА ОС
        {
            if (bunifuTextBox1.Text == "" || bunifuTextBox1.Text == null)
            {
                MessageBox.Show("Пожалуйста, введите наименование вида ОС.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bunifuTextBox1.Focus();
            }
            else
            {
                string[] array = null;
                int a = 0;
                array = getVidOs().Select(n => n.ToString()).ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    if (bunifuTextBox1.Text == array[i])
                    {
                        MessageBox.Show("Такой вид ОС уже зарегистрирован.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bunifuTextBox1.Text = "";
                        bunifuTextBox1.Focus();
                        a = 1;
                        break;
                    }
                }
                if (a != 1)
                {
                    try
                    {
                        Database db = new Database();
                        MySqlCommand command = new MySqlCommand("INSERT INTO `вид_ос` (`наименование`) VALUES ('" + bunifuTextBox1.Text + "');", db.GetConnection());
                        db.openConnection();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Вид основного средства добавлен.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            bunifuTextBox1.Text = "";
                            bunifuTextBox1.Focus();
                            pr = 0;
                        }
                        else
                        {
                            MessageBox.Show("Ошибка добавления вида ОС! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            bunifuTextBox1.Text = "";
                            bunifuTextBox1.Focus();
                        }
                        db.closeConnection();
                        Database db2 = new Database();
                        DataTable table2 = new DataTable();
                        MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                        MySqlCommand command2 = new MySqlCommand(db2.selectSvidOS, db2.GetConnection());
                        adapter2.SelectCommand = command2;
                        adapter2.Fill(table2);
                        bunifuDataGridView1.DataSource = table2.DefaultView;
                        bunifuDataGridView1.Columns[0].Visible = false;
                    }
                    catch
                    {
                        MessageBox.Show("Связь с базой данных не установлена. Пожалуйста, проверьте соединение с интернетом и перезапустите программу.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void bunifuButton1_Click_1(object sender, EventArgs e) //ДОБАВЛЕНИЕ ДОЛЖНОСТИ
        {
            if (bunifuTextBox2.Text == "" || bunifuTextBox2.Text == null)
            {
                MessageBox.Show("Пожалуйста, введите наименование должности.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bunifuTextBox2.Focus();
            }
            else
            {
                string[] array = null;
                int a = 0;
                array = getDolgnost().Select(n => n.ToString()).ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    if (bunifuTextBox2.Text == array[i])
                    {
                        MessageBox.Show("Такая должность уже зарегистрирована.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bunifuTextBox2.Text = "";
                        bunifuTextBox2.Focus();
                        a = 1;
                        break;
                    }
                }
                if (a != 1)
                {
                    try
                    {
                        Database db = new Database();
                        MySqlCommand command = new MySqlCommand("INSERT INTO `должности` (`наименование`) VALUES ('" + bunifuTextBox2.Text + "');", db.GetConnection());
                        db.openConnection();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Должность добавлена.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            bunifuTextBox2.Text = "";
                            bunifuTextBox2.Focus();
                            pr = 0;
                        }
                        else
                        {
                            MessageBox.Show("Ошибка добавления должности! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            bunifuTextBox2.Text = "";
                            bunifuTextBox2.Focus();
                        }
                        db.closeConnection();
                        Database db2 = new Database();
                        DataTable table2 = new DataTable();
                        MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                        MySqlCommand command2 = new MySqlCommand(db2.selectSdolgnosti, db2.GetConnection());
                        adapter2.SelectCommand = command2;
                        adapter2.Fill(table2);
                        bunifuDataGridView1.DataSource = table2.DefaultView;
                        bunifuDataGridView1.Columns[0].Visible = false;
                    }
                    catch
                    {
                        MessageBox.Show("Связь с базой данных не установлена. Пожалуйста, проверьте соединение с интернетом и перезапустите программу.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void bunifuButton6_Click(object sender, EventArgs e) //ДОБАВЛЕНИЕ ВИДА РЕМОНТА
        {
            if (bunifuTextBox3.Text == "" || bunifuTextBox3.Text == null)
            {
                MessageBox.Show("Пожалуйста, введите вид ремонта.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bunifuTextBox3.Focus();
            }
            else
            {
                string[] array = null;
                int a = 0;
                array = getVid_r().Select(n => n.ToString()).ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    if (bunifuTextBox3.Text == array[i])
                    {
                        MessageBox.Show("Такой вид ремонта уже зарегистрирован.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bunifuTextBox3.Text = "";
                        bunifuTextBox3.Focus();
                        a = 1;
                        break;
                    }
                }
                if (a != 1)
                {
                    try
                    {
                        Database db = new Database();
                        MySqlCommand command = new MySqlCommand("INSERT INTO `вид_р` (`наименование`) VALUES ('" + bunifuTextBox3.Text + "');", db.GetConnection());
                        db.openConnection();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Вид ремонта добавлен.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            bunifuTextBox3.Text = "";
                            bunifuTextBox3.Focus();
                            pr = 0;
                        }
                        else
                        {
                            MessageBox.Show("Ошибка добавления вида ремонта! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            bunifuTextBox3.Text = "";
                            bunifuTextBox3.Focus();
                        }
                        db.closeConnection();
                        Database db2 = new Database();
                        DataTable table2 = new DataTable();
                        MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                        MySqlCommand command2 = new MySqlCommand(db2.selectSvid_r, db2.GetConnection());
                        adapter2.SelectCommand = command2;
                        adapter2.Fill(table2);
                        bunifuDataGridView1.DataSource = table2.DefaultView;
                        bunifuDataGridView1.Columns[0].Visible = false;
                    }
                    catch
                    {
                        MessageBox.Show("Связь с базой данных не установлена. Пожалуйста, проверьте соединение с интернетом и перезапустите программу.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void bunifuButton10_Click(object sender, EventArgs e) //ДОБАВЛЕНИЕ ПРИЧИНЫ
        {
            if (bunifuTextBox4.Text == "" || bunifuTextBox4.Text == null)
            {
                MessageBox.Show("Пожалуйста, введите причину списания.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bunifuTextBox4.Focus();
            }
            else
            {
                string[] array = null;
                int a = 0;
                array = getPrichin().Select(n => n.ToString()).ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    if (bunifuTextBox4.Text == array[i])
                    {
                        MessageBox.Show("Такая причина списания уже зарегистрирована.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bunifuTextBox4.Text = "";
                        bunifuTextBox4.Focus();
                        a = 1;
                        break;
                    }
                }
                if (a != 1)
                {
                    try
                    {
                        Database db = new Database();
                        MySqlCommand command = new MySqlCommand("INSERT INTO `причины` (`наименование`) VALUES ('" + bunifuTextBox4.Text + "');", db.GetConnection());
                        db.openConnection();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("рричина списания добавлена.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            bunifuTextBox4.Text = "";
                            bunifuTextBox4.Focus();
                            pr = 0;
                        }
                        else
                        {
                            MessageBox.Show("Ошибка добавления причины списания! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            bunifuTextBox4.Text = "";
                            bunifuTextBox4.Focus();
                        }
                        db.closeConnection();
                        Database db2 = new Database();
                        DataTable table2 = new DataTable();
                        MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                        MySqlCommand command2 = new MySqlCommand(db2.selectSprichini, db2.GetConnection());
                        adapter2.SelectCommand = command2;
                        adapter2.Fill(table2);
                        bunifuDataGridView1.DataSource = table2.DefaultView;
                        bunifuDataGridView1.Columns[0].Visible = false;
                    }
                    catch
                    {
                        MessageBox.Show("Связь с базой данных не установлена. Пожалуйста, проверьте соединение с интернетом и перезапустите программу.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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
                switch (bunifuDropdown1.Text)
                {
                    case "Виды основных средств": { bunifuTextBox1.Text = editItem[1]; break; }
                    case "Должности": { bunifuTextBox2.Text = editItem[1]; break; }
                    case "Виды ремонта": { bunifuTextBox3.Text = editItem[1]; break; }
                    case "Причины списания": { bunifuTextBox4.Text = editItem[1]; break; }
                }  
            }
            catch
            {
                pr = 0;
            }
        }

        public List<string> getVidOs1()
        {
            List<string> opers = new List<string>();
            try
            {
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT `id_вида_ос`, `наименование` AS `Наименование вида ОС` FROM `вид_ос` WHERE `id_вида_ос` != '"+editItem[0]+"'", db.GetConnection());
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
        public List<string> getDolgnost1()
        {
            List<string> opers = new List<string>();
            try
            {
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT `id_должности`, `наименование` AS `Наименование должности` FROM `должности` WHERE `id_должности` != '" + editItem[0] + "'", db.GetConnection());
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
        public List<string> getVid_r1()
        {
            List<string> opers = new List<string>();
            try
            {
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT `id_вида_р`, `наименование` AS `Наименование вида ремонта` FROM `вид_р` WHERE `id_вида_р` != '" + editItem[0] + "'", db.GetConnection());
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
        public List<string> getPrichin1()
        {
            List<string> opers = new List<string>();
            try
            {
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT `id_причины`, `наименование` AS `Причины списания` FROM `причины`WHERE `id_причины` != '" + editItem[0] + "'", db.GetConnection());
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

        private void bunifuButton2_Click(object sender, EventArgs e)    //ИЗМЕНЕНИЕ ВИДА ОС
        {
            if (pr == 0 || pr == null)
            {
                MessageBox.Show("Не выбран элемент для изменения!", "Внимание!");
            }
            else
            {
                if (bunifuTextBox1.Text == "" || bunifuTextBox1.Text == null)
                {
                    MessageBox.Show("Пожалуйста, введите наименование вида ОС.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bunifuTextBox1.Focus();
                }
                else
                {
                    string[] array = null;
                    int a = 0;
                    array = getVidOs1().Select(n => n.ToString()).ToArray();
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (bunifuTextBox1.Text == array[i])
                        {
                            MessageBox.Show("Такой вид ОС уже зарегистрирован.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            bunifuTextBox1.Focus();
                            a = 1;
                            break;
                        }
                    }
                    if (Convert.ToInt32(editItem[0])==1)
                    {
                        MessageBox.Show("Данный вид ОС является базовым, по этому его нельзя изменять.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bunifuTextBox1.Focus(); bunifuTextBox1.Text = "";
                        pr = 0;
                    }
                    else if (a != 1)
                    {
                        try
                        {
                            Database db = new Database();
                            MySqlCommand command = new MySqlCommand("UPDATE `вид_ос` SET `наименование` = '"+ bunifuTextBox1.Text + "' WHERE `вид_ос`.`id_вида_ос` = " + editItem[0] + ";", db.GetConnection());
                            db.openConnection();
                            if (command.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Вид основного изменён.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                bunifuTextBox1.Focus();
                                bunifuTextBox1.Text = "";
                                pr = 0;
                            }
                            else
                            {
                                MessageBox.Show("Ошибка изменения вида ОС! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                bunifuTextBox1.Focus();
                            }
                            db.closeConnection();
                            Database db2 = new Database();
                            DataTable table2 = new DataTable();
                            MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                            MySqlCommand command2 = new MySqlCommand(db2.selectSvidOS, db2.GetConnection());
                            adapter2.SelectCommand = command2;
                            adapter2.Fill(table2);
                            bunifuDataGridView1.DataSource = table2.DefaultView;
                            bunifuDataGridView1.Columns[0].Visible = false;
                        }
                        catch
                        {
                            MessageBox.Show("Связь с базой данных не установлена. Пожалуйста, проверьте соединение с интернетом и перезапустите программу.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e) //ИЗМЕНЕНИЕ ДОЛЖНОСТИ
        {
            if (pr == 0 || pr == null)
            {
                MessageBox.Show("Не выбран элемент для изменения!", "Внимание!");
            }
            else
            {
                if (bunifuTextBox2.Text == "" || bunifuTextBox2.Text == null)
                {
                    MessageBox.Show("Пожалуйста, введите наименование должности.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bunifuTextBox2.Focus();
                }
                else
                {
                    string[] array = null;
                    int a = 0;
                    array = getDolgnost1().Select(n => n.ToString()).ToArray();
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (bunifuTextBox2.Text == array[i])
                        {
                            MessageBox.Show("Такая должность уже зарегистрирована.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            bunifuTextBox2.Focus();
                            a = 1;
                            break;
                        }
                    }
                    if (Convert.ToInt32(editItem[0]) == 1)
                    {
                        MessageBox.Show("Данная должность является базовым, по этому её нельзя изменять.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bunifuTextBox2.Focus(); bunifuTextBox2.Text = "";
                        pr = 0;
                    }
                    else if (a != 1)
                    {
                        try
                        {
                            Database db = new Database();
                            MySqlCommand command = new MySqlCommand("UPDATE `должности` SET `наименование` = '" + bunifuTextBox2.Text + "' WHERE `должности`.`id_должности` = " + editItem[0] + ";", db.GetConnection());
                            db.openConnection();
                            if (command.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Должность изменёна.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                bunifuTextBox2.Focus();
                                bunifuTextBox2.Text = "";
                                pr = 0;
                            }
                            else
                            {
                                MessageBox.Show("Ошибка изменения должности! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                bunifuTextBox2.Focus();
                            }
                            db.closeConnection();
                            Database db2 = new Database();
                            DataTable table2 = new DataTable();
                            MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                            MySqlCommand command2 = new MySqlCommand(db2.selectSdolgnosti, db2.GetConnection());
                            adapter2.SelectCommand = command2;
                            adapter2.Fill(table2);
                            bunifuDataGridView1.DataSource = table2.DefaultView;
                            bunifuDataGridView1.Columns[0].Visible = false;
                        }
                        catch
                        {
                            MessageBox.Show("Связь с базой данных не установлена. Пожалуйста, проверьте соединение с интернетом и перезапустите программу.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void bunifuButton8_Click(object sender, EventArgs e) //ИЗМЕНЕНИЕ ВИДА РЕМОНТА
        {
            if (pr == 0 || pr == null)
            {
                MessageBox.Show("Не выбран элемент для изменения!", "Внимание!");
            }
            else
            {
                if (bunifuTextBox3.Text == "" || bunifuTextBox3.Text == null)
                {
                    MessageBox.Show("Пожалуйста, введите наименование вида ремонта.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bunifuTextBox3.Focus();
                }
                else
                {
                    string[] array = null;
                    int a = 0;
                    array = getVid_r1().Select(n => n.ToString()).ToArray();
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (bunifuTextBox3.Text == array[i])
                        {
                            MessageBox.Show("Такой вид ремонта уже зарегистрирован.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            bunifuTextBox3.Focus();
                            a = 1;
                            break;
                        }
                    }
                    if (Convert.ToInt32(editItem[0]) == 1)
                    {
                        MessageBox.Show("Данный вид ремонта является базовым, по этому его нельзя изменять.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bunifuTextBox3.Focus(); bunifuTextBox3.Text = "";
                        pr = 0;
                    }
                    else if (a != 1)
                    {
                        try
                        {
                            Database db = new Database();
                            MySqlCommand command = new MySqlCommand("UPDATE `вид_р` SET `наименование` = '" + bunifuTextBox3.Text + "' WHERE `вид_р`.`id_вида_р` = " + editItem[0] + ";", db.GetConnection());
                            db.openConnection();
                            if (command.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Вид ремонта изменён.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                bunifuTextBox3.Focus();
                                bunifuTextBox3.Text = "";
                                pr = 0;
                            }
                            else
                            {
                                MessageBox.Show("Ошибка изменения вида ремонта! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                bunifuTextBox3.Focus();
                            }
                            db.closeConnection();
                            Database db2 = new Database();
                            DataTable table2 = new DataTable();
                            MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                            MySqlCommand command2 = new MySqlCommand(db2.selectSvid_r, db2.GetConnection());
                            adapter2.SelectCommand = command2;
                            adapter2.Fill(table2);
                            bunifuDataGridView1.DataSource = table2.DefaultView;
                            bunifuDataGridView1.Columns[0].Visible = false;
                        }
                        catch
                        {
                            MessageBox.Show("Связь с базой данных не установлена. Пожалуйста, проверьте соединение с интернетом и перезапустите программу.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void bunifuButton11_Click(object sender, EventArgs e)
        {
            if (pr == 0 || pr == null)
            {
                MessageBox.Show("Не выбран элемент для изменения!", "Внимание!");
            }
            else
            {
                if (bunifuTextBox4.Text == "" || bunifuTextBox4.Text == null)
                {
                    MessageBox.Show("Пожалуйста, введите наименование причины списания.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bunifuTextBox4.Focus();
                }
                else
                {
                    string[] array = null;
                    int a = 0;
                    array = getPrichin1().Select(n => n.ToString()).ToArray();
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (bunifuTextBox4.Text == array[i])
                        {
                            MessageBox.Show("Такая причина списания уже зарегистрирована.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            bunifuTextBox4.Focus();
                            a = 1;
                            break;
                        }
                    }
                    if (Convert.ToInt32(editItem[0]) == 1)
                    {
                        MessageBox.Show("Данная причина списания является базовым, по этому её нельзя изменять.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bunifuTextBox4.Focus(); bunifuTextBox4.Text = "";
                        pr = 0;
                    }
                    else if (a != 1)
                    {
                        try
                        {
                            Database db = new Database();
                            MySqlCommand command = new MySqlCommand("UPDATE `причины` SET `наименование` = '" + bunifuTextBox4.Text + "' WHERE `причины`.`id_причины` = " + editItem[0] + ";", db.GetConnection());
                            db.openConnection();
                            if (command.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Причина списания изменена.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                bunifuTextBox4.Focus();
                                bunifuTextBox4.Text = "";
                                pr = 0;
                            }
                            else
                            {
                                MessageBox.Show("Ошибка изменения причины списания! Попробуете ещё раз или перезагрузите программу.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                bunifuTextBox4.Focus();
                            }
                            db.closeConnection();
                            Database db2 = new Database();
                            DataTable table2 = new DataTable();
                            MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                            MySqlCommand command2 = new MySqlCommand(db2.selectSprichini, db2.GetConnection());
                            adapter2.SelectCommand = command2;
                            adapter2.Fill(table2);
                            bunifuDataGridView1.DataSource = table2.DefaultView;
                            bunifuDataGridView1.Columns[0].Visible = false;
                        }
                        catch
                        {
                            MessageBox.Show("Связь с базой данных не установлена. Пожалуйста, проверьте соединение с интернетом и перезапустите программу.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

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
            key = e.KeyChar;
            e.Handled = true;
            if (Char.IsLetter(key) || Char.IsControl(key) || key == ' ')
            {
                if (bunifuTextBox2.Text.Length == 0)
                {
                    e.KeyChar = char.ToUpper(e.KeyChar);
                }
                else
                {
                    e.KeyChar = char.ToLower(e.KeyChar);
                }
                if (key == ' ' && bunifuTextBox2.Text[bunifuTextBox2.Text.Length - 1] == ' ')
                {
                    e.Handled = true;
                }
                else e.Handled = false;
            }
        }

        private void bunifuTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            key = e.KeyChar;
            e.Handled = true;
            if (Char.IsLetter(key) || Char.IsControl(key) || key == ' ')
            {
                if (bunifuTextBox3.Text.Length == 0)
                {
                    e.KeyChar = char.ToUpper(e.KeyChar);
                }
                else
                {
                    e.KeyChar = char.ToLower(e.KeyChar);
                }
                if (key == ' ' && bunifuTextBox3.Text[bunifuTextBox3.Text.Length - 1] == ' ')
                {
                    e.Handled = true;
                }
                else e.Handled = false;
            }
        }

        private void bunifuTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            key = e.KeyChar;
            e.Handled = true;
            if (Char.IsLetter(key) || Char.IsControl(key) || key == ' ')
            {
                if (bunifuTextBox4.Text.Length == 0)
                {
                    e.KeyChar = char.ToUpper(e.KeyChar);
                }
                else
                {
                    e.KeyChar = char.ToLower(e.KeyChar);
                }
                if (key == ' ' && bunifuTextBox4.Text[bunifuTextBox4.Text.Length - 1] == ' ')
                {
                    e.Handled = true;
                }
                else e.Handled = false;
            }
        }
        private void bunifuButton3_Click(object sender, EventArgs e)  //УДАЛЕНИЕ ВИД ОС
        {
            if (pr == 0 || pr == null)
            {
                MessageBox.Show("Не выбран элемент для удаления!", "Внимание!");
            }
            else
            {
                DialogResult result = MessageBox.Show($"Вы уверены, что хотите удалить вид ОС '" + editItem[1] + "'?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Information); ;
                if (result.ToString() == "Yes")
                {
                    if (Convert.ToInt32(editItem[0]) == 1)
                    {
                        MessageBox.Show("Данный вид ОС является базовым, по этому его нельзя удалять.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bunifuTextBox1.Focus(); bunifuTextBox1.Text = "";
                        pr = 0;
                    }
                    else
                    {
                        Database db = new Database();
                        MySqlCommand command = new MySqlCommand("DELETE FROM `вид_ос` WHERE `вид_ос`.`id_вида_ос` = " + editItem[0] + ";", db.GetConnection());
                        db.openConnection();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Вид ОС удалён.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            MessageBox.Show("ОС удалённого вида переназначаются на базовый вид 'ОС общего назначения'.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            pr = 0; bunifuTextBox1.Text = "";
                            Database db2 = new Database();
                            DataTable table2 = new DataTable();
                            MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                            MySqlCommand command2 = new MySqlCommand(db2.selectSvidOS, db2.GetConnection());
                            adapter2.SelectCommand = command2;
                            adapter2.Fill(table2);
                            bunifuDataGridView1.DataSource = table2.DefaultView;
                            bunifuDataGridView1.Columns[0].Visible = false;
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
        }

        private void bunifuButton5_Click(object sender, EventArgs e) //УДАЛЕНИЕ ДОЛЖНОСТИ
        {
            if (pr == 0 || pr == null)
            {
                MessageBox.Show("Не выбран элемент для удаления!", "Внимание!");
            }
            else
            {
                DialogResult result = MessageBox.Show($"Вы уверены, что хотите удалить должность '" + editItem[1] + "'?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Information); ;
                if (result.ToString() == "Yes")
                {
                    if (Convert.ToInt32(editItem[0]) == 1 || Convert.ToInt32(editItem[0]) == 2)
                    {
                        MessageBox.Show("Данная должность является базовым, по этому её нельзя удалять.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bunifuTextBox2.Focus(); bunifuTextBox2.Text = "";
                        pr = 0;
                    }
                    else
                    {
                        Database db = new Database();
                        MySqlCommand command = new MySqlCommand("DELETE FROM `должности` WHERE `должности`.`id_должности` = " + editItem[0] + ";", db.GetConnection());
                        db.openConnection();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Должность удалена.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            MessageBox.Show("МОЛ, назначенные на удалённую должность, переназначаются на базовую должность 'ОС общего назначения'.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            pr = 0; bunifuTextBox2.Text = "";
                            Database db2 = new Database();
                            DataTable table2 = new DataTable();
                            MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                            MySqlCommand command2 = new MySqlCommand(db2.selectSdolgnosti, db2.GetConnection());
                            adapter2.SelectCommand = command2;
                            adapter2.Fill(table2);
                            bunifuDataGridView1.DataSource = table2.DefaultView;
                            bunifuDataGridView1.Columns[0].Visible = false;
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
        }

        private void bunifuButton9_Click(object sender, EventArgs e) //УДАЛЕНИЕ ВИДА РЕМОНТА
        {
            if (pr == 0 || pr == null)
            {
                MessageBox.Show("Не выбран элемент для удаления!", "Внимание!");
            }
            else
            {
                DialogResult result = MessageBox.Show($"Вы уверены, что хотите удалить вид ремонта '" + editItem[1] + "'?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Information); ;
                if (result.ToString() == "Yes")
                {
                    if (Convert.ToInt32(editItem[0]) == 1)
                    {
                        MessageBox.Show("Данный вид ремонта является базовым, по этому его нельзя удалять.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bunifuTextBox3.Focus(); bunifuTextBox3.Text = "";
                        pr = 0;
                    }
                    else
                    {
                        Database db = new Database();
                        MySqlCommand command = new MySqlCommand("DELETE FROM `вид_р` WHERE `вид_р`.`id_вида_р` = " + editItem[0] + ";", db.GetConnection());
                        db.openConnection();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Вид ремонта удален.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            MessageBox.Show("Записи о ремонте удалённого вида переназначаются на базовый вид ремонта 'Устранение локальной неисправности'.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            pr = 0; bunifuTextBox3.Text = "";
                            Database db2 = new Database();
                            DataTable table2 = new DataTable();
                            MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                            MySqlCommand command2 = new MySqlCommand(db2.selectSvid_r, db2.GetConnection());
                            adapter2.SelectCommand = command2;
                            adapter2.Fill(table2);
                            bunifuDataGridView1.DataSource = table2.DefaultView;
                            bunifuDataGridView1.Columns[0].Visible = false;
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
        }

        private void bunifuButton12_Click(object sender, EventArgs e) //УДАЛЕНИЕ ПРИЧИНЫ СПИСАНИЯ
        {
            if (pr == 0 || pr == null)
            {
                MessageBox.Show("Не выбран элемент для удаления!", "Внимание!");
            }
            else
            {
                DialogResult result = MessageBox.Show($"Вы уверены, что хотите удалить причину списания '" + editItem[1] + "'?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Information); ;
                if (result.ToString() == "Yes")
                {
                    if (Convert.ToInt32(editItem[0]) == 1)
                    {
                        MessageBox.Show("Данная причина списания является базовым, по этому её нельзя удалять.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bunifuTextBox4.Focus(); bunifuTextBox4.Text = "";
                        pr = 0;
                    }
                    else
                    {
                        Database db = new Database();
                        MySqlCommand command = new MySqlCommand("DELETE FROM `причины` WHERE `причины`.`id_причины` = " + editItem[0] + ";", db.GetConnection());
                        db.openConnection();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Причина списания удалена.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            MessageBox.Show("Записи о списании ОС по удалённой причин переназначаются на базовую причину 'Преждевременный износ'.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            pr = 0; bunifuTextBox4.Text = "";
                            Database db2 = new Database();
                            DataTable table2 = new DataTable();
                            MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                            MySqlCommand command2 = new MySqlCommand(db2.selectSprichini, db2.GetConnection());
                            adapter2.SelectCommand = command2;
                            adapter2.Fill(table2);
                            bunifuDataGridView1.DataSource = table2.DefaultView;
                            bunifuDataGridView1.Columns[0].Visible = false;
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
        }
    }
}














using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using PR_TRPO.Scripts;
using MySql.Data.MySqlClient;
using LiveCharts;
using LiveCharts.Wpf;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using LiveCharts.Defaults;

namespace PR_TRPO.Forms
{
    public partial class Chart : Form
    {
        public int ID_User;
        public Chart()
        {
            InitializeComponent();
            DateTime d = DateTime.Now;
            bunifuDatePicker1.Value = d;
            bunifuDatePicker1.MaxDate = d;
            bunifuDatePicker2.Value = d;
            bunifuDatePicker2.MaxDate = d;
        }
        private void bunifuDropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (bunifuDropdown1.Text)
            {
                case "Список поступления/списания ОС": { panel2.Visible = true; cartesianChart1.Visible = true; pieChart1.Visible = false; bunifuButton1.Visible = false; break; }
                case "Оборотное сальдо по итогам года": { panel2.Visible = false; cartesianChart1.Visible = true; pieChart1.Visible = false; bunifuButton1.Visible = true; break; }
                case "Отношение ответственности МОЛ": { Pie(); panel2.Visible = false; cartesianChart1.Visible = false; pieChart1.Visible = true; bunifuButton1.Visible = false; break; }
                case "Годовая динамика поступления ОС": { PosPoGog(); panel2.Visible = false; cartesianChart1.Visible = true; pieChart1.Visible = false; bunifuButton1.Visible = false; break; }
                case "Годовая динамика списания ОС": { SpisPoGog(); panel2.Visible = false; cartesianChart1.Visible = true; pieChart1.Visible = false; bunifuButton1.Visible = false; break; }
                case "Годовая динамика затрат на ремонтные работы": { RemPoGog(); panel2.Visible = false; cartesianChart1.Visible = true; pieChart1.Visible = false; bunifuButton1.Visible = false; break; }
                case "Годовая динамика Уставного Капитала": { UKPoGog(); panel2.Visible = false; cartesianChart1.Visible = true; pieChart1.Visible = false; bunifuButton1.Visible = false; break; }
            }
            Anima();
        }
        
        public void Pie()
        {
            List<string> MOL = new List<string>();
            List<int> KOL = new List<int>();
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT CONCAT(`мол`.`ф`,' ',`мол`.`и`,' ',`мол`.`о`), COUNT(`закрепление`.`id_мол`) FROM `мол` INNER JOIN `закрепление` on `мол`.`id_мол`=`закрепление`.`id_мол` GROUP BY CONCAT(`мол`.`ф`,' ',`мол`.`и`,' ',`мол`.`о`);", db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            foreach (DataRow item in table.Rows)
            {
                MOL.Add(item[0].ToString());
                KOL.Add(Convert.ToInt32(item[1]));
            }
            for (int i = 0; i < MOL.Count; i++)
            {
                pieChart1.Series.Add(
                    new PieSeries
                    {
                        Title = $"{MOL[i]}",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(KOL[i]) },
                        DataLabels = true
                    }
                );
            }
        }
        public void PosPoGog()
        {
            ChartValues<double> SUM = new ChartValues<double>(); //Значения которые будут на линии, будет создания чуть позже. (ТОЧКИ НА ГРАФИКЕ)
            List<string> god = new List<string>();
            for (int i = 2015; i <= DateTime.Now.Year; i++)
            {
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT SUM(`фин_стоимость`) FROM `ос` WHERE `дата_принятия` BETWEEN '"+i+ "-01-01' AND '" + (i+1) + "-01-01'", db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                foreach (DataRow item in table.Rows)
                {
                    god.Add(i.ToString());  
                    SUM.Add(Math.Round(Convert.ToDouble(item[0]),2));
                     
                }
            }
            cartesianChart1.AxisX.Clear(); //Очищаем ось X от значений по умолчанию
            cartesianChart1.AxisX.Add(new Axis //Добавляем на ось X значения, через блок инициализатора.
            {
                Title = "Годовая динамика поступления основных средств в отражении на Уставной капитал, руб.",
                Labels = god,
                FontSize=16

            });
            cartesianChart1.Series.Clear();
            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = SUM,
                    PointGeometrySize=15,
                }
            };
        }

        public void SpisPoGog()
        {
            ChartValues<double> SUM = new ChartValues<double>(); //Значения которые будут на линии, будет создания чуть позже. (ТОЧКИ НА ГРАФИКЕ)
            List<string> god = new List<string>();
            for (int i = 2015; i <= DateTime.Now.Year; i++)
            {
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT SUM(`фин_стоимость`) FROM `ос` INNER JOIN `списание` on `ос`.`id_ос`=`списание`.`id_ос` WHERE `дата_списания` BETWEEN '" + i + "-01-01' AND '" + (i + 1) + "-01-01'", db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                foreach (DataRow item in table.Rows)
                {
                    god.Add(i.ToString());
                    try
                    {
                        SUM.Add(Math.Round(Convert.ToDouble(item[0]), 2));
                    }
                    catch { SUM.Add(0); }
                }
            }
            cartesianChart1.AxisX.Clear(); //Очищаем ось X от значений по умолчанию
            cartesianChart1.AxisX.Add(new Axis //Добавляем на ось X значения, через блок инициализатора.
            {
                Title = "Годовая динамика списания основных средств в отражении на Уставной капитал, руб.",
                Labels = god,
                FontSize = 16

            });
            var converter1 = new System.Windows.Media.BrushConverter();
            cartesianChart1.Series.Clear();
            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = SUM,
                    PointGeometrySize=15,
                    Stroke= (System.Windows.Media.Brush)converter1.ConvertFromString("#FFF34336")
                }
            };
        }

        public void RemPoGog()
        {
            ChartValues<double> SUM = new ChartValues<double>(); //Значения которые будут на линии, будет создания чуть позже. (ТОЧКИ НА ГРАФИКЕ)
            List<string> god = new List<string>();
            for (int i = 2015; i <= DateTime.Now.Year; i++)
            {
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT SUM(`сум_затрат`) FROM `ос` INNER JOIN `ремонт` on `ос`.`id_ос`=`ремонт`.`id_ос` WHERE `дата_окончания` BETWEEN '" + i + "-01-01' AND '" + (i + 1) + "-01-01'", db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                foreach (DataRow item in table.Rows)
                {
                    god.Add(i.ToString());
                    try
                    {
                        SUM.Add(Math.Round(Convert.ToDouble(item[0]), 2));
                    }
                    catch { SUM.Add(0); }
                }
            }
            cartesianChart1.AxisX.Clear(); //Очищаем ось X от значений по умолчанию
            cartesianChart1.AxisX.Add(new Axis //Добавляем на ось X значения, через блок инициализатора.
            {
                Title = "Годовая динамика затрат на ремонтные работы, руб.",
                Labels = god,
                FontSize = 16

            });
            var converter1 = new System.Windows.Media.BrushConverter();
            cartesianChart1.Series.Clear();
            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = SUM,
                    PointGeometrySize=15,
                    Stroke = (System.Windows.Media.Brush)converter1.ConvertFromString("#FFFEC007")
                }
            };
        }

        public void UKPoGog()
        {
            ChartValues<double> SUM = new ChartValues<double>(); //Значения которые будут на линии, будет создания чуть позже. (ТОЧКИ НА ГРАФИКЕ)
            List<string> god = new List<string>();
            double SNG = 0, pos = 0, spisi = 0, SIG = 0;
            for (int i = 2015; i <= DateTime.Now.Year; i++)
            {
                SNG = 0; pos = 0; spisi = 0; SIG = 0;
                Database db = new Database();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT SUM(`фин_стоимость`) FROM `ос` WHERE `статус`= 1 AND `дата_принятия`< '" + i + "-01-01'", db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                foreach (DataRow item in table.Rows)
                {
                    try
                    {
                        SNG = Convert.ToDouble(item[0]);
                    }
                    catch { SNG = 0; }
                    
                }
                Database db2 = new Database();
                DataTable table2 = new DataTable();
                MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                MySqlCommand command2 = new MySqlCommand("SELECT SUM(`фин_стоимость`) FROM `ос` WHERE `статус`= 1 AND `дата_принятия` BETWEEN '" + i + "-01-01' AND '" + (i + 1) + "-01-01'", db2.GetConnection());
                adapter2.SelectCommand = command2;
                adapter2.Fill(table2);
                foreach (DataRow item in table2.Rows)
                {
                    try
                    {
                        pos = Convert.ToDouble(item[0]);
                    }
                    catch { pos = 0; }
                }
                Database db3 = new Database();
                DataTable table3 = new DataTable();
                MySqlDataAdapter adapter3 = new MySqlDataAdapter();
                MySqlCommand command3 = new MySqlCommand("SELECT SUM(`фин_стоимость`) FROM `ос` INNER JOIN `списание` on `ос`.`id_ос`=`списание`.`id_ос` WHERE `статус`= 0 AND `дата_списания` BETWEEN '" + i + "-01-01' AND '" + (i + 1) + "-01-01'", db3.GetConnection());
                adapter3.SelectCommand = command3;
                adapter3.Fill(table3);
                foreach (DataRow item in table3.Rows)
                {
                    try
                    {
                        spisi = Convert.ToDouble(item[0]);
                    }
                    catch { spisi = 0; }
                }
                SIG = SNG + pos - spisi;
                god.Add(i.ToString());
                try
                {
                   SUM.Add(Math.Round(SIG, 2));
                }
                catch { SUM.Add(0); }
            }
            cartesianChart1.AxisX.Clear(); //Очищаем ось X от значений по умолчанию
            cartesianChart1.AxisX.Add(new Axis //Добавляем на ось X значения, через блок инициализатора.
            {
                Title = "Годовая динамика изменения Уставного капитала, руб.",
                Labels = god,
                FontSize = 16

            });
            cartesianChart1.Series.Clear();
            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = SUM,
                    PointGeometrySize=15
                }
            };
        }

        public void Anima()
        {
            if (bunifuDropdown1.Text == "Список поступления/списания ОС")
            {
                panel2.Visible = false;
                bunifuTransition1.ShowSync(panel2, false, BunifuAnimatorNS.Animation.Transparent);
            }
            else
            {
                bunifuTransition1.HideSync(panel2, false, BunifuAnimatorNS.Animation.Transparent);
                panel2.Visible = false;

            }
        }
        //---------------------------------------------------------------------------------------------------
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuDatePicker1_ValueChanged(object sender, EventArgs e)
        {
            bunifuDatePicker2.MinDate = bunifuDatePicker1.Value;
        }

        private void bunifuDatePicker2_ValueChanged(object sender, EventArgs e)
        {
            bunifuDatePicker1.MaxDate = bunifuDatePicker2.Value;
        }
        DateTime d = DateTime.Now;
        string dat1 = ""; string dat2 = ""; string dat3 = ""; string dat4 = "";
        private void bunifuButton5_Click(object sender, EventArgs e) //ДОКУМЕНТ 1
        {
            MessageBox.Show("Начало процесса формирования документа!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DateTime d1 = d;
            string d_s = $"{d1.Day}-{d1.Month}-{d1.Year}";

            d1 = bunifuDatePicker1.Value;
            dat1 = $"{d1.Day}-{d1.Month}-{d1.Year}";
            dat3 = $"{d1.Year}-{d1.Month}-{d1.Day}";
            d1 = bunifuDatePicker2.Value;
            dat2 = $"{d1.Day}-{d1.Month}-{d1.Year}";
            dat4 = $"{d1.Year}-{d1.Month}-{d1.Day}";

            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT `ос`.`наименование`, `ин`, `дата_принятия` FROM `вид_ос` INNER JOIN `ос` ON `вид_ос`.`id_вида_ос`=`ос`.`id_вида_ос` WHERE `дата_принятия` BETWEEN '" + dat3 + "' AND '" + dat4 + "';", db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            foreach (DataRow item in table.Rows)
            {
                name1.Add(item[0].ToString());
                i_n1.Add(item[1].ToString());
                d1 = Convert.ToDateTime(item[2]);
                string d_na = $"{d1.Day}-{d1.Month}-{d1.Year}";
                d_ps1.Add(d_na);
            }

            Database db2 = new Database();
            DataTable table2 = new DataTable();
            MySqlDataAdapter adapter2 = new MySqlDataAdapter();
            MySqlCommand command2 = new MySqlCommand("SELECT `ос`.`наименование`, `ин`, `дата_списания` FROM `списание` INNER JOIN `ос` ON `списание`.`id_ос`=`ос`.`id_ос` INNER JOIN `причины` ON `списание`.`id_причины`=`причины`.`id_причины` WHERE `дата_списания` BETWEEN '" + dat3 + "' AND '" + dat4 + "';", db2.GetConnection());
            adapter2.SelectCommand = command2;
            adapter2.Fill(table2);
            foreach (DataRow item in table2.Rows)
            {
                name2.Add(item[0].ToString());
                i_n2.Add(item[1].ToString());
                d1 = Convert.ToDateTime(item[2]);
                string d_na = $"{d1.Day}-{d1.Month}-{d1.Year}";
                d_ps2.Add(d_na);
            }

            string path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Отчёт по ПС ОС.docx";
            Object newFileName = path;
            var newpathdoc = newFileName;

            //TODO
            var wordAPP = new Word.Application();
            wordAPP.Visible = false;

            var worddocument = wordAPP.Documents.Open(Application.StartupPath + @"\\Word\PrinSpis.docx");

            //ПЕРЕДАЧА ЗНАЧЕНИЙ
            Object missing = Type.Missing;
            var items = new Dictionary<string, string>
                    {
                        {"$n_d$", "14"},
                        {"$d_s$", d_s},
                        {"$dat1$", dat1},
                        {"$dat2$", dat2},
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
            for (int i = 1; i <= name1.Count; i++)
            {
                tab.Rows.Add(Missing.Value);
                tab.Cell(i, 1).Range.Text = name1[n];
                tab.Cell(i, 2).Range.Text = i_n1[n];
                tab.Cell(i, 3).Range.Text = d_ps1[n];
                n++;
            }

            //Таблица 2
            n = 0;
            Word.Table tab2 = worddocument.Tables[3];
            for (int i = 1; i <= name2.Count; i++)
            {
                tab2.Rows.Add(Missing.Value);
                tab2.Cell(i, 1).Range.Text = name2[n];
                tab2.Cell(i, 2).Range.Text = i_n2[n];
                tab2.Cell(i, 3).Range.Text = d_ps2[n];
                n++;
            }

            MessageBox.Show("Отчёт сформирован!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            worddocument.SaveAs(newpathdoc);
            wordAPP.Visible = true;
        }

        List<string> name1 = new List<string>();
        List<string> i_n1 = new List<string>();
        List<string> d_ps1 = new List<string>();
        List<string> name2 = new List<string>();
        List<string> i_n2 = new List<string>();
        List<string> d_ps2 = new List<string>();

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Начало процесса формирования документа!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            double SNG = 0, pos = 0, spisi = 0, SIG = 0;
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT SUM(`фин_стоимость`) FROM `ос` WHERE `статус`= 1 AND `дата_принятия`< '" + DateTime.Now.Year + "-01-01'", db.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            foreach (DataRow item in table.Rows)
            {
                SNG = Convert.ToDouble(item[0]);
            }
            Database db2 = new Database();
            DataTable table2 = new DataTable();
            MySqlDataAdapter adapter2 = new MySqlDataAdapter();
            MySqlCommand command2 = new MySqlCommand("SELECT SUM(`фин_стоимость`) FROM `ос` WHERE `статус`= 1 AND `дата_принятия`> '" + DateTime.Now.Year + "-01-01'", db2.GetConnection());
            adapter2.SelectCommand = command2;
            adapter2.Fill(table2);
            foreach (DataRow item in table2.Rows)
            {
                pos = Convert.ToDouble(item[0]);
            }
            Database db3 = new Database();
            DataTable table3 = new DataTable();
            MySqlDataAdapter adapter3 = new MySqlDataAdapter();
            MySqlCommand command3 = new MySqlCommand("SELECT SUM(`фин_стоимость`) FROM `ос` INNER JOIN `списание` on `ос`.`id_ос`=`списание`.`id_ос` WHERE `статус`= 0 AND `дата_списания`> '" + DateTime.Now.Year + "-01-01'", db3.GetConnection());
            adapter3.SelectCommand = command3;
            adapter3.Fill(table3);
            foreach (DataRow item in table3.Rows)
            {
                spisi = Convert.ToDouble(item[0]);
            }
            SIG = SNG + pos - spisi;
            try
            {
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
                //Книга.
                ExcelWorkBook = ExcelApp.Workbooks.Open(String.Format(@"{0}\Word\Saldo" + ".xlsx", Environment.CurrentDirectory));
                //Таблица.
                ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);
                ExcelApp.Cells[2, 1] = "от " + DateTime.Now.ToShortDateString();

                ExcelApp.Cells[3, 2] = Convert.ToString(Math.Round(SNG, 2));
                ExcelApp.Cells[4, 2] = Convert.ToString(Math.Round(pos, 2));
                ExcelApp.Cells[5, 2] = Convert.ToString(Math.Round(spisi, 2));
                ExcelApp.Cells[6, 2] = Convert.ToString(Math.Round(SIG, 2));

                MessageBox.Show("Отчёт сформирован!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ExcelApp.Visible = true;
                ExcelApp.UserControl = true;
            }
            catch
            {
                MessageBox.Show("При печати возникла ошибка");
            }
        }
    }
}

//# FF2195F2 - синий
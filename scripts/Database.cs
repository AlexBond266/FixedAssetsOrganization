using MySql.Data.MySqlClient;

namespace PR_TRPO.Scripts
{
    class Database
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=kbd");

        public string Users = "";
        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public MySqlConnection GetConnection()
        {
            return connection;
        }

        public string selectOS_bez_spis = "SELECT `ос`.`id_ос`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_ос`.`наименование` AS `Вид ОС`, `дата_принятия` AS `Дата принятия`, `пер_стоимость` AS `Первоначальная стоимость: руб.`, `спи` AS `СПИ: лет`, `норма_ндс` AS `Норма НДС: %`, `сумма_ндс` AS `Сумма НДС: руб.`, `год_сум_а` AS `ГСА: руб.`, `фин_стоимость` AS `Финальная стоимость: руб.`FROM `вид_ос` INNER JOIN `ос` ON `вид_ос`.`id_вида_ос`=`ос`.`id_вида_ос` WHERE `статус`=1;";
        public string selectOS_so_spis = "SELECT `ос`.`id_ос`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_ос`.`наименование` AS `Вид ОС`, `дата_принятия` AS `Дата принятия`, `пер_стоимость` AS `Первоначальная стоимость: руб.`, `спи` AS `СПИ: лет`, `норма_ндс` AS `Норма НДС: %`, `сумма_ндс` AS `Сумма НДС: руб.`, `год_сум_а` AS `ГСА: руб.`, `списание`.`дата_списания` AS `Дата списания` , `фин_стоимость` AS `Финальная стоимость: руб.`FROM `вид_ос` INNER JOIN (`ос` LEFT JOIN `списание` ON `ос`.`id_ос`=`списание`.`id_ос`) ON `вид_ос`.`id_вида_ос`=`ос`.`id_вида_ос`;";
        
        public string selectREM_OS = "SELECT `id_р`, `ремонт`.`id_ос`, `ремонт`.`id_вида_р`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_р`.`наименование` AS `Вид ремонта`, `дата_начала` AS `Дата начала ремонта`, `дата_окончания` AS `Дата окончания ремонта`, `сум_затрат` AS `Сумма затрат на ремонт: руб.` FROM `ремонт` INNER JOIN `ос` ON `ремонт`.`id_ос`=`ос`.`id_ос` INNER JOIN `вид_р` ON `ремонт`.`id_вида_р`=`вид_р`.`id_вида_р`";
        public string selectRemU = "SELECT `id_р`, `ремонт`.`id_ос`, `ремонт`.`id_вида_р`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_р`.`наименование` AS `Вид ремонта`, `дата_начала` AS `Дата начала ремонта`, `дата_окончания` AS `Дата окончания ремонта`, `сум_затрат` AS `Сумма затрат на ремонт: руб.` FROM `ремонт` INNER JOIN `ос` ON `ремонт`.`id_ос`=`ос`.`id_ос` INNER JOIN `вид_р` ON `ремонт`.`id_вида_р`=`вид_р`.`id_вида_р` WHERE `ос`.`статус`=1";
        public string selectRemS = "SELECT `id_р`, `ремонт`.`id_ос`, `ремонт`.`id_вида_р`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `вид_р`.`наименование` AS `Вид ремонта`, `дата_начала` AS `Дата начала ремонта`, `дата_окончания` AS `Дата окончания ремонта`, `сум_затрат` AS `Сумма затрат на ремонт: руб.` FROM `ремонт` INNER JOIN `ос` ON `ремонт`.`id_ос`=`ос`.`id_ос` INNER JOIN `вид_р` ON `ремонт`.`id_вида_р`=`вид_р`.`id_вида_р` WHERE `ос`.`статус`=0";

        public string selectSPIS_OS = "SELECT `id_спис`, `ос`.`id_ос`, `причины`.`id_причины`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, `причины`.`наименование` AS `Причина списания`, `дата_списания` AS `Дата списания` FROM `списание` INNER JOIN `ос` ON `списание`.`id_ос`=`ос`.`id_ос` INNER JOIN `причины` ON `списание`.`id_причины`=`причины`.`id_причины`;";

        public string selectMOL_Zakrep = "SELECT `id_закреп`, `ос`.`id_ос`, `мол`.`id_мол`, `ос`.`наименование` AS `Наименование ОС`, `ин` AS `Инвентарный номер`, CONCAT(`мол`.`ф`,' ',`мол`.`и`,' ',`мол`.`о`) AS `Материально ответственное лицо`, `должности`.`наименование` AS `Должность` FROM `ос` INNER JOIN (`закрепление` INNER JOIN (`мол` INNER JOIN `должности` ON `мол`.`id_должности`=`должности`.`id_должности`) ON `закрепление`.`id_мол`=`мол`.`id_мол` ) ON `ос`.`id_ос`=`закрепление`.`id_ос`;";

        public string selectAvtorisaciaVse = "SELECT `id_эко`, `fio` AS `ФИО работника`, `password` AS `Пароль`,`admin` FROM `авторизация`;";
        public string selectAvtorisaciaEko = "SELECT `id_эко`, `fio` AS `ФИО работника`, `password` AS `Пароль`,`admin` FROM `авторизация` WHERE `admin`=0;";
        public string selectAvtorisaciaAdmin = "SELECT `id_эко`, `fio` AS `ФИО работника`, `password` AS `Пароль`,`admin` FROM `авторизация` WHERE `admin`=1;";

        public string selectMOL = "SELECT `id_мол`, `мол`.`id_должности`, `ф` AS `Фамилия`, `и` AS `Имя`, `о` AS `Отчество`, `должности`.`наименование` AS `Должность` FROM `мол` INNER JOIN `должности` on `мол`.`id_должности`=`должности`.`id_должности`;";

        public string selectSvidOS = "SELECT `id_вида_ос`, `наименование` AS `Наименование вида ОС` FROM `вид_ос`";
        public string selectSdolgnosti = "SELECT `id_должности`, `наименование` AS `Наименование должности` FROM `должности`";
        public string selectSvid_r = "SELECT `id_вида_р`, `наименование` AS `Наименование вида ремонта` FROM `вид_р`";
        public string selectSprichini = "SELECT `id_причины`, `наименование` AS `Причины списания` FROM `причины`";
    }
}

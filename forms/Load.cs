using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR_TRPO.Forms
{
    public partial class Load : Form
    {
        public Load()
        {
            InitializeComponent();
        }
        int a=0;
        private void timer1_Tick(object sender, EventArgs e) //Анимированная загрузка
        {
            if (bunifuCircleProgress1.Value != 100)
            {
                bunifuCircleProgress1.Value += 2;
            }
            if (bunifuCircleProgress1.Value == 100)
            {
                a += 1;
                if (a==3)
                {
                    Login l1 = new Login();
                    l1.Show();
                    timer1.Enabled = false;
                }
            }
           
        }
    }
}

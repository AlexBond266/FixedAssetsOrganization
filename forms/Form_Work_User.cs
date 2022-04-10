using System;
using System.Windows.Forms;
using PR_TRPO.UsersControlers;
using PR_TRPO.Forms;

namespace PR_TRPO
{
    
    public partial class Form_Work_User : Form
    {
        public string User { get; set; }
        public Form_Work_User()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bunifuCustomLabel1.Text += User;
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"{User}, Вы уверены, что хотите закончить работу?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Information); ;
            if (result.ToString() == "Yes")
            {
                this.Close();
            }
        }
        private void ButtonMeny_Click(object sender, EventArgs e)
        {
            if (sidemenu.Width==60)
            {
                sidemenu.Visible = false;
                sidemenu.Width = 240;
                PanelAnimator2.ShowSync(sidemenu);
                LogoAnimator.ShowSync(logo);
            }
            else
            {
                LogoAnimator.HideSync(logo);
                sidemenu.Visible = false;
                sidemenu.Width = 60;
                PanelAnimator1.ShowSync(sidemenu);
            }
        }
        private void addControll(UserControl uc)
        {
            panel3.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panel3.Controls.Add(uc);
            uc.BringToFront();
        }
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            PostyplenieOS uc = new PostyplenieOS();
            addControll(uc);
        }
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            RemontOS uc = new RemontOS();
            addControll(uc);
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            SpisanieOS uc = new SpisanieOS();
            addControll(uc);
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            Zakreplenie uc = new Zakreplenie();
            addControll(uc);
        }
        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            Chart f = new Chart();
            f.Show();
        }
    }   
}
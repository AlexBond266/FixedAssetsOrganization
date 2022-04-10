
namespace PR_TRPO.Forms
{
    partial class Load
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Load));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.bunifuCircleProgress1 = new Bunifu.UI.WinForms.BunifuCircleProgress();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(69)))), ((int)(((byte)(80)))));
            this.panel1.Controls.Add(this.bunifuCircleProgress1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.bunifuCustomLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 600);
            this.panel1.TabIndex = 0;
            // 
            // bunifuCircleProgress1
            // 
            this.bunifuCircleProgress1.Animated = false;
            this.bunifuCircleProgress1.AnimationInterval = 1;
            this.bunifuCircleProgress1.AnimationSpeed = 1;
            this.bunifuCircleProgress1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuCircleProgress1.CircleMargin = 10;
            this.bunifuCircleProgress1.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Bold);
            this.bunifuCircleProgress1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.bunifuCircleProgress1.IsPercentage = true;
            this.bunifuCircleProgress1.LineProgressThickness = 8;
            this.bunifuCircleProgress1.LineThickness = 5;
            this.bunifuCircleProgress1.Location = new System.Drawing.Point(290, 276);
            this.bunifuCircleProgress1.Name = "bunifuCircleProgress1";
            this.bunifuCircleProgress1.ProgressBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(69)))), ((int)(((byte)(80)))));
            this.bunifuCircleProgress1.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.bunifuCircleProgress1.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.bunifuCircleProgress1.Size = new System.Drawing.Size(222, 222);
            this.bunifuCircleProgress1.Step = 1;
            this.bunifuCircleProgress1.SubScriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.bunifuCircleProgress1.SubScriptMargin = new System.Windows.Forms.Padding(5, -35, 0, 0);
            this.bunifuCircleProgress1.SubScriptText = "";
            this.bunifuCircleProgress1.SuperScriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.bunifuCircleProgress1.SuperScriptMargin = new System.Windows.Forms.Padding(5, 50, 0, 0);
            this.bunifuCircleProgress1.SuperScriptText = "%";
            this.bunifuCircleProgress1.TabIndex = 16;
            this.bunifuCircleProgress1.TextMargin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(340, 150);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Century Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(106, 21);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(581, 126);
            this.bunifuCustomLabel1.TabIndex = 4;
            this.bunifuCustomLabel1.Text = "Учёт\r\nосновных средств";
            this.bunifuCustomLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 45;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Load
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Load";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Panel panel1;
        private Bunifu.UI.WinForms.BunifuCircleProgress bunifuCircleProgress1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private System.Windows.Forms.Timer timer1;
    }
}
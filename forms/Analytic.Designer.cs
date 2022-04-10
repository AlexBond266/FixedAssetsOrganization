
namespace PR_TRPO.Forms
{
    partial class Chart
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
            BunifuAnimatorNS.Animation animation2 = new BunifuAnimatorNS.Animation();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges4 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chart));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges3 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bunifuLabel1 = new Bunifu.UI.WinForms.BunifuLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bunifuDropdown1 = new Bunifu.UI.WinForms.BunifuDropdown();
            this.bunifuLabel6 = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuTransition1 = new BunifuAnimatorNS.BunifuTransition(this.components);
            this.bunifuImageButton1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bunifuButton5 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.bunifuLabel9 = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuLabel8 = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuDatePicker2 = new Bunifu.UI.WinForms.BunifuDatePicker();
            this.bunifuDatePicker1 = new Bunifu.UI.WinForms.BunifuDatePicker();
            this.bunifuLabel7 = new Bunifu.UI.WinForms.BunifuLabel();
            this.pieChart1 = new LiveCharts.WinForms.PieChart();
            this.bunifuButton1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cartesianChart1
            // 
            this.bunifuTransition1.SetDecoration(this.cartesianChart1, BunifuAnimatorNS.DecorationType.None);
            this.cartesianChart1.ForeColor = System.Drawing.Color.White;
            this.cartesianChart1.Location = new System.Drawing.Point(110, 78);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(978, 367);
            this.cartesianChart1.TabIndex = 0;
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // bunifuLabel1
            // 
            this.bunifuLabel1.AutoEllipsis = false;
            this.bunifuLabel1.AutoSize = false;
            this.bunifuLabel1.AutoSizeHeightOnly = true;
            this.bunifuLabel1.CursorType = null;
            this.bunifuTransition1.SetDecoration(this.bunifuLabel1, BunifuAnimatorNS.DecorationType.None);
            this.bunifuLabel1.Font = new System.Drawing.Font("Century Gothic", 20.25F);
            this.bunifuLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.bunifuLabel1.Location = new System.Drawing.Point(504, 18);
            this.bunifuLabel1.Name = "bunifuLabel1";
            this.bunifuLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel1.Size = new System.Drawing.Size(191, 42);
            this.bunifuLabel1.TabIndex = 1;
            this.bunifuLabel1.Text = "Аналитика";
            this.bunifuLabel1.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel1.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bunifuDropdown1);
            this.panel1.Controls.Add(this.bunifuLabel6);
            this.bunifuTransition1.SetDecoration(this.panel1, BunifuAnimatorNS.DecorationType.None);
            this.panel1.Location = new System.Drawing.Point(110, 480);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(523, 105);
            this.panel1.TabIndex = 65;
            // 
            // bunifuDropdown1
            // 
            this.bunifuDropdown1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(69)))), ((int)(((byte)(80)))));
            this.bunifuDropdown1.BorderRadius = 1;
            this.bunifuDropdown1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.bunifuDropdown1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuTransition1.SetDecoration(this.bunifuDropdown1, BunifuAnimatorNS.DecorationType.None);
            this.bunifuDropdown1.Direction = Bunifu.UI.WinForms.BunifuDropdown.Directions.Down;
            this.bunifuDropdown1.DisabledColor = System.Drawing.Color.Gray;
            this.bunifuDropdown1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.bunifuDropdown1.DropdownBorderThickness = Bunifu.UI.WinForms.BunifuDropdown.BorderThickness.Thick;
            this.bunifuDropdown1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bunifuDropdown1.DropDownTextAlign = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Left;
            this.bunifuDropdown1.FillDropDown = false;
            this.bunifuDropdown1.FillIndicator = false;
            this.bunifuDropdown1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bunifuDropdown1.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bunifuDropdown1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.bunifuDropdown1.FormattingEnabled = true;
            this.bunifuDropdown1.Icon = null;
            this.bunifuDropdown1.IndicatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.bunifuDropdown1.IndicatorLocation = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.bunifuDropdown1.ItemBackColor = System.Drawing.Color.White;
            this.bunifuDropdown1.ItemBorderColor = System.Drawing.Color.White;
            this.bunifuDropdown1.ItemForeColor = System.Drawing.Color.DarkSlateGray;
            this.bunifuDropdown1.ItemHeight = 26;
            this.bunifuDropdown1.ItemHighLightColor = System.Drawing.Color.PowderBlue;
            this.bunifuDropdown1.Items.AddRange(new object[] {
            "Список поступления/списания ОС",
            "Оборотное сальдо по итогам года",
            "Отношение ответственности МОЛ",
            "Годовая динамика поступления ОС",
            "Годовая динамика списания ОС",
            "Годовая динамика затрат на ремонтные работы",
            "Годовая динамика Уставного Капитала"});
            this.bunifuDropdown1.Location = new System.Drawing.Point(3, 55);
            this.bunifuDropdown1.Name = "bunifuDropdown1";
            this.bunifuDropdown1.Size = new System.Drawing.Size(517, 32);
            this.bunifuDropdown1.TabIndex = 9;
            this.bunifuDropdown1.Text = null;
            this.bunifuDropdown1.SelectedIndexChanged += new System.EventHandler(this.bunifuDropdown1_SelectedIndexChanged);
            // 
            // bunifuLabel6
            // 
            this.bunifuLabel6.AutoEllipsis = false;
            this.bunifuLabel6.AutoSize = false;
            this.bunifuLabel6.AutoSizeHeightOnly = true;
            this.bunifuLabel6.CursorType = null;
            this.bunifuTransition1.SetDecoration(this.bunifuLabel6, BunifuAnimatorNS.DecorationType.None);
            this.bunifuLabel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.bunifuLabel6.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.bunifuLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.bunifuLabel6.Location = new System.Drawing.Point(0, 0);
            this.bunifuLabel6.Name = "bunifuLabel6";
            this.bunifuLabel6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel6.Size = new System.Drawing.Size(523, 44);
            this.bunifuLabel6.TabIndex = 1;
            this.bunifuLabel6.Text = "Выберите направление осуществления аналитических операций.";
            this.bunifuLabel6.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.bunifuLabel6.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // bunifuTransition1
            // 
            this.bunifuTransition1.AnimationType = BunifuAnimatorNS.AnimationType.VertSlide;
            this.bunifuTransition1.Cursor = null;
            animation2.AnimateOnlyDifferences = true;
            animation2.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.BlindCoeff")));
            animation2.LeafCoeff = 0F;
            animation2.MaxTime = 1F;
            animation2.MinTime = 0F;
            animation2.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.MosaicCoeff")));
            animation2.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation2.MosaicShift")));
            animation2.MosaicSize = 0;
            animation2.Padding = new System.Windows.Forms.Padding(0);
            animation2.RotateCoeff = 0F;
            animation2.RotateLimit = 0F;
            animation2.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.ScaleCoeff")));
            animation2.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.SlideCoeff")));
            animation2.TimeCoeff = 0F;
            animation2.TransparencyCoeff = 0F;
            this.bunifuTransition1.DefaultAnimation = animation2;
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuTransition1.SetDecoration(this.bunifuImageButton1, BunifuAnimatorNS.DecorationType.None);
            this.bunifuImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton1.Image")));
            this.bunifuImageButton1.ImageActive = null;
            this.bunifuImageButton1.Location = new System.Drawing.Point(1143, 18);
            this.bunifuImageButton1.Name = "bunifuImageButton1";
            this.bunifuImageButton1.Size = new System.Drawing.Size(35, 39);
            this.bunifuImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton1.TabIndex = 66;
            this.bunifuImageButton1.TabStop = false;
            this.bunifuImageButton1.Zoom = 10;
            this.bunifuImageButton1.Click += new System.EventHandler(this.bunifuImageButton1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bunifuButton5);
            this.panel2.Controls.Add(this.bunifuLabel9);
            this.panel2.Controls.Add(this.bunifuLabel8);
            this.panel2.Controls.Add(this.bunifuDatePicker2);
            this.panel2.Controls.Add(this.bunifuDatePicker1);
            this.panel2.Controls.Add(this.bunifuLabel7);
            this.bunifuTransition1.SetDecoration(this.panel2, BunifuAnimatorNS.DecorationType.None);
            this.panel2.Location = new System.Drawing.Point(639, 480);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(449, 161);
            this.panel2.TabIndex = 68;
            this.panel2.Visible = false;
            // 
            // bunifuButton5
            // 
            this.bunifuButton5.AllowToggling = false;
            this.bunifuButton5.AnimationSpeed = 200;
            this.bunifuButton5.AutoGenerateColors = false;
            this.bunifuButton5.BackColor = System.Drawing.Color.Transparent;
            this.bunifuButton5.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(90)))));
            this.bunifuButton5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuButton5.BackgroundImage")));
            this.bunifuButton5.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.bunifuButton5.ButtonText = "Сформировать документ";
            this.bunifuButton5.ButtonTextMarginLeft = 0;
            this.bunifuButton5.ColorContrastOnClick = 45;
            this.bunifuButton5.ColorContrastOnHover = 45;
            this.bunifuButton5.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges4.BottomLeft = true;
            borderEdges4.BottomRight = true;
            borderEdges4.TopLeft = true;
            borderEdges4.TopRight = true;
            this.bunifuButton5.CustomizableEdges = borderEdges4;
            this.bunifuTransition1.SetDecoration(this.bunifuButton5, BunifuAnimatorNS.DecorationType.None);
            this.bunifuButton5.DialogResult = System.Windows.Forms.DialogResult.None;
            this.bunifuButton5.DisabledBorderColor = System.Drawing.Color.Empty;
            this.bunifuButton5.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.bunifuButton5.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.bunifuButton5.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.bunifuButton5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.bunifuButton5.ForeColor = System.Drawing.Color.White;
            this.bunifuButton5.IconLeftCursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuButton5.IconMarginLeft = 11;
            this.bunifuButton5.IconPadding = 10;
            this.bunifuButton5.IconRightCursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuButton5.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(90)))));
            this.bunifuButton5.IdleBorderRadius = 3;
            this.bunifuButton5.IdleBorderThickness = 1;
            this.bunifuButton5.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(90)))));
            this.bunifuButton5.IdleIconLeftImage = null;
            this.bunifuButton5.IdleIconRightImage = null;
            this.bunifuButton5.IndicateFocus = false;
            this.bunifuButton5.Location = new System.Drawing.Point(134, 119);
            this.bunifuButton5.Name = "bunifuButton5";
            this.bunifuButton5.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.bunifuButton5.onHoverState.BorderRadius = 3;
            this.bunifuButton5.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.bunifuButton5.onHoverState.BorderThickness = 1;
            this.bunifuButton5.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.bunifuButton5.onHoverState.ForeColor = System.Drawing.Color.White;
            this.bunifuButton5.onHoverState.IconLeftImage = null;
            this.bunifuButton5.onHoverState.IconRightImage = null;
            this.bunifuButton5.OnIdleState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(90)))));
            this.bunifuButton5.OnIdleState.BorderRadius = 3;
            this.bunifuButton5.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.bunifuButton5.OnIdleState.BorderThickness = 1;
            this.bunifuButton5.OnIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(90)))));
            this.bunifuButton5.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.bunifuButton5.OnIdleState.IconLeftImage = null;
            this.bunifuButton5.OnIdleState.IconRightImage = null;
            this.bunifuButton5.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.bunifuButton5.OnPressedState.BorderRadius = 3;
            this.bunifuButton5.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.bunifuButton5.OnPressedState.BorderThickness = 1;
            this.bunifuButton5.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.bunifuButton5.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.bunifuButton5.OnPressedState.IconLeftImage = null;
            this.bunifuButton5.OnPressedState.IconRightImage = null;
            this.bunifuButton5.Size = new System.Drawing.Size(216, 35);
            this.bunifuButton5.TabIndex = 63;
            this.bunifuButton5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuButton5.TextMarginLeft = 0;
            this.bunifuButton5.UseDefaultRadiusAndThickness = true;
            this.bunifuButton5.Click += new System.EventHandler(this.bunifuButton5_Click);
            // 
            // bunifuLabel9
            // 
            this.bunifuLabel9.AutoEllipsis = false;
            this.bunifuLabel9.AutoSize = false;
            this.bunifuLabel9.CursorType = null;
            this.bunifuTransition1.SetDecoration(this.bunifuLabel9, BunifuAnimatorNS.DecorationType.None);
            this.bunifuLabel9.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.bunifuLabel9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.bunifuLabel9.Location = new System.Drawing.Point(88, 90);
            this.bunifuLabel9.Name = "bunifuLabel9";
            this.bunifuLabel9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel9.Size = new System.Drawing.Size(40, 23);
            this.bunifuLabel9.TabIndex = 62;
            this.bunifuLabel9.Text = "ПО";
            this.bunifuLabel9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuLabel9.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // bunifuLabel8
            // 
            this.bunifuLabel8.AutoEllipsis = false;
            this.bunifuLabel8.AutoSize = false;
            this.bunifuLabel8.CursorType = null;
            this.bunifuTransition1.SetDecoration(this.bunifuLabel8, BunifuAnimatorNS.DecorationType.None);
            this.bunifuLabel8.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.bunifuLabel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.bunifuLabel8.Location = new System.Drawing.Point(88, 51);
            this.bunifuLabel8.Name = "bunifuLabel8";
            this.bunifuLabel8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel8.Size = new System.Drawing.Size(40, 23);
            this.bunifuLabel8.TabIndex = 62;
            this.bunifuLabel8.Text = "С";
            this.bunifuLabel8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuLabel8.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // bunifuDatePicker2
            // 
            this.bunifuDatePicker2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(69)))), ((int)(((byte)(80)))));
            this.bunifuDatePicker2.BorderRadius = 1;
            this.bunifuDatePicker2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.bunifuDatePicker2.CustomFormat = "dd.MM.yyyy";
            this.bunifuDatePicker2.DateBorderThickness = Bunifu.UI.WinForms.BunifuDatePicker.BorderThickness.Thick;
            this.bunifuDatePicker2.DateTextAlign = Bunifu.UI.WinForms.BunifuDatePicker.TextAlign.Left;
            this.bunifuTransition1.SetDecoration(this.bunifuDatePicker2, BunifuAnimatorNS.DecorationType.None);
            this.bunifuDatePicker2.DisabledColor = System.Drawing.Color.Gray;
            this.bunifuDatePicker2.DisplayWeekNumbers = false;
            this.bunifuDatePicker2.DPHeight = 0;
            this.bunifuDatePicker2.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.bunifuDatePicker2.FillDatePicker = false;
            this.bunifuDatePicker2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bunifuDatePicker2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.bunifuDatePicker2.Icon = ((System.Drawing.Image)(resources.GetObject("bunifuDatePicker2.Icon")));
            this.bunifuDatePicker2.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.bunifuDatePicker2.IconLocation = Bunifu.UI.WinForms.BunifuDatePicker.Indicator.Right;
            this.bunifuDatePicker2.Location = new System.Drawing.Point(134, 81);
            this.bunifuDatePicker2.MaxDate = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
            this.bunifuDatePicker2.MinDate = new System.DateTime(1995, 1, 1, 0, 0, 0, 0);
            this.bunifuDatePicker2.MinimumSize = new System.Drawing.Size(253, 32);
            this.bunifuDatePicker2.Name = "bunifuDatePicker2";
            this.bunifuDatePicker2.Size = new System.Drawing.Size(253, 32);
            this.bunifuDatePicker2.TabIndex = 61;
            this.bunifuDatePicker2.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.bunifuDatePicker2.ValueChanged += new System.EventHandler(this.bunifuDatePicker2_ValueChanged);
            // 
            // bunifuDatePicker1
            // 
            this.bunifuDatePicker1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(69)))), ((int)(((byte)(80)))));
            this.bunifuDatePicker1.BorderRadius = 1;
            this.bunifuDatePicker1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.bunifuDatePicker1.CustomFormat = "dd.MM.yyyy";
            this.bunifuDatePicker1.DateBorderThickness = Bunifu.UI.WinForms.BunifuDatePicker.BorderThickness.Thick;
            this.bunifuDatePicker1.DateTextAlign = Bunifu.UI.WinForms.BunifuDatePicker.TextAlign.Left;
            this.bunifuTransition1.SetDecoration(this.bunifuDatePicker1, BunifuAnimatorNS.DecorationType.None);
            this.bunifuDatePicker1.DisabledColor = System.Drawing.Color.Gray;
            this.bunifuDatePicker1.DisplayWeekNumbers = false;
            this.bunifuDatePicker1.DPHeight = 0;
            this.bunifuDatePicker1.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.bunifuDatePicker1.FillDatePicker = false;
            this.bunifuDatePicker1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bunifuDatePicker1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.bunifuDatePicker1.Icon = ((System.Drawing.Image)(resources.GetObject("bunifuDatePicker1.Icon")));
            this.bunifuDatePicker1.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.bunifuDatePicker1.IconLocation = Bunifu.UI.WinForms.BunifuDatePicker.Indicator.Right;
            this.bunifuDatePicker1.Location = new System.Drawing.Point(134, 42);
            this.bunifuDatePicker1.MaxDate = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
            this.bunifuDatePicker1.MinDate = new System.DateTime(1995, 1, 1, 0, 0, 0, 0);
            this.bunifuDatePicker1.MinimumSize = new System.Drawing.Size(253, 32);
            this.bunifuDatePicker1.Name = "bunifuDatePicker1";
            this.bunifuDatePicker1.Size = new System.Drawing.Size(253, 32);
            this.bunifuDatePicker1.TabIndex = 61;
            this.bunifuDatePicker1.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.bunifuDatePicker1.ValueChanged += new System.EventHandler(this.bunifuDatePicker1_ValueChanged);
            // 
            // bunifuLabel7
            // 
            this.bunifuLabel7.AutoEllipsis = false;
            this.bunifuLabel7.AutoSize = false;
            this.bunifuLabel7.CursorType = null;
            this.bunifuTransition1.SetDecoration(this.bunifuLabel7, BunifuAnimatorNS.DecorationType.None);
            this.bunifuLabel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.bunifuLabel7.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.bunifuLabel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.bunifuLabel7.Location = new System.Drawing.Point(0, 0);
            this.bunifuLabel7.Name = "bunifuLabel7";
            this.bunifuLabel7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel7.Size = new System.Drawing.Size(449, 40);
            this.bunifuLabel7.TabIndex = 1;
            this.bunifuLabel7.Text = "Промежуток времени для поступления/списания ОС";
            this.bunifuLabel7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuLabel7.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // pieChart1
            // 
            this.bunifuTransition1.SetDecoration(this.pieChart1, BunifuAnimatorNS.DecorationType.None);
            this.pieChart1.Location = new System.Drawing.Point(110, 78);
            this.pieChart1.Name = "pieChart1";
            this.pieChart1.Size = new System.Drawing.Size(978, 367);
            this.pieChart1.TabIndex = 69;
            this.pieChart1.Text = "pieChart1";
            // 
            // bunifuButton1
            // 
            this.bunifuButton1.AllowToggling = false;
            this.bunifuButton1.AnimationSpeed = 200;
            this.bunifuButton1.AutoGenerateColors = false;
            this.bunifuButton1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuButton1.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(90)))));
            this.bunifuButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuButton1.BackgroundImage")));
            this.bunifuButton1.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.bunifuButton1.ButtonText = "Закрытие итогов года";
            this.bunifuButton1.ButtonTextMarginLeft = 0;
            this.bunifuButton1.ColorContrastOnClick = 45;
            this.bunifuButton1.ColorContrastOnHover = 45;
            this.bunifuButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges3.BottomLeft = true;
            borderEdges3.BottomRight = true;
            borderEdges3.TopLeft = true;
            borderEdges3.TopRight = true;
            this.bunifuButton1.CustomizableEdges = borderEdges3;
            this.bunifuTransition1.SetDecoration(this.bunifuButton1, BunifuAnimatorNS.DecorationType.None);
            this.bunifuButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.bunifuButton1.DisabledBorderColor = System.Drawing.Color.Empty;
            this.bunifuButton1.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.bunifuButton1.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.bunifuButton1.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.bunifuButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.bunifuButton1.ForeColor = System.Drawing.Color.White;
            this.bunifuButton1.IconLeftCursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuButton1.IconMarginLeft = 11;
            this.bunifuButton1.IconPadding = 10;
            this.bunifuButton1.IconRightCursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuButton1.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(90)))));
            this.bunifuButton1.IdleBorderRadius = 3;
            this.bunifuButton1.IdleBorderThickness = 1;
            this.bunifuButton1.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(90)))));
            this.bunifuButton1.IdleIconLeftImage = null;
            this.bunifuButton1.IdleIconRightImage = null;
            this.bunifuButton1.IndicateFocus = false;
            this.bunifuButton1.Location = new System.Drawing.Point(246, 591);
            this.bunifuButton1.Name = "bunifuButton1";
            this.bunifuButton1.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.bunifuButton1.onHoverState.BorderRadius = 3;
            this.bunifuButton1.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.bunifuButton1.onHoverState.BorderThickness = 1;
            this.bunifuButton1.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.bunifuButton1.onHoverState.ForeColor = System.Drawing.Color.White;
            this.bunifuButton1.onHoverState.IconLeftImage = null;
            this.bunifuButton1.onHoverState.IconRightImage = null;
            this.bunifuButton1.OnIdleState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(90)))));
            this.bunifuButton1.OnIdleState.BorderRadius = 3;
            this.bunifuButton1.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.bunifuButton1.OnIdleState.BorderThickness = 1;
            this.bunifuButton1.OnIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(90)))));
            this.bunifuButton1.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.bunifuButton1.OnIdleState.IconLeftImage = null;
            this.bunifuButton1.OnIdleState.IconRightImage = null;
            this.bunifuButton1.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.bunifuButton1.OnPressedState.BorderRadius = 3;
            this.bunifuButton1.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.bunifuButton1.OnPressedState.BorderThickness = 1;
            this.bunifuButton1.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.bunifuButton1.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.bunifuButton1.OnPressedState.IconLeftImage = null;
            this.bunifuButton1.OnPressedState.IconRightImage = null;
            this.bunifuButton1.Size = new System.Drawing.Size(216, 35);
            this.bunifuButton1.TabIndex = 70;
            this.bunifuButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuButton1.TextMarginLeft = 0;
            this.bunifuButton1.UseDefaultRadiusAndThickness = true;
            this.bunifuButton1.Visible = false;
            this.bunifuButton1.Click += new System.EventHandler(this.bunifuButton1_Click);
            // 
            // Chart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(69)))), ((int)(((byte)(80)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.bunifuButton1);
            this.Controls.Add(this.pieChart1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.bunifuImageButton1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bunifuLabel1);
            this.Controls.Add(this.cartesianChart1);
            this.bunifuTransition1.SetDecoration(this, BunifuAnimatorNS.DecorationType.None);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Chart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel1;
        private System.Windows.Forms.Panel panel1;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel6;
        public Bunifu.UI.WinForms.BunifuDropdown bunifuDropdown1;
        private BunifuAnimatorNS.BunifuTransition bunifuTransition1;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton1;
        private System.Windows.Forms.Panel panel2;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel7;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel9;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel8;
        private Bunifu.UI.WinForms.BunifuDatePicker bunifuDatePicker2;
        private Bunifu.UI.WinForms.BunifuDatePicker bunifuDatePicker1;
        private LiveCharts.WinForms.PieChart pieChart1;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton bunifuButton1;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton bunifuButton5;
    }
}
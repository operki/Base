namespace WindowsFormsApp1
{
    partial class ConnectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectForm));
            this.tbpConnectBD = new System.Windows.Forms.Panel();
            this.tbpUserPass = new System.Windows.Forms.Panel();
            this.lbUserPasswords = new System.Windows.Forms.ListBox();
            this.tbUserPass = new System.Windows.Forms.TextBox();
            this.tbConnectBD = new System.Windows.Forms.TextBox();
            this.pWindowState = new System.Windows.Forms.Panel();
            this.bMin = new System.Windows.Forms.Button();
            this.bExit = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tPass = new System.Windows.Forms.Timer(this.components);
            this.lbUserNames = new System.Windows.Forms.ListBox();
            this.bTerminal = new System.Windows.Forms.Button();
            this.tbpbConnectBD = new System.Windows.Forms.PictureBox();
            this.pbPassLook = new System.Windows.Forms.PictureBox();
            this.bConnectBD = new System.Windows.Forms.Button();
            this.lbpbUserNames = new System.Windows.Forms.PictureBox();
            this.tbpbUserPass = new System.Windows.Forms.PictureBox();
            this.lbUserNamesScroll = new WindowsFormsApp1.FlatScrollBar();
            this.pWindowState.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbpbConnectBD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPassLook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbpbUserNames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbpbUserPass)).BeginInit();
            this.SuspendLayout();
            // 
            // tbpConnectBD
            // 
            this.tbpConnectBD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbpConnectBD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.tbpConnectBD.Location = new System.Drawing.Point(54, 70);
            this.tbpConnectBD.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbpConnectBD.Name = "tbpConnectBD";
            this.tbpConnectBD.Size = new System.Drawing.Size(316, 1);
            this.tbpConnectBD.TabIndex = 57;
            // 
            // tbpUserPass
            // 
            this.tbpUserPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbpUserPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.tbpUserPass.Location = new System.Drawing.Point(54, 478);
            this.tbpUserPass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbpUserPass.Name = "tbpUserPass";
            this.tbpUserPass.Size = new System.Drawing.Size(277, 1);
            this.tbpUserPass.TabIndex = 52;
            // 
            // lbUserPasswords
            // 
            this.lbUserPasswords.BackColor = System.Drawing.Color.White;
            this.lbUserPasswords.FormattingEnabled = true;
            this.lbUserPasswords.IntegralHeight = false;
            this.lbUserPasswords.ItemHeight = 23;
            this.lbUserPasswords.Items.AddRange(new object[] {
            "111",
            "222"});
            this.lbUserPasswords.Location = new System.Drawing.Point(12, 180);
            this.lbUserPasswords.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbUserPasswords.Name = "lbUserPasswords";
            this.lbUserPasswords.Size = new System.Drawing.Size(63, 62);
            this.lbUserPasswords.TabIndex = 51;
            this.lbUserPasswords.TabStop = false;
            this.lbUserPasswords.Visible = false;
            // 
            // tbUserPass
            // 
            this.tbUserPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUserPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.tbUserPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbUserPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.tbUserPass.Location = new System.Drawing.Point(61, 454);
            this.tbUserPass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbUserPass.Name = "tbUserPass";
            this.tbUserPass.PasswordChar = '*';
            this.tbUserPass.Size = new System.Drawing.Size(267, 23);
            this.tbUserPass.TabIndex = 1;
            this.tbUserPass.Text = "настя";
            this.tbUserPass.TextChanged += new System.EventHandler(this.lbUserNames_SelectedIndexChanged);
            this.tbUserPass.Enter += new System.EventHandler(this.tbUserPass_Enter);
            this.tbUserPass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ConnectForm_KeyPress);
            this.tbUserPass.Leave += new System.EventHandler(this.tbUserPass_Leave);
            this.tbUserPass.MouseEnter += new System.EventHandler(this.tbUserPass_Enter);
            this.tbUserPass.MouseLeave += new System.EventHandler(this.tbUserPass_Leave);
            // 
            // tbConnectBD
            // 
            this.tbConnectBD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbConnectBD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.tbConnectBD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbConnectBD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.tbConnectBD.Location = new System.Drawing.Point(61, 46);
            this.tbConnectBD.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbConnectBD.Name = "tbConnectBD";
            this.tbConnectBD.Size = new System.Drawing.Size(309, 23);
            this.tbConnectBD.TabIndex = 3;
            this.tbConnectBD.Text = "NOK-PC-1\\MSSQL";
            this.tbConnectBD.Enter += new System.EventHandler(this.tbConnectBD_Enter);
            this.tbConnectBD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ConnectForm_KeyPress);
            this.tbConnectBD.Leave += new System.EventHandler(this.tbConnectBD_Leave);
            this.tbConnectBD.MouseEnter += new System.EventHandler(this.tbConnectBD_Enter);
            this.tbConnectBD.MouseLeave += new System.EventHandler(this.tbConnectBD_Leave);
            // 
            // pWindowState
            // 
            this.pWindowState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pWindowState.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pWindowState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.pWindowState.Controls.Add(this.bMin);
            this.pWindowState.Controls.Add(this.bExit);
            this.pWindowState.Controls.Add(this.button2);
            this.pWindowState.Location = new System.Drawing.Point(1, 1);
            this.pWindowState.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pWindowState.Name = "pWindowState";
            this.pWindowState.Size = new System.Drawing.Size(377, 30);
            this.pWindowState.TabIndex = 59;
            this.pWindowState.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pWindowState_MouseDown);
            // 
            // bMin
            // 
            this.bMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bMin.FlatAppearance.BorderSize = 0;
            this.bMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.bMin.Image = global::WindowsFormsApp1.Properties.Resources.minimum;
            this.bMin.Location = new System.Drawing.Point(294, 0);
            this.bMin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bMin.Name = "bMin";
            this.bMin.Size = new System.Drawing.Size(40, 30);
            this.bMin.TabIndex = 5;
            this.bMin.TabStop = false;
            this.bMin.UseVisualStyleBackColor = false;
            this.bMin.Click += new System.EventHandler(this.bMin_Click);
            this.bMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ConnectForm_KeyPress);
            this.bMin.MouseEnter += new System.EventHandler(this.bMin_MouseEnter);
            this.bMin.MouseLeave += new System.EventHandler(this.bMin_MouseLeave);
            // 
            // bExit
            // 
            this.bExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bExit.FlatAppearance.BorderSize = 0;
            this.bExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.bExit.Image = global::WindowsFormsApp1.Properties.Resources.close30;
            this.bExit.Location = new System.Drawing.Point(337, 0);
            this.bExit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bExit.Name = "bExit";
            this.bExit.Size = new System.Drawing.Size(40, 30);
            this.bExit.TabIndex = 6;
            this.bExit.TabStop = false;
            this.bExit.UseVisualStyleBackColor = false;
            this.bExit.Click += new System.EventHandler(this.bExit_Click);
            this.bExit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ConnectForm_KeyPress);
            // 
            // button2
            // 
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(1437, 4);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(37, 36);
            this.button2.TabIndex = 0;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // tPass
            // 
            this.tPass.Interval = 2000;
            this.tPass.Tick += new System.EventHandler(this.tPass_Tick);
            // 
            // lbUserNames
            // 
            this.lbUserNames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbUserNames.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.lbUserNames.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbUserNames.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbUserNames.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lbUserNames.FormattingEnabled = true;
            this.lbUserNames.IntegralHeight = false;
            this.lbUserNames.ItemHeight = 30;
            this.lbUserNames.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.lbUserNames.Location = new System.Drawing.Point(54, 90);
            this.lbUserNames.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbUserNames.Name = "lbUserNames";
            this.lbUserNames.Size = new System.Drawing.Size(317, 341);
            this.lbUserNames.TabIndex = 0;
            this.lbUserNames.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbUserNames_DrawItem);
            this.lbUserNames.SelectedIndexChanged += new System.EventHandler(this.lbUserNames_SelectedIndexChanged);
            this.lbUserNames.LocationChanged += new System.EventHandler(this.lbUserNames_LocationChanged);
            this.lbUserNames.DoubleClick += new System.EventHandler(this.lbUserNames_DoubleClick);
            this.lbUserNames.Enter += new System.EventHandler(this.lbUserNames_Enter);
            this.lbUserNames.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ConnectForm_KeyPress);
            this.lbUserNames.Leave += new System.EventHandler(this.lbUserNames_Leave);
            this.lbUserNames.MouseEnter += new System.EventHandler(this.lbUserNames_Enter);
            this.lbUserNames.MouseLeave += new System.EventHandler(this.lbUserNames_Leave);
            // 
            // bTerminal
            // 
            this.bTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bTerminal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.bTerminal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bTerminal.FlatAppearance.BorderSize = 0;
            this.bTerminal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bTerminal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.bTerminal.Image = global::WindowsFormsApp1.Properties.Resources.terminal40;
            this.bTerminal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bTerminal.Location = new System.Drawing.Point(12, 558);
            this.bTerminal.Margin = new System.Windows.Forms.Padding(0);
            this.bTerminal.Name = "bTerminal";
            this.bTerminal.Size = new System.Drawing.Size(359, 51);
            this.bTerminal.TabIndex = 71;
            this.bTerminal.Text = "        Терминал";
            this.bTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bTerminal.UseVisualStyleBackColor = false;
            this.bTerminal.Click += new System.EventHandler(this.bTerminal_Click);
            // 
            // tbpbConnectBD
            // 
            this.tbpbConnectBD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tbpbConnectBD.Image = global::WindowsFormsApp1.Properties.Resources.link;
            this.tbpbConnectBD.Location = new System.Drawing.Point(12, 41);
            this.tbpbConnectBD.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbpbConnectBD.Name = "tbpbConnectBD";
            this.tbpbConnectBD.Size = new System.Drawing.Size(30, 30);
            this.tbpbConnectBD.TabIndex = 56;
            this.tbpbConnectBD.TabStop = false;
            // 
            // pbPassLook
            // 
            this.pbPassLook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbPassLook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbPassLook.Image = global::WindowsFormsApp1.Properties.Resources.pass;
            this.pbPassLook.Location = new System.Drawing.Point(341, 449);
            this.pbPassLook.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbPassLook.Name = "pbPassLook";
            this.pbPassLook.Size = new System.Drawing.Size(30, 30);
            this.pbPassLook.TabIndex = 55;
            this.pbPassLook.TabStop = false;
            this.pbPassLook.MouseLeave += new System.EventHandler(this.pbPassLook_MouseLeave);
            this.pbPassLook.MouseHover += new System.EventHandler(this.pbPassLook_MouseHover_1);
            // 
            // bConnectBD
            // 
            this.bConnectBD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bConnectBD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(93)))), ((int)(((byte)(165)))));
            this.bConnectBD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bConnectBD.FlatAppearance.BorderSize = 0;
            this.bConnectBD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bConnectBD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.bConnectBD.Image = global::WindowsFormsApp1.Properties.Resources.connect40;
            this.bConnectBD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bConnectBD.Location = new System.Drawing.Point(12, 494);
            this.bConnectBD.Margin = new System.Windows.Forms.Padding(0);
            this.bConnectBD.Name = "bConnectBD";
            this.bConnectBD.Size = new System.Drawing.Size(358, 50);
            this.bConnectBD.TabIndex = 2;
            this.bConnectBD.Text = "        Подключиться";
            this.bConnectBD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bConnectBD.UseVisualStyleBackColor = false;
            this.bConnectBD.Click += new System.EventHandler(this.bConnectBD_Click);
            this.bConnectBD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ConnectForm_KeyPress);
            // 
            // lbpbUserNames
            // 
            this.lbpbUserNames.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.lbpbUserNames.Image = global::WindowsFormsApp1.Properties.Resources.user;
            this.lbpbUserNames.Location = new System.Drawing.Point(12, 92);
            this.lbpbUserNames.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbpbUserNames.Name = "lbpbUserNames";
            this.lbpbUserNames.Size = new System.Drawing.Size(30, 30);
            this.lbpbUserNames.TabIndex = 53;
            this.lbpbUserNames.TabStop = false;
            this.lbpbUserNames.Click += new System.EventHandler(this.lbpbUserNames_Click);
            // 
            // tbpbUserPass
            // 
            this.tbpbUserPass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbpbUserPass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tbpbUserPass.Image = global::WindowsFormsApp1.Properties.Resources.key;
            this.tbpbUserPass.InitialImage = ((System.Drawing.Image)(resources.GetObject("tbpbUserPass.InitialImage")));
            this.tbpbUserPass.Location = new System.Drawing.Point(12, 449);
            this.tbpbUserPass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbpbUserPass.Name = "tbpbUserPass";
            this.tbpbUserPass.Size = new System.Drawing.Size(30, 30);
            this.tbpbUserPass.TabIndex = 54;
            this.tbpbUserPass.TabStop = false;
            // 
            // lbUserNamesScroll
            // 
            this.lbUserNamesScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbUserNamesScroll.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.lbUserNamesScroll.Location = new System.Drawing.Point(331, 90);
            this.lbUserNamesScroll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbUserNamesScroll.Maximum = 100;
            this.lbUserNamesScroll.Name = "lbUserNamesScroll";
            this.lbUserNamesScroll.Orientation = System.Windows.Forms.ScrollOrientation.VerticalScroll;
            this.lbUserNamesScroll.Size = new System.Drawing.Size(40, 341);
            this.lbUserNamesScroll.TabIndex = 70;
            this.lbUserNamesScroll.TabStop = false;
            this.lbUserNamesScroll.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(93)))), ((int)(((byte)(165)))));
            this.lbUserNamesScroll.ThumbSize = 15;
            this.lbUserNamesScroll.Value = 0;
            this.lbUserNamesScroll.Visible = false;
            this.lbUserNamesScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.lbUserNamesScroll_Scroll);
            // 
            // ConnectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(380, 618);
            this.Controls.Add(this.bTerminal);
            this.Controls.Add(this.lbUserNamesScroll);
            this.Controls.Add(this.lbUserNames);
            this.Controls.Add(this.pWindowState);
            this.Controls.Add(this.tbpbConnectBD);
            this.Controls.Add(this.pbPassLook);
            this.Controls.Add(this.bConnectBD);
            this.Controls.Add(this.tbConnectBD);
            this.Controls.Add(this.lbpbUserNames);
            this.Controls.Add(this.lbUserPasswords);
            this.Controls.Add(this.tbpbUserPass);
            this.Controls.Add(this.tbpConnectBD);
            this.Controls.Add(this.tbpUserPass);
            this.Controls.Add(this.tbUserPass);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ConnectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConnectForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConnectForm_FormClosing);
            this.Load += new System.EventHandler(this.ConnectForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ConnectForm_Paint);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ConnectForm_KeyPress);
            this.pWindowState.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbpbConnectBD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPassLook)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbpbUserNames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbpbUserPass)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel tbpConnectBD;
        private System.Windows.Forms.PictureBox tbpbConnectBD;
        private System.Windows.Forms.PictureBox pbPassLook;
        private System.Windows.Forms.PictureBox tbpbUserPass;
        private System.Windows.Forms.PictureBox lbpbUserNames;
        private System.Windows.Forms.Panel tbpUserPass;
        private System.Windows.Forms.ListBox lbUserPasswords;
        private System.Windows.Forms.TextBox tbUserPass;
        private System.Windows.Forms.TextBox tbConnectBD;
        private System.Windows.Forms.Button bConnectBD;
        private System.Windows.Forms.Panel pWindowState;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button bExit;
        private System.Windows.Forms.Button bMin;
        private System.Windows.Forms.Timer tPass;
        private System.Windows.Forms.ListBox lbUserNames;
        private FlatScrollBar lbUserNamesScroll;
        private System.Windows.Forms.Button bTerminal;
    }
}
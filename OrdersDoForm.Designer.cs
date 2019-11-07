namespace WindowsFormsApp1
{
    partial class OrdersDoForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrdersDoForm));
            this.pWindowState = new System.Windows.Forms.Panel();
            this.bFlash = new System.Windows.Forms.Button();
            this.bMax = new System.Windows.Forms.Button();
            this.bMin = new System.Windows.Forms.Button();
            this.bExit = new System.Windows.Forms.Button();
            this.bEditBack = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbpFpFail = new System.Windows.Forms.Panel();
            this.tbFpFail = new System.Windows.Forms.TextBox();
            this.bLoadFpFails = new System.Windows.Forms.Button();
            this.dgvFails = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            this.pFails = new System.Windows.Forms.Panel();
            this.pWindowState.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFails)).BeginInit();
            this.pFails.SuspendLayout();
            this.SuspendLayout();
            // 
            // pWindowState
            // 
            this.pWindowState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pWindowState.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pWindowState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.pWindowState.Controls.Add(this.bFlash);
            this.pWindowState.Controls.Add(this.bMax);
            this.pWindowState.Controls.Add(this.bMin);
            this.pWindowState.Controls.Add(this.bExit);
            this.pWindowState.Location = new System.Drawing.Point(1, 1);
            this.pWindowState.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pWindowState.Name = "pWindowState";
            this.pWindowState.Size = new System.Drawing.Size(771, 30);
            this.pWindowState.TabIndex = 111;
            this.pWindowState.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pWindowState_MouseDown);
            // 
            // bFlash
            // 
            this.bFlash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bFlash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.bFlash.FlatAppearance.BorderSize = 0;
            this.bFlash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFlash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.bFlash.Location = new System.Drawing.Point(618, 4);
            this.bFlash.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.bFlash.Name = "bFlash";
            this.bFlash.Size = new System.Drawing.Size(35, 25);
            this.bFlash.TabIndex = 113;
            this.bFlash.TabStop = false;
            this.bFlash.UseVisualStyleBackColor = false;
            this.bFlash.Click += new System.EventHandler(this.bFlash_Click);
            // 
            // bMax
            // 
            this.bMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bMax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bMax.FlatAppearance.BorderSize = 0;
            this.bMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.bMax.Location = new System.Drawing.Point(694, 4);
            this.bMax.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.bMax.Name = "bMax";
            this.bMax.Size = new System.Drawing.Size(35, 25);
            this.bMax.TabIndex = 7;
            this.bMax.TabStop = false;
            this.bMax.UseVisualStyleBackColor = false;
            this.bMax.Click += new System.EventHandler(this.bMax_Click);
            // 
            // bMin
            // 
            this.bMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bMin.FlatAppearance.BorderSize = 0;
            this.bMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.bMin.Location = new System.Drawing.Point(656, 4);
            this.bMin.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.bMin.Name = "bMin";
            this.bMin.Size = new System.Drawing.Size(35, 25);
            this.bMin.TabIndex = 5;
            this.bMin.TabStop = false;
            this.bMin.UseVisualStyleBackColor = false;
            this.bMin.Click += new System.EventHandler(this.bMin_Click);
            // 
            // bExit
            // 
            this.bExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bExit.FlatAppearance.BorderSize = 0;
            this.bExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.bExit.Location = new System.Drawing.Point(732, 4);
            this.bExit.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.bExit.Name = "bExit";
            this.bExit.Size = new System.Drawing.Size(35, 25);
            this.bExit.TabIndex = 6;
            this.bExit.TabStop = false;
            this.bExit.UseVisualStyleBackColor = false;
            this.bExit.Click += new System.EventHandler(this.bExit_Click);
            // 
            // bEditBack
            // 
            this.bEditBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(93)))), ((int)(((byte)(165)))));
            this.bEditBack.FlatAppearance.BorderSize = 0;
            this.bEditBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEditBack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.bEditBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bEditBack.Location = new System.Drawing.Point(1, 43);
            this.bEditBack.Margin = new System.Windows.Forms.Padding(0);
            this.bEditBack.Name = "bEditBack";
            this.bEditBack.Size = new System.Drawing.Size(160, 30);
            this.bEditBack.TabIndex = 112;
            this.bEditBack.Text = "       Назад";
            this.bEditBack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bEditBack.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label2.Location = new System.Drawing.Point(5, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(767, 22);
            this.label2.TabIndex = 133;
            this.label2.Text = "Основание для установления двойки по ФП:";
            // 
            // tbpFpFail
            // 
            this.tbpFpFail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbpFpFail.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tbpFpFail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(93)))), ((int)(((byte)(165)))));
            this.tbpFpFail.Location = new System.Drawing.Point(4, 94);
            this.tbpFpFail.Margin = new System.Windows.Forms.Padding(4);
            this.tbpFpFail.Name = "tbpFpFail";
            this.tbpFpFail.Size = new System.Drawing.Size(768, 1);
            this.tbpFpFail.TabIndex = 134;
            // 
            // tbFpFail
            // 
            this.tbFpFail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFpFail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.tbFpFail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbFpFail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.tbFpFail.Location = new System.Drawing.Point(8, 73);
            this.tbFpFail.Margin = new System.Windows.Forms.Padding(4);
            this.tbFpFail.MaxLength = 15;
            this.tbFpFail.Multiline = true;
            this.tbFpFail.Name = "tbFpFail";
            this.tbFpFail.ReadOnly = true;
            this.tbFpFail.Size = new System.Drawing.Size(764, 21);
            this.tbFpFail.TabIndex = 0;
            this.tbFpFail.TabStop = false;
            this.tbFpFail.Text = "пример";
            // 
            // bLoadFpFails
            // 
            this.bLoadFpFails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bLoadFpFails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(93)))), ((int)(((byte)(165)))));
            this.bLoadFpFails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bLoadFpFails.FlatAppearance.BorderSize = 0;
            this.bLoadFpFails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bLoadFpFails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.bLoadFpFails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bLoadFpFails.Location = new System.Drawing.Point(172, 43);
            this.bLoadFpFails.Margin = new System.Windows.Forms.Padding(0);
            this.bLoadFpFails.Name = "bLoadFpFails";
            this.bLoadFpFails.Size = new System.Drawing.Size(601, 30);
            this.bLoadFpFails.TabIndex = 137;
            this.bLoadFpFails.Tag = "";
            this.bLoadFpFails.Text = "       Загрузить список с двойками по ФП";
            this.bLoadFpFails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bLoadFpFails.UseVisualStyleBackColor = false;
            this.bLoadFpFails.Click += new System.EventHandler(this.bLoadFpFails_Click);
            // 
            // dgvFails
            // 
            this.dgvFails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFails.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvFails.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.dgvFails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvFails.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(58)))), ((int)(((byte)(107)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column4,
            this.Column5,
            this.Column9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.Column2});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(129)))), ((int)(((byte)(16)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFails.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvFails.EnableHeadersVisualStyles = false;
            this.dgvFails.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.dgvFails.Location = new System.Drawing.Point(0, 123);
            this.dgvFails.Margin = new System.Windows.Forms.Padding(0);
            this.dgvFails.Name = "dgvFails";
            this.dgvFails.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(58)))), ((int)(((byte)(107)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFails.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvFails.RowHeadersWidth = 16;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(129)))), ((int)(((byte)(16)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvFails.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvFails.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvFails.Size = new System.Drawing.Size(771, 389);
            this.dgvFails.TabIndex = 139;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "В.звание";
            this.Column3.MinimumWidth = 145;
            this.Column3.Name = "Column3";
            this.Column3.Width = 145;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Фамилия";
            this.Column1.MinimumWidth = 250;
            this.Column1.Name = "Column1";
            this.Column1.Width = 250;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Имя";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Отчество";
            this.Column5.Name = "Column5";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Вид взыскания";
            this.Column9.MinimumWidth = 145;
            this.Column9.Name = "Column9";
            this.Column9.Width = 145;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn10.HeaderText = "За что объявлено";
            this.dataGridViewTextBoxColumn10.MinimumWidth = 150;
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Приказ";
            this.dataGridViewTextBoxColumn11.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Width = 109;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "№";
            this.dataGridViewTextBoxColumn12.MinimumWidth = 25;
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.Width = 49;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "Дата";
            this.dataGridViewTextBoxColumn13.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Процент премии";
            this.Column2.MinimumWidth = 60;
            this.Column2.Name = "Column2";
            this.Column2.Width = 60;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(93)))), ((int)(((byte)(165)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(171, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(601, 30);
            this.button2.TabIndex = 140;
            this.button2.Text = "       Приказ на подпись";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // pFails
            // 
            this.pFails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pFails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pFails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.pFails.Controls.Add(this.tbFpFail);
            this.pFails.Controls.Add(this.tbpFpFail);
            this.pFails.Controls.Add(this.button2);
            this.pFails.Controls.Add(this.dgvFails);
            this.pFails.Controls.Add(this.label2);
            this.pFails.Location = new System.Drawing.Point(1, 91);
            this.pFails.Margin = new System.Windows.Forms.Padding(4);
            this.pFails.Name = "pFails";
            this.pFails.Size = new System.Drawing.Size(771, 512);
            this.pFails.TabIndex = 141;
            this.pFails.Visible = false;
            // 
            // OrdersDoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(774, 616);
            this.Controls.Add(this.pFails);
            this.Controls.Add(this.bLoadFpFails);
            this.Controls.Add(this.bEditBack);
            this.Controls.Add(this.pWindowState);
            this.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OrdersDoForm";
            this.Text = "OrdersDoForm";
            this.Load += new System.EventHandler(this.OrdersDoForm_Load);
            this.pWindowState.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFails)).EndInit();
            this.pFails.ResumeLayout(false);
            this.pFails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pWindowState;
        private System.Windows.Forms.Button bFlash;
        private System.Windows.Forms.Button bMax;
        private System.Windows.Forms.Button bMin;
        private System.Windows.Forms.Button bExit;
        private System.Windows.Forms.Button bEditBack;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel tbpFpFail;
        private System.Windows.Forms.TextBox tbFpFail;
        private System.Windows.Forms.Button bLoadFpFails;
        private System.Windows.Forms.DataGridView dgvFails;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel pFails;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}
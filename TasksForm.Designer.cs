namespace WindowsFormsApp1
{
    partial class TasksForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TasksForm));
            this.pNavigator = new System.Windows.Forms.Panel();
            this.bMax = new System.Windows.Forms.Button();
            this.bExit = new System.Windows.Forms.Button();
            this.bEditBack = new System.Windows.Forms.Button();
            this.lbpTasks = new System.Windows.Forms.Panel();
            this.lbTasksId = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.bCloseTask = new System.Windows.Forms.Button();
            this.bOpenPeople = new System.Windows.Forms.Button();
            this.lbTasksPeoplesId = new System.Windows.Forms.ListBox();
            this.lbTasks = new System.Windows.Forms.ListBox();
            this.pNavigator.SuspendLayout();
            this.lbpTasks.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pNavigator
            // 
            this.pNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pNavigator.Controls.Add(this.bMax);
            this.pNavigator.Controls.Add(this.bExit);
            this.pNavigator.Location = new System.Drawing.Point(1, 1);
            this.pNavigator.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.pNavigator.Name = "pNavigator";
            this.pNavigator.Size = new System.Drawing.Size(786, 30);
            this.pNavigator.TabIndex = 38;
            // 
            // bMax
            // 
            this.bMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bMax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bMax.FlatAppearance.BorderSize = 0;
            this.bMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.bMax.Image = global::WindowsFormsApp1.Properties.Resources.fullin30;
            this.bMax.Location = new System.Drawing.Point(701, 0);
            this.bMax.Margin = new System.Windows.Forms.Padding(4);
            this.bMax.Name = "bMax";
            this.bMax.Size = new System.Drawing.Size(40, 30);
            this.bMax.TabIndex = 42;
            this.bMax.TabStop = false;
            this.bMax.UseVisualStyleBackColor = false;
            this.bMax.Click += new System.EventHandler(this.bMax_Click);
            this.bMax.MouseEnter += new System.EventHandler(this.bMax_MouseEnter);
            this.bMax.MouseLeave += new System.EventHandler(this.bMax_MouseLeave);
            // 
            // bExit
            // 
            this.bExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bExit.FlatAppearance.BorderSize = 0;
            this.bExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.bExit.Image = global::WindowsFormsApp1.Properties.Resources.close30;
            this.bExit.Location = new System.Drawing.Point(744, 0);
            this.bExit.Margin = new System.Windows.Forms.Padding(4);
            this.bExit.Name = "bExit";
            this.bExit.Size = new System.Drawing.Size(40, 30);
            this.bExit.TabIndex = 41;
            this.bExit.TabStop = false;
            this.bExit.UseVisualStyleBackColor = false;
            this.bExit.Click += new System.EventHandler(this.bExit_Click);
            // 
            // bEditBack
            // 
            this.bEditBack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bEditBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(93)))), ((int)(((byte)(165)))));
            this.bEditBack.FlatAppearance.BorderSize = 0;
            this.bEditBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEditBack.Image = global::WindowsFormsApp1.Properties.Resources.back40;
            this.bEditBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bEditBack.Location = new System.Drawing.Point(1, 31);
            this.bEditBack.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.bEditBack.Name = "bEditBack";
            this.bEditBack.Size = new System.Drawing.Size(786, 40);
            this.bEditBack.TabIndex = 5;
            this.bEditBack.Text = "        Назад";
            this.bEditBack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bEditBack.UseVisualStyleBackColor = false;
            this.bEditBack.Click += new System.EventHandler(this.BEditBack_Click);
            // 
            // lbpTasks
            // 
            this.lbpTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbpTasks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(93)))), ((int)(((byte)(165)))));
            this.lbpTasks.Controls.Add(this.lbTasksId);
            this.lbpTasks.Controls.Add(this.panel2);
            this.lbpTasks.Controls.Add(this.lbTasksPeoplesId);
            this.lbpTasks.Controls.Add(this.lbTasks);
            this.lbpTasks.Location = new System.Drawing.Point(14, 91);
            this.lbpTasks.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.lbpTasks.Name = "lbpTasks";
            this.lbpTasks.Size = new System.Drawing.Size(710, 370);
            this.lbpTasks.TabIndex = 39;
            // 
            // lbTasksId
            // 
            this.lbTasksId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(249)))), ((int)(((byte)(254)))));
            this.lbTasksId.FormattingEnabled = true;
            this.lbTasksId.ItemHeight = 18;
            this.lbTasksId.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.lbTasksId.Location = new System.Drawing.Point(279, 123);
            this.lbTasksId.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.lbTasksId.Name = "lbTasksId";
            this.lbTasksId.Size = new System.Drawing.Size(33, 166);
            this.lbTasksId.TabIndex = 41;
            this.lbTasksId.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.bCloseTask);
            this.panel2.Controls.Add(this.bOpenPeople);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(710, 60);
            this.panel2.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(93)))), ((int)(((byte)(165)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::WindowsFormsApp1.Properties.Resources.refresh40;
            this.button1.Location = new System.Drawing.Point(10, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 40);
            this.button1.TabIndex = 59;
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // bCloseTask
            // 
            this.bCloseTask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(93)))), ((int)(((byte)(165)))));
            this.bCloseTask.FlatAppearance.BorderSize = 0;
            this.bCloseTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCloseTask.Image = global::WindowsFormsApp1.Properties.Resources.delete40;
            this.bCloseTask.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCloseTask.Location = new System.Drawing.Point(60, 10);
            this.bCloseTask.Name = "bCloseTask";
            this.bCloseTask.Size = new System.Drawing.Size(250, 40);
            this.bCloseTask.TabIndex = 58;
            this.bCloseTask.Text = "         Удалить задачу";
            this.bCloseTask.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCloseTask.UseVisualStyleBackColor = false;
            this.bCloseTask.Click += new System.EventHandler(this.BCloseTask_Click);
            // 
            // bOpenPeople
            // 
            this.bOpenPeople.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bOpenPeople.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(93)))), ((int)(((byte)(165)))));
            this.bOpenPeople.FlatAppearance.BorderSize = 0;
            this.bOpenPeople.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOpenPeople.Image = global::WindowsFormsApp1.Properties.Resources.user_open40;
            this.bOpenPeople.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bOpenPeople.Location = new System.Drawing.Point(320, 10);
            this.bOpenPeople.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.bOpenPeople.Name = "bOpenPeople";
            this.bOpenPeople.Size = new System.Drawing.Size(380, 40);
            this.bOpenPeople.TabIndex = 4;
            this.bOpenPeople.Text = "         Открыть военнослужащего";
            this.bOpenPeople.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bOpenPeople.UseVisualStyleBackColor = false;
            this.bOpenPeople.Click += new System.EventHandler(this.bOpenPeople_Click);
            // 
            // lbTasksPeoplesId
            // 
            this.lbTasksPeoplesId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(249)))), ((int)(((byte)(254)))));
            this.lbTasksPeoplesId.FormattingEnabled = true;
            this.lbTasksPeoplesId.ItemHeight = 18;
            this.lbTasksPeoplesId.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.lbTasksPeoplesId.Location = new System.Drawing.Point(206, 129);
            this.lbTasksPeoplesId.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.lbTasksPeoplesId.Name = "lbTasksPeoplesId";
            this.lbTasksPeoplesId.Size = new System.Drawing.Size(33, 166);
            this.lbTasksPeoplesId.TabIndex = 40;
            this.lbTasksPeoplesId.Visible = false;
            // 
            // lbTasks
            // 
            this.lbTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTasks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.lbTasks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbTasks.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbTasks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lbTasks.FormattingEnabled = true;
            this.lbTasks.IntegralHeight = false;
            this.lbTasks.ItemHeight = 36;
            this.lbTasks.Items.AddRange(new object[] {
            "О прохождении службы",
            "О составе семьи",
            "О выслуге лет",
            "Послужной список"});
            this.lbTasks.Location = new System.Drawing.Point(0, 58);
            this.lbTasks.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.lbTasks.Name = "lbTasks";
            this.lbTasks.Size = new System.Drawing.Size(736, 311);
            this.lbTasks.TabIndex = 1;
            this.lbTasks.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbChoosePeopleFind_DrawItem);
            this.lbTasks.SelectedIndexChanged += new System.EventHandler(this.LbTasks_SelectedIndexChanged);
            this.lbTasks.DoubleClick += new System.EventHandler(this.lbTasks_DoubleClick);
            // 
            // TasksForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(788, 525);
            this.Controls.Add(this.bEditBack);
            this.Controls.Add(this.lbpTasks);
            this.Controls.Add(this.pNavigator);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "TasksForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.TasksForm_Load);
            this.pNavigator.ResumeLayout(false);
            this.lbpTasks.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pNavigator;
        public System.Windows.Forms.Button bEditBack;
        private System.Windows.Forms.Panel lbpTasks;
        private System.Windows.Forms.ListBox lbTasks;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button bOpenPeople;
        private System.Windows.Forms.ListBox lbTasksPeoplesId;
        private System.Windows.Forms.Button bCloseTask;
        private System.Windows.Forms.ListBox lbTasksId;
        private System.Windows.Forms.Button bMax;
        private System.Windows.Forms.Button bExit;
        private System.Windows.Forms.Button button1;
    }
}
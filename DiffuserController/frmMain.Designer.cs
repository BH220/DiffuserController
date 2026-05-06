namespace DiffuserController
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            btnOff = new Button();
            btnOn = new Button();
            btnRefresh = new Button();
            plInterval = new Panel();
            label3 = new Label();
            dtStartH = new NumericUpDown();
            btnStop1 = new Button();
            dtStartM = new NumericUpDown();
            dtEndH = new NumericUpDown();
            btnRun1 = new Button();
            dtTermH = new NumericUpDown();
            label10 = new Label();
            dtTermM = new NumericUpDown();
            label6 = new Label();
            dtEndM = new NumericUpDown();
            label5 = new Label();
            dtTermS = new NumericUpDown();
            label9 = new Label();
            dtTermInterval = new NumericUpDown();
            label15 = new Label();
            label7 = new Label();
            label8 = new Label();
            label4 = new Label();
            plSchedule = new Panel();
            lstBox = new ListBox();
            label18 = new Label();
            btnStop2 = new Button();
            label19 = new Label();
            btnRun2 = new Button();
            label20 = new Label();
            dtTermSchedule = new NumericUpDown();
            btnScDel = new Button();
            btnScAdd = new Button();
            cmbUsbList = new ComboBox();
            label16 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            rbSchedule = new RadioButton();
            label1 = new Label();
            rbInterval = new RadioButton();
            tabPage2 = new TabPage();
            grid = new DataGridView();
            Column1 = new DataGridViewCheckBoxColumn();
            dateDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            yearDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            monthDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            dayDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            messageDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            dateModelBindingSource = new BindingSource(components);
            panel3 = new Panel();
            btnSelectedDel = new Button();
            panel2 = new Panel();
            groupBox2 = new GroupBox();
            defYear = new NumericUpDown();
            label13 = new Label();
            btnSunday = new Button();
            btnSatDay = new Button();
            btnHoliDay = new Button();
            label14 = new Label();
            groupBox1 = new GroupBox();
            monthCalendar1 = new MonthCalendar();
            label11 = new Label();
            btnDel = new Button();
            txtDt = new TextBox();
            btnRange = new Button();
            txtMessage = new TextBox();
            label12 = new Label();
            btnApply = new Button();
            panel4 = new Panel();
            label17 = new Label();
            statusStrip1 = new StatusStrip();
            lbLeft = new ToolStripStatusLabel();
            lbRunning = new ToolStripStatusLabel();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            lbTime = new ToolStripStatusLabel();
            timer1 = new System.Windows.Forms.Timer(components);
            runningTimer = new System.Windows.Forms.Timer(components);
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            열기ToolStripMenuItem1 = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            종료ToolStripMenuItem1 = new ToolStripMenuItem();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            plInterval.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtStartH).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dtStartM).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dtEndH).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dtTermH).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dtTermM).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dtEndM).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dtTermS).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dtTermInterval).BeginInit();
            plSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtTermSchedule).BeginInit();
            panel1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateModelBindingSource).BeginInit();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)defYear).BeginInit();
            groupBox1.SuspendLayout();
            panel4.SuspendLayout();
            statusStrip1.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1020, 567);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(btnOff);
            tabPage1.Controls.Add(btnOn);
            tabPage1.Controls.Add(btnRefresh);
            tabPage1.Controls.Add(plInterval);
            tabPage1.Controls.Add(plSchedule);
            tabPage1.Controls.Add(cmbUsbList);
            tabPage1.Controls.Add(label16);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(panel1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1012, 539);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "제어판";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnOff
            // 
            btnOff.Location = new Point(106, 444);
            btnOff.Name = "btnOff";
            btnOff.Size = new Size(75, 45);
            btnOff.TabIndex = 6;
            btnOff.Text = "OFF";
            btnOff.UseVisualStyleBackColor = true;
            btnOff.Visible = false;
            btnOff.Click += btnOff_Click;
            // 
            // btnOn
            // 
            btnOn.Location = new Point(25, 444);
            btnOn.Name = "btnOn";
            btnOn.Size = new Size(75, 45);
            btnOn.TabIndex = 6;
            btnOn.Text = "ON";
            btnOn.UseVisualStyleBackColor = true;
            btnOn.Visible = false;
            btnOn.Click += btnOn_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Image = (Image)resources.GetObject("btnRefresh.Image");
            btnRefresh.Location = new Point(466, 24);
            btnRefresh.Margin = new Padding(0);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(26, 26);
            btnRefresh.TabIndex = 5;
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += button1_Click;
            // 
            // plInterval
            // 
            plInterval.BackColor = Color.FromArgb(249, 249, 249);
            plInterval.Controls.Add(label3);
            plInterval.Controls.Add(dtStartH);
            plInterval.Controls.Add(btnStop1);
            plInterval.Controls.Add(dtStartM);
            plInterval.Controls.Add(dtEndH);
            plInterval.Controls.Add(btnRun1);
            plInterval.Controls.Add(dtTermH);
            plInterval.Controls.Add(label10);
            plInterval.Controls.Add(dtTermM);
            plInterval.Controls.Add(label6);
            plInterval.Controls.Add(dtEndM);
            plInterval.Controls.Add(label5);
            plInterval.Controls.Add(dtTermS);
            plInterval.Controls.Add(label9);
            plInterval.Controls.Add(dtTermInterval);
            plInterval.Controls.Add(label15);
            plInterval.Controls.Add(label7);
            plInterval.Controls.Add(label8);
            plInterval.Controls.Add(label4);
            plInterval.Location = new Point(25, 125);
            plInterval.Name = "plInterval";
            plInterval.Size = new Size(393, 298);
            plInterval.TabIndex = 3;
            // 
            // label3
            // 
            label3.Location = new Point(3, 17);
            label3.Name = "label3";
            label3.Size = new Size(111, 23);
            label3.TabIndex = 0;
            label3.Text = "시작 시간";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dtStartH
            // 
            dtStartH.Location = new Point(155, 17);
            dtStartH.Maximum = new decimal(new int[] { 23, 0, 0, 0 });
            dtStartH.Name = "dtStartH";
            dtStartH.Size = new Size(43, 23);
            dtStartH.TabIndex = 1;
            dtStartH.TextAlign = HorizontalAlignment.Center;
            dtStartH.Value = new decimal(new int[] { 23, 0, 0, 0 });
            dtStartH.ValueChanged += dtStartH_ValueChanged;
            // 
            // btnStop1
            // 
            btnStop1.Location = new Point(283, 235);
            btnStop1.Name = "btnStop1";
            btnStop1.Size = new Size(105, 42);
            btnStop1.TabIndex = 18;
            btnStop1.Text = "중지";
            btnStop1.UseVisualStyleBackColor = true;
            btnStop1.Click += btnStop_Click;
            // 
            // dtStartM
            // 
            dtStartM.Location = new Point(237, 17);
            dtStartM.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            dtStartM.Name = "dtStartM";
            dtStartM.Size = new Size(43, 23);
            dtStartM.TabIndex = 3;
            dtStartM.TextAlign = HorizontalAlignment.Center;
            dtStartM.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // dtEndH
            // 
            dtEndH.Location = new Point(155, 72);
            dtEndH.Maximum = new decimal(new int[] { 23, 0, 0, 0 });
            dtEndH.Name = "dtEndH";
            dtEndH.Size = new Size(43, 23);
            dtEndH.TabIndex = 5;
            dtEndH.TextAlign = HorizontalAlignment.Center;
            dtEndH.Value = new decimal(new int[] { 17, 0, 0, 0 });
            // 
            // btnRun1
            // 
            btnRun1.Enabled = false;
            btnRun1.Location = new Point(155, 235);
            btnRun1.Name = "btnRun1";
            btnRun1.Size = new Size(105, 42);
            btnRun1.TabIndex = 17;
            btnRun1.Text = "동작";
            btnRun1.UseVisualStyleBackColor = true;
            btnRun1.Click += btnRun_Click;
            // 
            // dtTermH
            // 
            dtTermH.Location = new Point(155, 129);
            dtTermH.Maximum = new decimal(new int[] { 23, 0, 0, 0 });
            dtTermH.Name = "dtTermH";
            dtTermH.Size = new Size(43, 23);
            dtTermH.TabIndex = 9;
            dtTermH.TextAlign = HorizontalAlignment.Center;
            // 
            // label10
            // 
            label10.Location = new Point(204, 182);
            label10.Name = "label10";
            label10.Size = new Size(27, 23);
            label10.TabIndex = 16;
            label10.Text = "초";
            label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dtTermM
            // 
            dtTermM.Location = new Point(237, 129);
            dtTermM.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            dtTermM.Name = "dtTermM";
            dtTermM.Size = new Size(43, 23);
            dtTermM.TabIndex = 11;
            dtTermM.TextAlign = HorizontalAlignment.Center;
            dtTermM.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label6
            // 
            label6.Location = new Point(3, 182);
            label6.Name = "label6";
            label6.Size = new Size(111, 23);
            label6.TabIndex = 14;
            label6.Text = "분사 유지 시간";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dtEndM
            // 
            dtEndM.Location = new Point(237, 72);
            dtEndM.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            dtEndM.Name = "dtEndM";
            dtEndM.Size = new Size(43, 23);
            dtEndM.TabIndex = 6;
            dtEndM.TextAlign = HorizontalAlignment.Center;
            dtEndM.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // label5
            // 
            label5.Location = new Point(3, 127);
            label5.Name = "label5";
            label5.Size = new Size(111, 23);
            label5.TabIndex = 8;
            label5.Text = "간격";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dtTermS
            // 
            dtTermS.Location = new Point(319, 129);
            dtTermS.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            dtTermS.Name = "dtTermS";
            dtTermS.Size = new Size(43, 23);
            dtTermS.TabIndex = 13;
            dtTermS.TextAlign = HorizontalAlignment.Center;
            // 
            // label9
            // 
            label9.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold);
            label9.Location = new Point(286, 125);
            label9.Name = "label9";
            label9.Size = new Size(27, 23);
            label9.TabIndex = 12;
            label9.Text = ":";
            label9.TextAlign = ContentAlignment.TopCenter;
            // 
            // dtTermInterval
            // 
            dtTermInterval.Location = new Point(155, 182);
            dtTermInterval.Name = "dtTermInterval";
            dtTermInterval.Size = new Size(43, 23);
            dtTermInterval.TabIndex = 15;
            dtTermInterval.TextAlign = HorizontalAlignment.Center;
            dtTermInterval.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label15
            // 
            label15.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold);
            label15.Location = new Point(204, 125);
            label15.Name = "label15";
            label15.Size = new Size(27, 23);
            label15.TabIndex = 10;
            label15.Text = ":";
            label15.TextAlign = ContentAlignment.TopCenter;
            // 
            // label7
            // 
            label7.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold);
            label7.Location = new Point(204, 13);
            label7.Name = "label7";
            label7.Size = new Size(27, 23);
            label7.TabIndex = 2;
            label7.Text = ":";
            label7.TextAlign = ContentAlignment.TopCenter;
            // 
            // label8
            // 
            label8.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold);
            label8.Location = new Point(204, 68);
            label8.Name = "label8";
            label8.Size = new Size(27, 23);
            label8.TabIndex = 7;
            label8.Text = ":";
            label8.TextAlign = ContentAlignment.TopCenter;
            // 
            // label4
            // 
            label4.Location = new Point(3, 72);
            label4.Name = "label4";
            label4.Size = new Size(111, 23);
            label4.TabIndex = 4;
            label4.Text = "종료 시간";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // plSchedule
            // 
            plSchedule.BackColor = Color.FromArgb(249, 249, 249);
            plSchedule.Controls.Add(lstBox);
            plSchedule.Controls.Add(label18);
            plSchedule.Controls.Add(btnStop2);
            plSchedule.Controls.Add(label19);
            plSchedule.Controls.Add(btnRun2);
            plSchedule.Controls.Add(label20);
            plSchedule.Controls.Add(dtTermSchedule);
            plSchedule.Controls.Add(btnScDel);
            plSchedule.Controls.Add(btnScAdd);
            plSchedule.Location = new Point(424, 125);
            plSchedule.Name = "plSchedule";
            plSchedule.Size = new Size(393, 298);
            plSchedule.TabIndex = 4;
            // 
            // lstBox
            // 
            lstBox.FormattingEnabled = true;
            lstBox.ItemHeight = 15;
            lstBox.Location = new Point(155, 17);
            lstBox.Name = "lstBox";
            lstBox.Size = new Size(76, 139);
            lstBox.Sorted = true;
            lstBox.TabIndex = 1;
            // 
            // label18
            // 
            label18.Location = new Point(3, 17);
            label18.Name = "label18";
            label18.Size = new Size(111, 23);
            label18.TabIndex = 0;
            label18.Text = "시작 시간";
            label18.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnStop2
            // 
            btnStop2.Location = new Point(283, 235);
            btnStop2.Name = "btnStop2";
            btnStop2.Size = new Size(105, 42);
            btnStop2.TabIndex = 8;
            btnStop2.Text = "중지";
            btnStop2.UseVisualStyleBackColor = true;
            btnStop2.Click += btnStop_Click;
            // 
            // label19
            // 
            label19.Location = new Point(204, 182);
            label19.Name = "label19";
            label19.Size = new Size(27, 23);
            label19.TabIndex = 6;
            label19.Text = "초";
            label19.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnRun2
            // 
            btnRun2.Enabled = false;
            btnRun2.Location = new Point(155, 235);
            btnRun2.Name = "btnRun2";
            btnRun2.Size = new Size(105, 42);
            btnRun2.TabIndex = 7;
            btnRun2.Text = "동작";
            btnRun2.UseVisualStyleBackColor = true;
            btnRun2.Click += btnRun_Click;
            // 
            // label20
            // 
            label20.Location = new Point(3, 182);
            label20.Name = "label20";
            label20.Size = new Size(111, 23);
            label20.TabIndex = 4;
            label20.Text = "분사 유지 시간";
            label20.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dtTermSchedule
            // 
            dtTermSchedule.Location = new Point(155, 182);
            dtTermSchedule.Name = "dtTermSchedule";
            dtTermSchedule.Size = new Size(43, 23);
            dtTermSchedule.TabIndex = 5;
            dtTermSchedule.TextAlign = HorizontalAlignment.Center;
            dtTermSchedule.Value = new decimal(new int[] { 23, 0, 0, 0 });
            // 
            // btnScDel
            // 
            btnScDel.Location = new Point(237, 46);
            btnScDel.Name = "btnScDel";
            btnScDel.Size = new Size(75, 23);
            btnScDel.TabIndex = 3;
            btnScDel.Text = "삭제";
            btnScDel.UseVisualStyleBackColor = true;
            btnScDel.Click += btnScDel_Click;
            // 
            // btnScAdd
            // 
            btnScAdd.Location = new Point(237, 17);
            btnScAdd.Name = "btnScAdd";
            btnScAdd.Size = new Size(75, 23);
            btnScAdd.TabIndex = 2;
            btnScAdd.Text = "추가";
            btnScAdd.UseVisualStyleBackColor = true;
            btnScAdd.Click += btnScAdd_Click;
            // 
            // cmbUsbList
            // 
            cmbUsbList.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUsbList.FormattingEnabled = true;
            cmbUsbList.Location = new Point(177, 26);
            cmbUsbList.Name = "cmbUsbList";
            cmbUsbList.Size = new Size(283, 23);
            cmbUsbList.TabIndex = 1;
            // 
            // label16
            // 
            label16.Location = new Point(25, 25);
            label16.Name = "label16";
            label16.Size = new Size(100, 23);
            label16.TabIndex = 0;
            label16.Text = "대상 USB";
            label16.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Location = new Point(25, 83);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 2;
            label2.Text = "동작 구분";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            panel1.Controls.Add(rbSchedule);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(rbInterval);
            panel1.Location = new Point(177, 80);
            panel1.Name = "panel1";
            panel1.Size = new Size(283, 29);
            panel1.TabIndex = 2;
            // 
            // rbSchedule
            // 
            rbSchedule.AutoSize = true;
            rbSchedule.Dock = DockStyle.Left;
            rbSchedule.Location = new Point(103, 0);
            rbSchedule.Name = "rbSchedule";
            rbSchedule.Size = new Size(74, 29);
            rbSchedule.TabIndex = 2;
            rbSchedule.TabStop = true;
            rbSchedule.Text = "Schedule";
            rbSchedule.UseVisualStyleBackColor = true;
            rbSchedule.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Left;
            label1.Location = new Point(64, 0);
            label1.Name = "label1";
            label1.Size = new Size(39, 29);
            label1.TabIndex = 1;
            label1.Text = " ";
            // 
            // rbInterval
            // 
            rbInterval.AutoSize = true;
            rbInterval.Checked = true;
            rbInterval.Dock = DockStyle.Left;
            rbInterval.Location = new Point(0, 0);
            rbInterval.Name = "rbInterval";
            rbInterval.Size = new Size(64, 29);
            rbInterval.TabIndex = 0;
            rbInterval.TabStop = true;
            rbInterval.Text = "Interval";
            rbInterval.UseVisualStyleBackColor = true;
            rbInterval.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(grid);
            tabPage2.Controls.Add(panel3);
            tabPage2.Controls.Add(panel2);
            tabPage2.Controls.Add(panel4);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1012, 539);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "동작 제외 날짜 구성";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // grid
            // 
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeColumns = false;
            grid.AllowUserToResizeRows = false;
            grid.AutoGenerateColumns = false;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid.Columns.AddRange(new DataGridViewColumn[] { Column1, dateDataGridViewTextBoxColumn, yearDataGridViewTextBoxColumn, monthDataGridViewTextBoxColumn, dayDataGridViewTextBoxColumn, dataGridViewTextBoxColumn1, messageDataGridViewTextBoxColumn });
            grid.DataSource = dateModelBindingSource;
            grid.Dock = DockStyle.Fill;
            grid.Location = new Point(239, 70);
            grid.Name = "grid";
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.Size = new Size(770, 466);
            grid.TabIndex = 2;
            // 
            // Column1
            // 
            Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Column1.DataPropertyName = "IsSelected";
            Column1.HeaderText = "";
            Column1.Name = "Column1";
            Column1.Resizable = DataGridViewTriState.False;
            Column1.Width = 30;
            // 
            // dateDataGridViewTextBoxColumn
            // 
            dateDataGridViewTextBoxColumn.DataPropertyName = "Date";
            dateDataGridViewTextBoxColumn.HeaderText = "Date";
            dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            dateDataGridViewTextBoxColumn.Visible = false;
            // 
            // yearDataGridViewTextBoxColumn
            // 
            yearDataGridViewTextBoxColumn.DataPropertyName = "year";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            yearDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            yearDataGridViewTextBoxColumn.HeaderText = "년";
            yearDataGridViewTextBoxColumn.Name = "yearDataGridViewTextBoxColumn";
            yearDataGridViewTextBoxColumn.ReadOnly = true;
            yearDataGridViewTextBoxColumn.Width = 60;
            // 
            // monthDataGridViewTextBoxColumn
            // 
            monthDataGridViewTextBoxColumn.DataPropertyName = "month";
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            monthDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            monthDataGridViewTextBoxColumn.HeaderText = "월";
            monthDataGridViewTextBoxColumn.Name = "monthDataGridViewTextBoxColumn";
            monthDataGridViewTextBoxColumn.ReadOnly = true;
            monthDataGridViewTextBoxColumn.Width = 40;
            // 
            // dayDataGridViewTextBoxColumn
            // 
            dayDataGridViewTextBoxColumn.DataPropertyName = "day";
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dayDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            dayDataGridViewTextBoxColumn.HeaderText = "일";
            dayDataGridViewTextBoxColumn.Name = "dayDataGridViewTextBoxColumn";
            dayDataGridViewTextBoxColumn.ReadOnly = true;
            dayDataGridViewTextBoxColumn.Width = 40;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.DataPropertyName = "DoW";
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle8;
            dataGridViewTextBoxColumn1.HeaderText = "요일";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.Width = 40;
            // 
            // messageDataGridViewTextBoxColumn
            // 
            messageDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            messageDataGridViewTextBoxColumn.DataPropertyName = "Message";
            messageDataGridViewTextBoxColumn.HeaderText = "내용";
            messageDataGridViewTextBoxColumn.Name = "messageDataGridViewTextBoxColumn";
            // 
            // dateModelBindingSource
            // 
            dateModelBindingSource.DataSource = typeof(DateModel);
            // 
            // panel3
            // 
            panel3.Controls.Add(btnSelectedDel);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(239, 38);
            panel3.Name = "panel3";
            panel3.Size = new Size(770, 32);
            panel3.TabIndex = 4;
            // 
            // btnSelectedDel
            // 
            btnSelectedDel.Location = new Point(1, 5);
            btnSelectedDel.Name = "btnSelectedDel";
            btnSelectedDel.Size = new Size(105, 23);
            btnSelectedDel.TabIndex = 2;
            btnSelectedDel.Text = "선택항목 삭제";
            btnSelectedDel.UseVisualStyleBackColor = true;
            btnSelectedDel.Click += btnSelectedDel_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(groupBox2);
            panel2.Controls.Add(label14);
            panel2.Controls.Add(groupBox1);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(3, 38);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(5);
            panel2.Size = new Size(236, 498);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(defYear);
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(btnSunday);
            groupBox2.Controls.Add(btnSatDay);
            groupBox2.Controls.Add(btnHoliDay);
            groupBox2.Dock = DockStyle.Top;
            groupBox2.Location = new Point(5, 385);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(226, 143);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = " 일괄 저장 처리 ";
            // 
            // defYear
            // 
            defYear.Location = new Point(73, 21);
            defYear.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            defYear.Minimum = new decimal(new int[] { 2026, 0, 0, 0 });
            defYear.Name = "defYear";
            defYear.Size = new Size(55, 23);
            defYear.TabIndex = 2;
            defYear.TextAlign = HorizontalAlignment.Center;
            defYear.Value = new decimal(new int[] { 2026, 0, 0, 0 });
            // 
            // label13
            // 
            label13.Location = new Point(7, 25);
            label13.Name = "label13";
            label13.Size = new Size(74, 15);
            label13.TabIndex = 0;
            label13.Text = "기준년도: ";
            label13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSunday
            // 
            btnSunday.Location = new Point(14, 51);
            btnSunday.Name = "btnSunday";
            btnSunday.Size = new Size(90, 23);
            btnSunday.TabIndex = 1;
            btnSunday.Text = "일요일 반영";
            btnSunday.UseVisualStyleBackColor = true;
            btnSunday.Click += btnSunday_Click;
            // 
            // btnSatDay
            // 
            btnSatDay.Location = new Point(123, 51);
            btnSatDay.Name = "btnSatDay";
            btnSatDay.Size = new Size(90, 23);
            btnSatDay.TabIndex = 1;
            btnSatDay.Text = "토요일 반영";
            btnSatDay.UseVisualStyleBackColor = true;
            btnSatDay.Click += btnSatDay_Click;
            // 
            // btnHoliDay
            // 
            btnHoliDay.Location = new Point(15, 80);
            btnHoliDay.Name = "btnHoliDay";
            btnHoliDay.Size = new Size(90, 23);
            btnHoliDay.TabIndex = 0;
            btnHoliDay.Text = "공휴일 반영";
            btnHoliDay.UseVisualStyleBackColor = true;
            btnHoliDay.Click += btnHoliDay_Click;
            // 
            // label14
            // 
            label14.Dock = DockStyle.Top;
            label14.Location = new Point(5, 370);
            label14.Name = "label14";
            label14.Size = new Size(226, 15);
            label14.TabIndex = 0;
            label14.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(monthCalendar1);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(btnDel);
            groupBox1.Controls.Add(txtDt);
            groupBox1.Controls.Add(btnRange);
            groupBox1.Controls.Add(txtMessage);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(btnApply);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(5, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(226, 365);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = " 개별 저장 처리 ";
            // 
            // monthCalendar1
            // 
            monthCalendar1.Dock = DockStyle.Top;
            monthCalendar1.Location = new Point(3, 19);
            monthCalendar1.MaxSelectionCount = 1;
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 1;
            monthCalendar1.DateChanged += monthCalendar1_DateChanged;
            // 
            // label11
            // 
            label11.Location = new Point(6, 186);
            label11.Name = "label11";
            label11.Size = new Size(70, 23);
            label11.TabIndex = 4;
            label11.Text = "선택 날짜";
            label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnDel
            // 
            btnDel.Location = new Point(163, 336);
            btnDel.Name = "btnDel";
            btnDel.Size = new Size(60, 23);
            btnDel.TabIndex = 7;
            btnDel.Text = "삭제";
            btnDel.UseVisualStyleBackColor = true;
            btnDel.Click += btnDel_Click;
            // 
            // txtDt
            // 
            txtDt.Location = new Point(82, 186);
            txtDt.Name = "txtDt";
            txtDt.ReadOnly = true;
            txtDt.Size = new Size(141, 23);
            txtDt.TabIndex = 2;
            // 
            // btnRange
            // 
            btnRange.Location = new Point(3, 336);
            btnRange.Name = "btnRange";
            btnRange.Size = new Size(69, 23);
            btnRange.TabIndex = 7;
            btnRange.Text = "연속 적용";
            btnRange.UseVisualStyleBackColor = true;
            btnRange.Click += btnRange_Click;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(82, 227);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(141, 98);
            txtMessage.TabIndex = 5;
            // 
            // label12
            // 
            label12.Location = new Point(6, 227);
            label12.Name = "label12";
            label12.Size = new Size(70, 23);
            label12.TabIndex = 6;
            label12.Text = "내용";
            label12.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnApply
            // 
            btnApply.Location = new Point(97, 336);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(60, 23);
            btnApply.TabIndex = 7;
            btnApply.Text = "적용";
            btnApply.UseVisualStyleBackColor = true;
            btnApply.Click += btnApply_Click;
            // 
            // panel4
            // 
            panel4.Controls.Add(label17);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(3, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(1006, 35);
            panel4.TabIndex = 5;
            // 
            // label17
            // 
            label17.Dock = DockStyle.Fill;
            label17.Font = new Font("맑은 고딕", 15F);
            label17.Location = new Point(0, 0);
            label17.Name = "label17";
            label17.Size = new Size(1006, 35);
            label17.TabIndex = 0;
            label17.Text = "이 탭을 통해 등록된 날짜에는 스케줄링이 동작하지 않습니다.";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lbLeft, lbRunning, toolStripStatusLabel1, lbTime });
            statusStrip1.Location = new Point(0, 567);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1020, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // lbLeft
            // 
            lbLeft.Name = "lbLeft";
            lbLeft.Size = new Size(0, 17);
            // 
            // lbRunning
            // 
            lbRunning.Name = "lbRunning";
            lbRunning.Size = new Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Overflow = ToolStripItemOverflow.Always;
            toolStripStatusLabel1.Size = new Size(1005, 17);
            toolStripStatusLabel1.Spring = true;
            // 
            // lbTime
            // 
            lbTime.Name = "lbTime";
            lbTime.Size = new Size(0, 17);
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // runningTimer
            // 
            runningTimer.Interval = 1000;
            runningTimer.Tick += runningTimer_Tick;
            // 
            // notifyIcon1
            // 
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "디퓨저 제어기";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { 열기ToolStripMenuItem1, toolStripSeparator2, 종료ToolStripMenuItem1 });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(99, 54);
            // 
            // 열기ToolStripMenuItem1
            // 
            열기ToolStripMenuItem1.Name = "열기ToolStripMenuItem1";
            열기ToolStripMenuItem1.Size = new Size(98, 22);
            열기ToolStripMenuItem1.Text = "열기";
            열기ToolStripMenuItem1.Click += 열기ToolStripMenuItem1_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(95, 6);
            // 
            // 종료ToolStripMenuItem1
            // 
            종료ToolStripMenuItem1.Name = "종료ToolStripMenuItem1";
            종료ToolStripMenuItem1.Size = new Size(98, 22);
            종료ToolStripMenuItem1.Text = "종료";
            종료ToolStripMenuItem1.Click += 종료ToolStripMenuItem1_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1020, 589);
            Controls.Add(tabControl1);
            Controls.Add(statusStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(662, 628);
            Name = "frmMain";
            StartPosition = FormStartPosition.Manual;
            Text = "디퓨저 제어기";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            plInterval.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtStartH).EndInit();
            ((System.ComponentModel.ISupportInitialize)dtStartM).EndInit();
            ((System.ComponentModel.ISupportInitialize)dtEndH).EndInit();
            ((System.ComponentModel.ISupportInitialize)dtTermH).EndInit();
            ((System.ComponentModel.ISupportInitialize)dtTermM).EndInit();
            ((System.ComponentModel.ISupportInitialize)dtEndM).EndInit();
            ((System.ComponentModel.ISupportInitialize)dtTermS).EndInit();
            ((System.ComponentModel.ISupportInitialize)dtTermInterval).EndInit();
            plSchedule.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtTermSchedule).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateModelBindingSource).EndInit();
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)defYear).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel4.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label label2;
        private Panel panel1;
        private RadioButton rbSchedule;
        private Label label1;
        private StatusStrip statusStrip1;
        private Button btnHoliDay;
        private Button btnSelectedDel;
        private Panel panel2;
        private Button btnApply;
        private Label label12;
        private TextBox txtMessage;
        private Label label11;
        private TextBox txtDt;
        private MonthCalendar monthCalendar1;
        private DataGridView grid;
        private BindingSource dateModelBindingSource;
        private DataGridViewTextBoxColumn dowDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn Column1;
        private DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn yearDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn monthDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dayDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn messageDataGridViewTextBoxColumn;
        private Button btnDel;
        private Button btnRange;
        private Button btnSatDay;
        private Panel panel3;
        private Label label13;
        private NumericUpDown defYear;
        private GroupBox groupBox2;
        private Button btnSunday;
        private Label label14;
        private GroupBox groupBox1;
        private ComboBox cmbUsbList;
        private Label label16;
        private RadioButton rbInterval;
        private Panel panel4;
        private Label label17;
        private Panel plInterval;
        private Label label3;
        private NumericUpDown dtStartH;
        private Button btnStop1;
        private NumericUpDown dtStartM;
        private NumericUpDown dtEndH;
        private Button btnRun1;
        private NumericUpDown dtTermH;
        private Label label10;
        private NumericUpDown dtTermM;
        private Label label6;
        private NumericUpDown dtEndM;
        private Label label5;
        private NumericUpDown dtTermS;
        private Label label9;
        private NumericUpDown dtTermInterval;
        private Label label15;
        private Label label7;
        private Label label8;
        private Label label4;
        private Panel plSchedule;
        private ListBox lstBox;
        private Label label18;
        private Button btnStop2;
        private Label label19;
        private Button btnRun2;
        private Label label20;
        private NumericUpDown dtTermSchedule;
        private Button btnScDel;
        private Button btnScAdd;
        private ToolStripStatusLabel lbTime;
        private ToolStripStatusLabel lbLeft;
        private System.Windows.Forms.Timer timer1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Button btnRefresh;
        private Button btnOff;
        private Button btnOn;
        private System.Windows.Forms.Timer runningTimer;
        private ToolStripStatusLabel lbRunning;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 열기ToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem 종료ToolStripMenuItem1;
    }
}

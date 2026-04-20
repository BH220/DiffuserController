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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            tabControl1 = new TabControl();
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
            tabPage1 = new TabPage();
            button2 = new Button();
            button1 = new Button();
            label10 = new Label();
            label6 = new Label();
            label9 = new Label();
            label5 = new Label();
            label8 = new Label();
            label4 = new Label();
            label7 = new Label();
            label3 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            radioButton2 = new RadioButton();
            label1 = new Label();
            radioButton1 = new RadioButton();
            numericUpDown4 = new NumericUpDown();
            numericUpDown3 = new NumericUpDown();
            numericUpDown6 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            numericUpDown5 = new NumericUpDown();
            numericUpDown1 = new NumericUpDown();
            statusStrip1 = new StatusStrip();
            tabControl1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateModelBindingSource).BeginInit();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)defYear).BeginInit();
            groupBox1.SuspendLayout();
            tabPage1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(646, 567);
            tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(grid);
            tabPage2.Controls.Add(panel3);
            tabPage2.Controls.Add(panel2);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(638, 539);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "스케줄링 표";
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
            grid.Location = new Point(239, 35);
            grid.Name = "grid";
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.Size = new Size(396, 501);
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
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            yearDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            yearDataGridViewTextBoxColumn.HeaderText = "년";
            yearDataGridViewTextBoxColumn.Name = "yearDataGridViewTextBoxColumn";
            yearDataGridViewTextBoxColumn.Width = 60;
            // 
            // monthDataGridViewTextBoxColumn
            // 
            monthDataGridViewTextBoxColumn.DataPropertyName = "month";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            monthDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            monthDataGridViewTextBoxColumn.HeaderText = "월";
            monthDataGridViewTextBoxColumn.Name = "monthDataGridViewTextBoxColumn";
            monthDataGridViewTextBoxColumn.Width = 40;
            // 
            // dayDataGridViewTextBoxColumn
            // 
            dayDataGridViewTextBoxColumn.DataPropertyName = "day";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dayDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            dayDataGridViewTextBoxColumn.HeaderText = "일";
            dayDataGridViewTextBoxColumn.Name = "dayDataGridViewTextBoxColumn";
            dayDataGridViewTextBoxColumn.Width = 40;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.DataPropertyName = "DoW";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewTextBoxColumn1.HeaderText = "요일";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
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
            panel3.Location = new Point(239, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(396, 32);
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
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(5);
            panel2.Size = new Size(236, 533);
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
            // tabPage1
            // 
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(label10);
            tabPage1.Controls.Add(label6);
            tabPage1.Controls.Add(label9);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(label8);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(label7);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(panel1);
            tabPage1.Controls.Add(numericUpDown4);
            tabPage1.Controls.Add(numericUpDown3);
            tabPage1.Controls.Add(numericUpDown6);
            tabPage1.Controls.Add(numericUpDown2);
            tabPage1.Controls.Add(numericUpDown5);
            tabPage1.Controls.Add(numericUpDown1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(638, 539);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "제어판";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(307, 314);
            button2.Name = "button2";
            button2.Size = new Size(105, 42);
            button2.TabIndex = 4;
            button2.Text = "중지";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(179, 314);
            button1.Name = "button1";
            button1.Size = new Size(105, 42);
            button1.TabIndex = 4;
            button1.Text = "동작";
            button1.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.Location = new Point(228, 253);
            label10.Name = "label10";
            label10.Size = new Size(27, 23);
            label10.TabIndex = 3;
            label10.Text = "초";
            label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.Location = new Point(27, 253);
            label6.Name = "label6";
            label6.Size = new Size(100, 23);
            label6.TabIndex = 3;
            label6.Text = "분사 유지 시간";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            label9.Location = new Point(228, 198);
            label9.Name = "label9";
            label9.Size = new Size(27, 23);
            label9.TabIndex = 3;
            label9.Text = "초";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Location = new Point(27, 198);
            label5.Name = "label5";
            label5.Size = new Size(100, 23);
            label5.TabIndex = 3;
            label5.Text = "간격";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            label8.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold);
            label8.Location = new Point(228, 139);
            label8.Name = "label8";
            label8.Size = new Size(27, 23);
            label8.TabIndex = 3;
            label8.Text = ":";
            label8.TextAlign = ContentAlignment.TopCenter;
            // 
            // label4
            // 
            label4.Location = new Point(27, 143);
            label4.Name = "label4";
            label4.Size = new Size(100, 23);
            label4.TabIndex = 3;
            label4.Text = "종료 시간";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            label7.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold);
            label7.Location = new Point(228, 84);
            label7.Name = "label7";
            label7.Size = new Size(27, 23);
            label7.TabIndex = 3;
            label7.Text = ":";
            label7.TextAlign = ContentAlignment.TopCenter;
            // 
            // label3
            // 
            label3.Location = new Point(27, 88);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 3;
            label3.Text = "시작 시간";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Location = new Point(27, 30);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 3;
            label2.Text = "동작 구분";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            panel1.Controls.Add(radioButton2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(radioButton1);
            panel1.Location = new Point(179, 27);
            panel1.Name = "panel1";
            panel1.Size = new Size(283, 29);
            panel1.TabIndex = 2;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Dock = DockStyle.Left;
            radioButton2.Location = new Point(168, 0);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(105, 29);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "별도 제어 동작";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Left;
            label1.Location = new Point(129, 0);
            label1.Name = "label1";
            label1.Size = new Size(39, 29);
            label1.TabIndex = 2;
            label1.Text = " ";
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Dock = DockStyle.Left;
            radioButton1.Location = new Point(0, 0);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(129, 29);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "스케줄링 기반 동작";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // numericUpDown4
            // 
            numericUpDown4.Location = new Point(179, 253);
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(43, 23);
            numericUpDown4.TabIndex = 1;
            numericUpDown4.TextAlign = HorizontalAlignment.Center;
            numericUpDown4.Value = new decimal(new int[] { 23, 0, 0, 0 });
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(179, 198);
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(43, 23);
            numericUpDown3.TabIndex = 1;
            numericUpDown3.TextAlign = HorizontalAlignment.Center;
            numericUpDown3.Value = new decimal(new int[] { 23, 0, 0, 0 });
            // 
            // numericUpDown6
            // 
            numericUpDown6.Location = new Point(261, 143);
            numericUpDown6.Name = "numericUpDown6";
            numericUpDown6.Size = new Size(43, 23);
            numericUpDown6.TabIndex = 1;
            numericUpDown6.TextAlign = HorizontalAlignment.Center;
            numericUpDown6.Value = new decimal(new int[] { 23, 0, 0, 0 });
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(179, 143);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(43, 23);
            numericUpDown2.TabIndex = 1;
            numericUpDown2.TextAlign = HorizontalAlignment.Center;
            numericUpDown2.Value = new decimal(new int[] { 23, 0, 0, 0 });
            // 
            // numericUpDown5
            // 
            numericUpDown5.Location = new Point(261, 88);
            numericUpDown5.Name = "numericUpDown5";
            numericUpDown5.Size = new Size(43, 23);
            numericUpDown5.TabIndex = 1;
            numericUpDown5.TextAlign = HorizontalAlignment.Center;
            numericUpDown5.Value = new decimal(new int[] { 23, 0, 0, 0 });
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(179, 88);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(43, 23);
            numericUpDown1.TabIndex = 1;
            numericUpDown1.TextAlign = HorizontalAlignment.Center;
            numericUpDown1.Value = new decimal(new int[] { 23, 0, 0, 0 });
            // 
            // statusStrip1
            // 
            statusStrip1.Location = new Point(0, 567);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(646, 22);
            statusStrip1.TabIndex = 6;
            statusStrip1.Text = "statusStrip1";
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(646, 589);
            Controls.Add(tabControl1);
            Controls.Add(statusStrip1);
            MinimumSize = new Size(662, 628);
            Name = "frmMain";
            Text = "Form1";
            tabControl1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateModelBindingSource).EndInit();
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)defYear).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPage1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown1;
        private TabPage tabPage2;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Panel panel1;
        private RadioButton radioButton2;
        private Label label1;
        private RadioButton radioButton1;
        private NumericUpDown numericUpDown4;
        private NumericUpDown numericUpDown3;
        private NumericUpDown numericUpDown6;
        private NumericUpDown numericUpDown5;
        private Button button2;
        private Button button1;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
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
    }
}

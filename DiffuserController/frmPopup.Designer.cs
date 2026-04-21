namespace DiffuserController
{
    partial class frmPopup
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
            txtStart = new TextBox();
            label1 = new Label();
            label2 = new Label();
            monthCalendar1 = new MonthCalendar();
            txtDays = new TextBox();
            label3 = new Label();
            label4 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // txtStart
            // 
            txtStart.Location = new Point(118, 15);
            txtStart.Name = "txtStart";
            txtStart.ReadOnly = true;
            txtStart.Size = new Size(113, 23);
            txtStart.TabIndex = 0;
            txtStart.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 1;
            label1.Text = "시작일";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Location = new Point(12, 50);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 1;
            label2.Text = "종료일";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(118, 50);
            monthCalendar1.MaxSelectionCount = 1;
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 2;
            monthCalendar1.DateChanged += monthCalendar1_DateChanged;
            // 
            // txtDays
            // 
            txtDays.Location = new Point(118, 224);
            txtDays.Name = "txtDays";
            txtDays.ReadOnly = true;
            txtDays.Size = new Size(49, 23);
            txtDays.TabIndex = 0;
            txtDays.Text = "0";
            txtDays.TextAlign = HorizontalAlignment.Center;
            // 
            // label3
            // 
            label3.Location = new Point(12, 224);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 1;
            label3.Text = "범위";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            label4.Location = new Point(173, 224);
            label4.Name = "label4";
            label4.Size = new Size(100, 23);
            label4.TabIndex = 1;
            label4.Text = "일";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            button1.Location = new Point(118, 267);
            button1.Name = "button1";
            button1.Size = new Size(113, 34);
            button1.TabIndex = 3;
            button1.Text = "적용";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // frmPopup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(351, 315);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(txtDays);
            Controls.Add(txtStart);
            Controls.Add(monthCalendar1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmPopup";
            Text = "연속일의 마지막 일자 설정";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtStart;
        private Label label1;
        private Label label2;
        private MonthCalendar monthCalendar1;
        private TextBox txtDays;
        private Label label3;
        private Label label4;
        private Button button1;
    }
}
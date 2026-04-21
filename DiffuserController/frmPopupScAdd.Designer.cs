namespace DiffuserController
{
    partial class frmPopupScAdd
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
            label1 = new Label();
            txtTime = new TextBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 0;
            label1.Text = "시간 입력";
            // 
            // txtTime
            // 
            txtTime.Location = new Point(125, 17);
            txtTime.Name = "txtTime";
            txtTime.Size = new Size(60, 23);
            txtTime.TabIndex = 1;
            txtTime.TextAlign = HorizontalAlignment.Center;
            txtTime.KeyDown += txtTime_KeyDown;
            // 
            // button1
            // 
            button1.Location = new Point(12, 99);
            button1.Name = "button1";
            button1.Size = new Size(173, 38);
            button1.TabIndex = 2;
            button1.Text = "추가 후 계속(Ctrl + Enter)";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(12, 55);
            button2.Name = "button2";
            button2.Size = new Size(173, 38);
            button2.TabIndex = 2;
            button2.Text = "추가 ( Enter )";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // frmPopupScAdd
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(198, 151);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(txtTime);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "frmPopupScAdd";
            Text = "스케줄링 시간 추가";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtTime;
        private Button button1;
        private Button button2;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiffuserController
{
    public partial class frmPopupScAdd : Form
    {
        DateTime dtNow = DateTime.MinValue;
        public event EventHandler<DateTime> AddTimeEvent;

        public frmPopupScAdd()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {//단건추가
            if (Validation())
            {
                AddTimeEvent?.Invoke(this, dtNow);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {//연속추가
            if (Validation())
            {
                AddTimeEvent?.Invoke(this, dtNow);
                txtTime.Text = "";
                txtTime.Focus();
            }
        }

        private void txtTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                button2_Click(null, null);
            }
        }

        private bool Validation()
        {
            if (!TryParseTime(txtTime.Text, out int hour, out int minute))
            {
                MessageBox.Show("시간 형식이 올바르지 않습니다.\n예: 9:30, 23:59",
                                "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTime.Focus();
                txtTime.SelectAll();
                return false;
            }
            return true;
        }

        private bool TryParseTime(string input, out int hour, out int minute)
        {
            hour = 0;
            minute = 0;

            if (string.IsNullOrWhiteSpace(input)) return false;

            var parts = input.Split(':');
            if (parts.Length != 2) return false;

            if (!int.TryParse(parts[0].Trim(), out hour)) return false;
            if (!int.TryParse(parts[1].Trim(), out minute)) return false;

            if (hour < 0 || hour > 23) return false;
            if (minute < 0 || minute > 59) return false;

            dtNow = new DateTime(2000, 1, 1, hour, minute, 0);
            return true;
        }
    }
}

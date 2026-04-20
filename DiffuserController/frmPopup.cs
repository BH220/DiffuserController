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
    public partial class frmPopup : Form
    {
        [Obsolete("디자이너 모드 때문에 남겨둠")]
        public frmPopup()
        {
            InitializeComponent();
        }

        DateTime startDate = DateTime.MinValue;
        public DateTime EndDate { get; private set; }
        public frmPopup(DateTime startDate)
        {
            InitializeComponent();
            this.startDate = startDate;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtStart.Text = startDate.ToString("yyyy-MM-dd (ddd)");
            monthCalendar1.MinDate = startDate;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            EndDate = monthCalendar1.SelectionStart;
            txtDays.Text = (EndDate - startDate).Days.ToString();
        }
    }
}

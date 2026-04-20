
using System.Windows.Forms;

namespace DiffuserController
{
    public partial class frmMain : Form
    {
        private CheckBox _headerCheckBox = null!;
        private bool _syncingCheckState = false;  // 무한 루프 방지


        public frmMain()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            InitGrid();
            base.OnLoad(e);
            InitLoadData();
            monthCalendar1_DateChanged(null, null);
        }

        private void InitGrid()
        {
            grid.AutoGenerateColumns = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.EnableHeadersVisualStyles = false;  // ← 추가

            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            grid.ColumnHeadersHeight = 32;  // 원하는 높이로 조절


            var headerStyle = grid.ColumnHeadersDefaultCellStyle;
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            headerStyle.BackColor = SystemColors.Control;
            headerStyle.ForeColor = SystemColors.ControlText;
            headerStyle.SelectionBackColor = headerStyle.BackColor;
            headerStyle.SelectionForeColor = headerStyle.ForeColor;

            foreach (DataGridViewColumn col in grid.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            grid.DataError += DataGridView1_DataError;
            grid.CurrentCellDirtyStateChanged += Grid_CurrentCellDirtyStateChanged;

            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 243, 255);
            grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 30, 30);
            SetupHeaderCheckBox();
        }

        private void SetupHeaderCheckBox()
        {
            _headerCheckBox = new CheckBox
            {
                Size = new Size(14, 14),
                BackColor = Color.Transparent,
                Padding = Padding.Empty,
                Margin = Padding.Empty,
                ThreeState = true
            };

            grid.Controls.Add(_headerCheckBox);
            PositionHeaderCheckBox();

            _headerCheckBox.CheckedChanged += HeaderCheckBox_CheckedChanged;
            grid.Scroll += (s, e) => PositionHeaderCheckBox();
            grid.ColumnWidthChanged += (s, e) => PositionHeaderCheckBox();
            grid.CellValueChanged += Grid_CellValueChanged;
            grid.RowsAdded += (s, e) => UpdateHeaderCheckBoxState();
            grid.RowsRemoved += (s, e) => UpdateHeaderCheckBoxState();
        }

        private void PositionHeaderCheckBox()
        {
            var rect = grid.GetCellDisplayRectangle(Column1.Index, -1, true);
            _headerCheckBox.Location = new Point(
                rect.Left + (rect.Width - _headerCheckBox.Width) / 2,
                rect.Top + (rect.Height - _headerCheckBox.Height) / 2
            );
        }

        private void HeaderCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            if (_syncingCheckState) return;

            _syncingCheckState = true;
            try
            {
                foreach (DataGridViewRow row in grid.Rows)
                {
                    row.Cells[Column1.Index].Value = _headerCheckBox.Checked;
                }
            }
            finally
            {
                _syncingCheckState = false;
            }
        }

        private void Grid_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (_syncingCheckState) return;
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex != Column1.Index) return;

            UpdateHeaderCheckBoxState();
        }

        private void UpdateHeaderCheckBoxState()
        {
            int total = grid.Rows.Count;
            int checkedCount = grid.Rows.Cast<DataGridViewRow>()
                .Count(r => Convert.ToBoolean(r.Cells[Column1.Index].Value));

            CheckState state = checkedCount == 0 ? CheckState.Unchecked
                             : checkedCount == total ? CheckState.Checked
                             : CheckState.Indeterminate;

            _syncingCheckState = true;
            try { _headerCheckBox.CheckState = state; }
            finally { _syncingCheckState = false; }
        }



        private void SetHeaderCheckedSilently(bool isChecked)
        {
            _syncingCheckState = true;
            try { _headerCheckBox.Checked = isChecked; }
            finally { _syncingCheckState = false; }
        }

        private void Grid_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (grid.CurrentCell is DataGridViewCheckBoxCell)
                grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void DataGridView1_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void InitLoadData()
        {
            dateModelBindingSource.DataSource = DateHelper.Instance.Dates;
            grid.Refresh();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            txtDt.Text = monthCalendar1.SelectionStart.ToString("yyyy-MM-dd (ddd)");
            txtMessage.Text = DateHelper.Instance.GetMessage(monthCalendar1.SelectionStart);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnRange_Click(object sender, EventArgs e)
        {

        }

        private void btnApply_Click(object sender, EventArgs e)
        {

        }

        private void btnDel_Click(object sender, EventArgs e)
        {

        }

        private void btnCheckHoliDay_Click(object sender, EventArgs e)
        {

        }

        private void btnCheckSat_Click(object sender, EventArgs e)
        {

        }

        private void btnSelectedDel_Click(object sender, EventArgs e)
        {

        }
    }
}


using System.Web;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            grid.CellContentDoubleClick += Grid_CellContentDoubleClick;
            grid.CellBeginEdit += Grid_CellBeginEdit;
        }

        private void Grid_CellBeginEdit(object? sender, DataGridViewCellCancelEventArgs e)
        {
            // 체크박스 컬럼이 아니면 편집 진입 차단
            if (e.ColumnIndex != Column1.Index)
            {
                e.Cancel = true;
            }
        }

        private void Grid_CellContentDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;  // 헤더 더블클릭 방지
            
            DateModel selectedModel = (DateModel)grid.Rows[e.RowIndex].DataBoundItem;
            if (selectedModel != null)
            {
                monthCalendar1.SetDate(selectedModel.Date.ToDateTime(TimeOnly.MinValue));
            }
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
            if (string.IsNullOrEmpty(txtMessage.Text))
            {
                MessageBox.Show("스케줄링 제외 사유를 입력하세요");
                return;
            }
            else
            {
                using (frmPopup frm = new frmPopup(monthCalendar1.SelectionStart))
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        ContinueSave(monthCalendar1.SelectionStart, frm.EndDate, txtMessage.Text);
                    }
                }
            }
        }

        private void ContinueSave(DateTime selectionStart, DateTime endDate, string text)
        {
            DateTime dt = selectionStart;
            while (true)
            {
                if (dt.Date > endDate.Date)
                    break;
                var find = DateHelper.Instance.Dates.FirstOrDefault(x => x.Date == DateOnly.FromDateTime(dt.Date));
                if (find != null)
                {
                    find.Message = text;
                }
                else
                {
                    DateModel dm = new DateModel();
                    dm.Date = DateOnly.FromDateTime(dt);
                    dm.Message = text;
                    DateHelper.Instance.Dates.Add(dm);
                }
                dt = dt.AddDays(1);
            }
            DateHelper.Instance.Save();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            var find = DateHelper.Instance.Dates.FirstOrDefault(x => x.Date == DateOnly.FromDateTime(monthCalendar1.SelectionStart.Date));
            if(find != null)
            {
                find.Message = txtMessage.Text;
            }
            else
            {
                DateModel dm = new DateModel();
                dm.Date = DateOnly.FromDateTime(monthCalendar1.SelectionStart);
                dm.Message = txtMessage.Text;
                DateHelper.Instance.Dates.Add(dm);
            }
            DateHelper.Instance.Save();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            var find = DateHelper.Instance.Dates.FirstOrDefault(x => x.Date == DateOnly.FromDateTime(monthCalendar1.SelectionStart.Date));
            if (find != null)
            {
                if (MessageBox.Show("선택된 일정을 삭제 하시겠습니가?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    DateHelper.Instance.Dates.Remove(find);
                    DateHelper.Instance.Save();
                    txtMessage.Text = "";
                }
            }
        }

        private void btnSelectedDel_Click(object sender, EventArgs e)
        {
            List<DateModel> checkedItems = grid.Rows
      .Cast<DataGridViewRow>()
      .Where(r => Convert.ToBoolean(r.Cells[Column1.Index].Value))
      .Select(r => (DateModel)r.DataBoundItem)
      .ToList();

            if (checkedItems.Count > 0)
            {
                if (MessageBox.Show("선택된 일정을 모두 삭제 하시겠습니가?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    foreach (var item in checkedItems)
                    {
                        DateHelper.Instance.Dates.Remove(item);
                    }
                    DateHelper.Instance.Save();
                    txtMessage.Text = "";
                }
            }
        }

        private void btnSunday_Click(object sender, EventArgs e)
        {
            DateTime dt = new DateTime((int)defYear.Value, 1, 1);
            while(true)
            {
                if(dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    break;
                }
                dt = dt.AddDays(1);
            }
            while(true)
            {
                if(dt.Year> defYear.Value)
                {
                    break;
                }
                DateModel dm = new DateModel();
                dm.Date = DateOnly.FromDateTime(dt);
                dm.Message = "쉬는날 - 일요일";
                var find = DateHelper.Instance.Dates.FirstOrDefault(x => x.Date == dm.Date);
                if(find != null)
                {
                    find.Message = dm.Message;
                }
                else
                {
                    DateHelper.Instance.Dates.Add(dm);
                }
                dt = dt.AddDays(7);
            }
            DateHelper.Instance.Save();
            
        }

        private void btnSatDay_Click(object sender, EventArgs e)
        {
            DateTime dt = new DateTime((int)defYear.Value, 1, 1);
            while (true)
            {
                if (dt.DayOfWeek == DayOfWeek.Saturday)
                {
                    break;
                }
                dt = dt.AddDays(1);
            }
            while (true)
            {
                if (dt.Year > defYear.Value)
                {
                    break;
                }
                DateModel dm = new DateModel();
                dm.Date = DateOnly.FromDateTime(dt);
                dm.Message = "쉬는날 - 토요일";
                var find = DateHelper.Instance.Dates.FirstOrDefault(x => x.Date == dm.Date);
                if (find != null)
                {
                    find.Message = dm.Message;
                }
                else
                {
                    DateHelper.Instance.Dates.Add(dm);
                }
                dt = dt.AddDays(7);
            }
            DateHelper.Instance.Save();
        }

        private async void btnHoliDay_Click(object sender, EventArgs e)
        {
            var holidays = await HolidayApi.GetHolidaysAsync((int)defYear.Value);

            foreach (var h in holidays)
            {
                DateModel dm = new DateModel();
                dm.Date = h.Date;
                dm.Message = h.DateName;
                var find = DateHelper.Instance.Dates.FirstOrDefault(x => x.Date == dm.Date);
                if (find != null)
                {
                    find.Message = dm.Message;
                }
                else
                {
                    DateHelper.Instance.Dates.Add(dm);
                }
            }

            DateHelper.Instance.Save();
        }
    }

    public static class HolidayApi
    {
        private static readonly HttpClient _http = new();

        public static async Task<List<HolidayItem>> GetHolidaysAsync(int year)
        {
            string url = $"https://apis.data.go.kr/B090041/openapi/service/SpcdeInfoService/getRestDeInfo?serviceKey={Program.ApiKey}&solYear={year}&numOfRows=500";

            string xml = await _http.GetStringAsync(url);

            var serializer = new XmlSerializer(typeof(HolidayResponse));
            using var reader = new StringReader(xml);
            var response = (HolidayResponse?)serializer.Deserialize(reader);

            if (response?.Header.ResultCode != "00")
                throw new Exception($"API 오류: {response?.Header.ResultMsg}");

            return response.Body.Items;
        }
    }
}

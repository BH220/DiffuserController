
using System.Web;
using System.Windows.Forms;
using System.Xml.Serialization; 

namespace DiffuserController
{
    public partial class frmMain : Form
    {
        private CheckBox _headerCheckBox = null!;
        private bool _syncingCheckState = false;  // 무한 루프 방지
        private bool IsRunning = true;

        public frmMain()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            defYear.Value = DateTime.Now.Year;
            plSchedule.Location = plInterval.Location;
            InitGrid();
            base.OnLoad(e);
            LoadSetting();
            monthCalendar1_DateChanged(null, null);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            timer1.Start();
            NumericUpDown[] items = new NumericUpDown[] {
                dtStartH, dtStartM, dtEndH, dtEndM, dtTermH, dtTermM, dtTermS, dtTermInterval, dtTermSchedule
            };
            foreach (var item in items)
            {
                item.ValueChanged += ValueChanged;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTime.Text = $"{DateTime.Now.ToString("yy-MM-dd HH:mm:ss")}";

            if (IsRunning)
            {
                lbLeft.Text = $"동작중";
            }
            else
            {
                lbLeft.Text = $"중지중";
            }


            //1시간 2분 4초 뒤 5초간 분사 예정..
            //분사 중... 남은 시간( 3초 )
            //[yy-MM-dd HH:mm:ss]
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
            dateModelBindingSource.DataSource = LocalDbManager.Instance.Dates;
            grid.Refresh();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            txtDt.Text = monthCalendar1.SelectionStart.ToString("yyyy-MM-dd (ddd)");
            txtMessage.Text = LocalDbManager.Instance.GetMessage(monthCalendar1.SelectionStart);
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
                var find = LocalDbManager.Instance.Dates.FirstOrDefault(x => x.Date == DateOnly.FromDateTime(dt.Date));
                if (find != null)
                {
                    find.Message = text;
                }
                else
                {
                    DateModel dm = new DateModel();
                    dm.Date = DateOnly.FromDateTime(dt);
                    dm.Message = text;
                    LocalDbManager.Instance.Dates.Add(dm);
                }
                dt = dt.AddDays(1);
            }
            LocalDbManager.Instance.Save();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            var find = LocalDbManager.Instance.Dates.FirstOrDefault(x => x.Date == DateOnly.FromDateTime(monthCalendar1.SelectionStart.Date));
            if (find != null)
            {
                find.Message = txtMessage.Text;
            }
            else
            {
                DateModel dm = new DateModel();
                dm.Date = DateOnly.FromDateTime(monthCalendar1.SelectionStart);
                dm.Message = txtMessage.Text;
                LocalDbManager.Instance.Dates.Add(dm);
            }
            LocalDbManager.Instance.Save();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            var find = LocalDbManager.Instance.Dates.FirstOrDefault(x => x.Date == DateOnly.FromDateTime(monthCalendar1.SelectionStart.Date));
            if (find != null)
            {
                if (MessageBox.Show("선택된 일정을 삭제 하시겠습니가?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    LocalDbManager.Instance.Dates.Remove(find);
                    LocalDbManager.Instance.Save();
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
                        LocalDbManager.Instance.Dates.Remove(item);
                    }
                    LocalDbManager.Instance.Save();
                    txtMessage.Text = "";
                }
            }
        }

        private void btnSunday_Click(object sender, EventArgs e)
        {
            DateTime dt = new DateTime((int)defYear.Value, 1, 1);
            while (true)
            {
                if (dt.DayOfWeek == DayOfWeek.Sunday)
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
                dm.Message = "쉬는날 - 일요일";
                var find = LocalDbManager.Instance.Dates.FirstOrDefault(x => x.Date == dm.Date);
                if (find != null)
                {
                    find.Message = dm.Message;
                }
                else
                {
                    LocalDbManager.Instance.Dates.Add(dm);
                }
                dt = dt.AddDays(7);
            }
            LocalDbManager.Instance.Save();

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
                var find = LocalDbManager.Instance.Dates.FirstOrDefault(x => x.Date == dm.Date);
                if (find != null)
                {
                    find.Message = dm.Message;
                }
                else
                {
                    LocalDbManager.Instance.Dates.Add(dm);
                }
                dt = dt.AddDays(7);
            }
            LocalDbManager.Instance.Save();
        }

        private async void btnHoliDay_Click(object sender, EventArgs e)
        {
            var holidays = await HolidayApi.GetHolidaysAsync((int)defYear.Value);

            foreach (var h in holidays)
            {
                DateModel dm = new DateModel();
                dm.Date = h.Date;
                dm.Message = h.DateName;
                var find = LocalDbManager.Instance.Dates.FirstOrDefault(x => x.Date == dm.Date);
                if (find != null)
                {
                    find.Message = dm.Message;
                }
                else
                {
                    LocalDbManager.Instance.Dates.Add(dm);
                }
            }

            LocalDbManager.Instance.Save();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            plInterval.Visible = true;
            plSchedule.Visible = false;
            SaveSetting();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            plInterval.Visible = false;
            plSchedule.Visible = true;
            SaveSetting();
        }

        private void btnScAdd_Click(object sender, EventArgs e)
        {
            using (frmPopupScAdd frm = new frmPopupScAdd())
            {
                frm.AddTimeEvent += Frm_AddTimeEvent;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
            }
        }

        private void Frm_AddTimeEvent(object? sender, DateTime e)
        {
            string val = e.ToString("HH:mm");
            if (lstBox.Items.Contains(val) == false)
            {
                lstBox.Items.Add(val);
                SaveSetting();
            }
        }

        private void btnScDel_Click(object sender, EventArgs e)
        {
            if (lstBox.SelectedItems.Count <= 0)
            {
                MessageBox.Show("삭제 대상을 선택해 주세요");
                return;
            }
            if (MessageBox.Show("선택항목을 삭제 하시겠습니까?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                foreach (var item in lstBox.SelectedItems)
                {
                    lstBox.Items.Remove(item);
                }
                SaveSetting();
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            IsRunning = true;
            btnRun1.Enabled = btnRun2.Enabled = !IsRunning;
            btnStop1.Enabled = btnStop2.Enabled = IsRunning;
            SaveSetting();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            IsRunning = false;
            btnRun1.Enabled = btnRun2.Enabled = !IsRunning;
            btnStop1.Enabled = btnStop2.Enabled = IsRunning;
            SaveSetting();
        }

        private void cmbUsbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveSetting();
        }

        private void SaveSetting()
        {
            LocalDbManager.Instance.ControlModel.SelectedUSB = cmbUsbList.Text;

            LocalDbManager.Instance.ControlModel.IsInterval = radioButton1.Checked;
            LocalDbManager.Instance.ControlModel.StartAt = new DateTime(2000, 1, 1, (int)dtStartH.Value, (int)dtStartM.Value, 0);
            LocalDbManager.Instance.ControlModel.EndAt = new DateTime(2000, 1, 1, (int)dtEndH.Value, (int)dtEndM.Value, 0);
            LocalDbManager.Instance.ControlModel.IntervalSecond = ((int)dtTermH.Value * 60 * 60) + ((int)dtTermM.Value * 60) + ((int)dtTermS.Value);
            LocalDbManager.Instance.ControlModel.IntervalMaintainSecond = (int)dtTermInterval.Value;

            LocalDbManager.Instance.ControlModel.IsSchedule = !radioButton1.Checked;
            List<string> items = new List<string>();
            foreach (var itm in lstBox.Items)
            {
                items.Add(itm.ToString());
            }
            LocalDbManager.Instance.ControlModel.ScheduleTimes = items;
            LocalDbManager.Instance.ControlModel.ScheduleMaintainSecond = (int)dtTermSchedule.Value;
            LocalDbManager.Instance.Save();
        }

        private void LoadSetting()
        {
            int idx = -1;
            bool find = false;
            foreach (var itm in lstBox.Items)
            {
                if (itm.ToString() == LocalDbManager.Instance.ControlModel.SelectedUSB)
                {
                    find = true;
                }
                idx++;
                if (find)
                    break;
            }
            cmbUsbList.SelectedIndex = idx;

            if (LocalDbManager.Instance.ControlModel.IsInterval)
                radioButton1.Checked = true;
            else
                radioButton2.Checked = true;
            dtStartH.Value = LocalDbManager.Instance.ControlModel.StartAt.Hour;
            dtStartM.Value = LocalDbManager.Instance.ControlModel.StartAt.Minute;

            dtEndH.Value = LocalDbManager.Instance.ControlModel.EndAt.Hour;
            dtEndM.Value = LocalDbManager.Instance.ControlModel.EndAt.Minute;

            dtTermH.Value = dtTermM.Value = dtTermS.Value = 0;
            if (LocalDbManager.Instance.ControlModel.IntervalSecond < 60)
                dtTermH.Value = LocalDbManager.Instance.ControlModel.IntervalSecond;
            else if(LocalDbManager.Instance.ControlModel.IntervalSecond < (60*60))
                dtTermM.Value = LocalDbManager.Instance.ControlModel.IntervalSecond / 60;
            else
                dtTermS.Value = LocalDbManager.Instance.ControlModel.IntervalSecond / 360;

            dtTermInterval.Value = LocalDbManager.Instance.ControlModel.IntervalMaintainSecond;

            LocalDbManager.Instance.ControlModel.IsSchedule = !radioButton1.Checked;
            foreach (var itm in LocalDbManager.Instance.ControlModel.ScheduleTimes)
            {
                lstBox.Items.Add(itm);
            }
            dtTermSchedule.Value = LocalDbManager.Instance.ControlModel.ScheduleMaintainSecond;

            InitLoadData();
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            SaveSetting();
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

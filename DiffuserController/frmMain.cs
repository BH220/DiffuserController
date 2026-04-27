
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.IO.Ports;
using System.Management;
using System.Text.RegularExpressions;
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
        DateTime dtToday = DateTime.Now;
        private bool _isRealClose = false;
        private SerialPort? _port;
        int runningSec = 0;

        List<DateTime> lstTargetDatetime = new List<DateTime>();

        public frmMain()
        {
            InitializeComponent();
            SetFormPosition();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
#if DEBUG
            btnOn.Visible = btnOff.Visible = true;
#else
            RegistOnString();
#endif

        }

        private void SetFormPosition()
        {
            var screen = Screen.PrimaryScreen.WorkingArea; // 작업표시줄 제외한 영역
            Left = screen.Right - Width + 10;   // 우측 여백 10px
            Top = screen.Bottom - Height + 10;  // 하단 여백 10px
        }

        private void RegistOnString()
        {
            try
            {
                string exePath = Application.ExecutablePath;
                string taskName = "DiffuserController";

                string xml = $@"<?xml version=""1.0"" encoding=""UTF-16""?>
<Task version=""1.2"" xmlns=""http://schemas.microsoft.com/windows/2004/02/mit/task"">
  <RegistrationInfo>
    <Description>디퓨저 제어기</Description>
  </RegistrationInfo>
  <Triggers>
    <BootTrigger>
      <Enabled>true</Enabled>
    </BootTrigger>
  </Triggers>
  <Settings>
    <MultipleInstancesPolicy>IgnoreNew</MultipleInstancesPolicy>
    <DisallowStartIfOnBatteries>false</DisallowStartIfOnBatteries>
    <StopIfGoingOnBatteries>false</StopIfGoingOnBatteries>
    <ExecutionTimeLimit>PT0S</ExecutionTimeLimit>
    <RestartOnFailure>
      <Interval>PT10M</Interval>
      <Count>100</Count>
    </RestartOnFailure>
  </Settings>
  <Actions>
    <Exec>
      <Command>{exePath}</Command>
    </Exec>
  </Actions>
  <Principals>
    <Principal>
      <LogonType>InteractiveToken</LogonType>
      <RunLevel>HighestAvailable</RunLevel>
    </Principal>
  </Principals>
</Task>";

                // XML 임시 파일로 저장
                string xmlPath = Path.Combine(Path.GetTempPath(), "relay_task.xml");
                File.WriteAllText(xmlPath, xml, System.Text.Encoding.Unicode);

                // schtasks로 등록
                var psi = new ProcessStartInfo
                {
                    FileName = "schtasks.exe",
                    Arguments = $"/Create /TN \"{taskName}\" /XML \"{xmlPath}\" /F",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using var process = Process.Start(psi);
                process!.WaitForExit();

                File.Delete(xmlPath);

                if (process.ExitCode != 0)
                    MessageBox.Show("등록 실패. 관리자 권한으로 실행해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUSB()
        {
            List<ComPortItem> ports = new List<ComPortItem>();

            using var searcher = new ManagementObjectSearcher(
                "SELECT Name, DeviceID FROM Win32_PnPEntity WHERE Name LIKE '%(COM%'");

            foreach (ManagementObject obj in searcher.Get())
            {
                var name = obj["Name"]?.ToString();   // "USB-SERIAL CH340 (COM3)"
                var deviceId = obj["DeviceID"]?.ToString();

                if (name == null) continue;


                // COM 포트 번호 추출
                var match = Regex.Match(name, @"\(COM\d+\)");
                if (match.Success)
                {
                    ComPortItem ci = new ComPortItem();
                    var comPort = match.Value.Trim('(', ')').Trim();
                    var deviceName = name.Replace(match.Value, "").Trim();
                    var display = $"{comPort} : {deviceName}";
                    ci.Name = name;
                    ci.ComPort = comPort;
                    ci.Display = display;
                    ports.Add(ci);
                }
            }


            cmbUsbList.Items.Clear();

            foreach (var ci in ports)
            {
                // 표시: "USB-SERIAL CH340 (COM3)"
                // 실제 사용: "COM3"
                cmbUsbList.Items.Add(new ComPortItem
                {
                    ComPort = ci.ComPort,
                    Name = ci.Name,
                    Display = ci.Display
                });
            }

            cmbUsbList.DisplayMember = "Display";
            cmbUsbList.ValueMember = "ComPort";
        }



        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            dtToday = DateTime.Now;
            defYear.Value = DateTime.Now.Year;
            plSchedule.Location = plInterval.Location;
            InitGrid();
            LoadUSB();
            LoadSetting();
            SettingTargetDatetime();
            monthCalendar1_DateChanged(null, null);

            timer1.Start();
            NumericUpDown[] items = new NumericUpDown[] {
                dtStartH, dtStartM, dtEndH, dtEndM, dtTermH, dtTermM, dtTermS, dtTermInterval, dtTermSchedule
            };
            foreach (var item in items)
            {
                item.ValueChanged += ValueChanged;
            }
            cmbUsbList.SelectedIndexChanged += cmbUsbList_SelectedIndexChanged;
            SetEnabled();
            cmbUsbList_SelectedIndexChanged(null, null);
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTime.Text = $"{DateTime.Now.ToString("yy-MM-dd (ddd) HH:mm:ss")}";
            if (dtToday.Day != DateTime.Now.Day)
            {
                SettingTargetDatetime();
                dtToday = DateTime.Now;
            }

            DateModel dm = LocalDbManager.Instance.Dates.FirstOrDefault(x => x.Date == DateOnly.FromDateTime(DateTime.Now));
            if (dm == null)
            {//동작해야 하는 날
                if (IsRunning)
                {
                    DateTime? target = lstTargetDatetime.Where(x => x >= DateTime.Now).FirstOrDefault();
                    if (target != DateTime.MinValue)
                    {
                        TimeSpan ts = target.Value - DateTime.Now;
                        if (ts.Hours > 0)
                            lbLeft.Text = $"{ts.Hours}시간 {ts.Minutes}분 {ts.Seconds}초 뒤 {dtTermInterval.Value}초간 분사 예정..";
                        else if (ts.Minutes > 0)
                            lbLeft.Text = $"{ts.Minutes}분 {ts.Seconds}초 뒤 {dtTermInterval.Value}초간 분사 예정..";
                        else if (ts.Seconds > 0)
                            lbLeft.Text = $"{ts.Seconds}초 뒤 {dtTermInterval.Value}초간 분사 예정..";
                        else
                        {
                            if (rbInterval.Checked)
                                runningSec = (int)dtTermInterval.Value;
                            else
                                runningSec = (int)dtTermSchedule.Value;
                            runningTimer.Enabled = true;
                            runningTimer.Start();
                            btnOn_Click(null, null);
                        }
                    }
                    else
                    {
                        lbLeft.Text = $"오늘 동작 일정 종료";
                    }
                }
                else
                {
                    lbLeft.Text = $"중지됨";
                }
            }
            else
            {
                lbLeft.Text = $"오늘은 동작 제외 날짜 입니다. ( {dm.Message} )";
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

        private void SetEnabled()
        {
            List<Control> lstControl = new List<Control>();
            lstControl.Add(cmbUsbList);
            lstControl.Add(btnRefresh);
            lstControl.Add(rbInterval);
            lstControl.Add(rbSchedule);
            lstControl.Add(dtStartH);
            lstControl.Add(dtStartM);
            lstControl.Add(dtEndH);
            lstControl.Add(dtEndM);
            lstControl.Add(dtTermH);
            lstControl.Add(dtTermM);
            lstControl.Add(dtTermS);
            lstControl.Add(dtTermSchedule);
            lstControl.Add(lstBox);
            lstControl.Add(btnScAdd);
            lstControl.Add(btnDel);
            lstControl.Add(dtTermInterval);
            lstControl.ForEach(x => x.Enabled = !IsRunning);
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            IsRunning = true;
            btnRun1.Enabled = btnRun2.Enabled = !IsRunning;
            btnStop1.Enabled = btnStop2.Enabled = IsRunning;
            SaveSetting();
            SettingTargetDatetime();
            SetEnabled();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            IsRunning = false;
            btnRun1.Enabled = btnRun2.Enabled = !IsRunning;
            btnStop1.Enabled = btnStop2.Enabled = IsRunning;
            SaveSetting();
            SetEnabled();
        }

        private void cmbUsbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveSetting();
            ComPortItem ci = cmbUsbList.SelectedItem as ComPortItem;
            if (ci != null)
            {
                _port = new SerialPort(ci.ComPort, 9600);
                _port.Open();
            }
        }

        private void SaveSetting()
        {
            LocalDbManager.Instance.ControlModel.SelectedUSB = cmbUsbList.Text;

            LocalDbManager.Instance.ControlModel.IsInterval = rbInterval.Checked;
            LocalDbManager.Instance.ControlModel.StartAt = new DateTime(2000, 1, 1, (int)dtStartH.Value, (int)dtStartM.Value, 0);
            LocalDbManager.Instance.ControlModel.EndAt = new DateTime(2000, 1, 1, (int)dtEndH.Value, (int)dtEndM.Value, 0);
            LocalDbManager.Instance.ControlModel.IntervalSecond = ((int)dtTermH.Value * 60 * 60) + ((int)dtTermM.Value * 60) + ((int)dtTermS.Value);
            LocalDbManager.Instance.ControlModel.IntervalMaintainSecond = (int)dtTermInterval.Value;

            LocalDbManager.Instance.ControlModel.IsSchedule = !rbInterval.Checked;
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
            FindUsb();

            dtStartH.Value = LocalDbManager.Instance.ControlModel.StartAt.Hour;
            dtStartM.Value = LocalDbManager.Instance.ControlModel.StartAt.Minute;

            dtEndH.Value = LocalDbManager.Instance.ControlModel.EndAt.Hour;
            dtEndM.Value = LocalDbManager.Instance.ControlModel.EndAt.Minute;

            dtTermH.Value = dtTermM.Value = dtTermS.Value = 0;

            int h = LocalDbManager.Instance.ControlModel.IntervalSecond / 3600;
            int m = (LocalDbManager.Instance.ControlModel.IntervalSecond % 3600) / 60;
            int s = (LocalDbManager.Instance.ControlModel.IntervalSecond % 3600) % 60;
            dtTermH.Value = h;
            dtTermM.Value = m;
            dtTermS.Value = s;
            dtTermInterval.Value = LocalDbManager.Instance.ControlModel.IntervalMaintainSecond;

            LocalDbManager.Instance.ControlModel.IsSchedule = !rbInterval.Checked;
            foreach (var itm in LocalDbManager.Instance.ControlModel.ScheduleTimes)
            {
                lstBox.Items.Add(itm);
            }
            dtTermSchedule.Value = LocalDbManager.Instance.ControlModel.ScheduleMaintainSecond;

            if (LocalDbManager.Instance.ControlModel.IsInterval)
                rbInterval.Checked = true;
            else
                rbSchedule.Checked = true;
            InitLoadData();

            SettingTargetDatetime();
        }

        private void FindUsb()
        {
            int idx = -1;
            bool find = false;
            foreach (ComPortItem itm in cmbUsbList.Items)
            {
                if (itm.Display == LocalDbManager.Instance.ControlModel.SelectedUSB)
                {
                    find = true;
                }
                idx++;
                if (find)
                    break;
            }
            cmbUsbList.SelectedIndex = idx;
        }

        private void SettingTargetDatetime()
        {
            lstTargetDatetime = new List<DateTime>();
            int y = DateTime.Now.Year;
            int m = DateTime.Now.Month;
            int d = DateTime.Now.Day;
            if (rbInterval.Checked)
            {
                DateTime dt = new DateTime(y, m, d, (int)dtStartH.Value, (int)dtStartM.Value, 0);
                DateTime edt = new DateTime(y, m, d, (int)dtEndH.Value, (int)dtEndM.Value, 0);

                while (true)
                {
                    if (dt > edt)
                        break;
                    lstTargetDatetime.Add(dt);
                    dt = dt.AddHours((int)dtTermH.Value);
                    dt = dt.AddMinutes((int)dtTermM.Value);
                    dt = dt.AddSeconds((int)dtTermS.Value);
                }
            }
            else
            {
                foreach (string item in lstBox.Items)
                {
                    var parts = item.Split(':');
                    int h = int.Parse(parts[0]);
                    int mm = int.Parse(parts[1]);
                    DateTime dt = new DateTime(y, m, d, h, mm, 0);
                    lstTargetDatetime.Add(dt);
                }
            }
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            SaveSetting();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadUSB();
            FindUsb();
        }

        private void btnOn_Click(object sender, EventArgs e)
        {
            _port?.Write(new byte[] { 0xA0, 0x01, 0x01, 0xA2 }, 0, 4);
        }

        private void btnOff_Click(object sender, EventArgs e)
        {
            _port?.Write(new byte[] { 0xA0, 0x01, 0x00, 0xA1 }, 0, 4);
        }

        private void dtStartH_ValueChanged(object sender, EventArgs e)
        {

        }


        private void runningTimer_Tick(object sender, EventArgs e)
        {
            if (runningSec == 0)
            {
                lbRunning.Text = "";
                btnOff_Click(null, null);
                runningTimer.Stop();
                runningTimer.Enabled = false;
            }
            else
            {
                lbRunning.Text = $" ( 분사 중... 남은 시간: {runningSec}초 )";
                runningSec--;
            }
        }

        private void RealClose()
        {
            _isRealClose = true;
            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!_isRealClose)
            {
                e.Cancel = true;
                Hide();
            }
            else
            {
                notifyIcon1.Dispose();
            }
            base.OnFormClosing(e);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            열기ToolStripMenuItem1_Click(null, null);
        }

        private void 열기ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            BringToFront();
        }

        private void 종료ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RealClose();
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


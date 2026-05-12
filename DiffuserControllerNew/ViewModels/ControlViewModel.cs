using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DiffuserControllerNew.Common;
using DiffuserControllerNew.Db;
using DiffuserControllerNew.Interface;
using DiffuserControllerNew.Message;
using DiffuserControllerNew.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace DiffuserControllerNew.ViewModels
{
    public partial class ControlViewModel : ObservableObject
    {
        private readonly IMessenger _messenger;
        [ObservableProperty] private ActionType _selectedActionType = ActionType.Interval;

        [ObservableProperty] private ObservableCollection<ComPortItem> _usbItems = new(); 
        [ObservableProperty] private ObservableCollection<string> _listItems = new();
        [ObservableProperty] private string _selectedListItem;
        [ObservableProperty] private bool _btnRunEnabled = false;
        [ObservableProperty] private bool _btnStopEnabled = true;
        [ObservableProperty] private ComPortItem _selectedComPort;
        [ObservableProperty] private ObservableCollection<string> _dtStartH;
        [ObservableProperty] private ObservableCollection<string> _dtStartM;
        [ObservableProperty] private ObservableCollection<string> _dtEndH;
        [ObservableProperty] private ObservableCollection<string> _dtEndM;
        [ObservableProperty] private ObservableCollection<string> _dtTermInterval;
        [ObservableProperty] private ObservableCollection<string> _dtTermH;
        [ObservableProperty] private ObservableCollection<string> _dtTermM;
        [ObservableProperty] private ObservableCollection<string> _dtTermS;
        //[ObservableProperty] private ObservableCollection<SystemEventDataModel> _systemEventCollection = new();
        //[ObservableProperty] private DateTime _startDate;
        [ObservableProperty] private DateTime _endDate;

        [ObservableProperty] private Visibility _visibleInterval = Visibility.Collapsed;
        [ObservableProperty] private Visibility _visibleSchedule = Visibility.Collapsed;
        
        [ObservableProperty] private int _usbSelectedIndex = -1;
        [ObservableProperty] private string _selectedDtStartH;
        [ObservableProperty] private string _selectedDtStartM;
        [ObservableProperty] private string _selectedDtEndH;
        [ObservableProperty] private string _selectedDtEndM;
        [ObservableProperty] private string _selectedDtTermInterval;
        [ObservableProperty] private string _selectedDtTermH;
        [ObservableProperty] private string _selectedDtTermM;
        [ObservableProperty] private string _selectedDtTermS;
        bool InitLoad = false;
        public ControlViewModel(IMessenger messenger)
        {
            _messenger = messenger;
            LoadUSB();
            InitTimeControl(); 
            _messenger.Register<ControlDataMessage>(this, (r, m) => ControlDataMessageHandler(m));
            _messenger.Register<ScheduleMessage>(this, (r, m) => ScheduleMessageHandler(m));
            
        }

        private void ScheduleMessageHandler(ScheduleMessage m)
        {
            if (m.Sender == Sender.btnSchedulePopupAddClose)
            {
                string time = m.Args as string;
                if (LocalDbManager.Instance.ControlModel.ScheduleTimes.Contains(time) == false)
                {
                    LocalDbManager.Instance.ControlModel.ScheduleTimes.Add(time);
                    LocalDbManager.Instance.ControlModel.ScheduleTimes = LocalDbManager.Instance.ControlModel.ScheduleTimes.OrderBy(x => x).ToList();
                    LocalDbManager.Instance.Save();
                }
                if (ListItems.Contains(time) == false)
                {
                    ListItems.Add(time);
                    var sorted = ListItems.OrderBy(x => x).ToList();
                    ListItems.Clear();
                    foreach (var item in sorted)
                        ListItems.Add(item);
                }
                SaveSettings();
            }
        }

        private void ControlDataMessageHandler(ControlDataMessage m)
        {
            if (m.Sender == Sender.None)
            {
                LoadSetting();
                SelectedDtStartH = LocalDbManager.Instance.ControlModel.StartAt.Hour.ToString("D2");
                SelectedDtStartM = LocalDbManager.Instance.ControlModel.StartAt.Minute.ToString("D2");
                SelectedDtEndH = LocalDbManager.Instance.ControlModel.EndAt.Hour.ToString("D2");
                SelectedDtEndM = LocalDbManager.Instance.ControlModel.EndAt.Minute.ToString("D2");
                SelectedDtTermInterval = LocalDbManager.Instance.ControlModel.MaintainSecond.ToString("D2");
                SelectedDtTermH = LocalDbManager.Instance.ControlModel.Term.Hour.ToString("D2");
                SelectedDtTermM = LocalDbManager.Instance.ControlModel.Term.Minute.ToString("D2");
                SelectedDtTermS = LocalDbManager.Instance.ControlModel.Term.Second.ToString("D2");
                ActionType at = ActionType.Interval;
                if (LocalDbManager.Instance.ControlModel.IsInterval)
                    at = ActionType.Interval;
                else if (LocalDbManager.Instance.ControlModel.IsSchedule)
                    at = ActionType.Schedule;

                OnSelectedActionTypeChanged(at);
            }
            InitLoad = true;
        }

        private void InitTimeControl()
        {
            DtStartH = new(Enumerable.Range(0, 24).Select(i => i.ToString("D2")));
            DtStartH = new(Enumerable.Range(0, 24).Select(i => i.ToString("D2")));
            DtEndH = new(Enumerable.Range(0, 24).Select(i => i.ToString("D2")));
            DtTermH = new(Enumerable.Range(0, 24).Select(i => i.ToString("D2")));

            DtStartM = new(Enumerable.Range(0, 59).Select(i => i.ToString("D2")));
            DtEndM = new(Enumerable.Range(0, 59).Select(i => i.ToString("D2")));
            DtTermM = new(Enumerable.Range(0, 59).Select(i => i.ToString("D2")));
            DtTermS = new(Enumerable.Range(0, 59).Select(i => i.ToString("D2")));

            DtTermInterval = new(Enumerable.Range(1, 10).Select(i => i.ToString("D2")));
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


            UsbItems.Clear();

            foreach (var ci in ports)
            {
                // 표시: "USB-SERIAL CH340 (COM3)"
                // 실제 사용: "COM3"
                UsbItems.Add(new ComPortItem
                {
                    ComPort = ci.ComPort,
                    Name = ci.Name,
                    Display = ci.Display
                });
            }
        }
        private void LoadSetting()
        {
            FindUsb();

            //DtStartH = LocalDbManager.Instance.ControlModel.StartAt.Hour;
            //DtStartM = LocalDbManager.Instance.ControlModel.StartAt.Minute;

            //DtEndH = LocalDbManager.Instance.ControlModel.EndAt.Hour;
            //DtEndM = LocalDbManager.Instance.ControlModel.EndAt.Minute;

            //DtTermH = DtTermM = DtTermS = 0;

            //int h = LocalDbManager.Instance.ControlModel.IntervalSecond / 3600;
            //int m = (LocalDbManager.Instance.ControlModel.IntervalSecond % 3600) / 60;
            //int s = (LocalDbManager.Instance.ControlModel.IntervalSecond % 3600) % 60;
            //DtTermH = h;
            //DtTermM = m;
            //DtTermS = s;
            //DtTermInterval = LocalDbManager.Instance.ControlModel.IntervalMaintainSecond;

            foreach (var itm in LocalDbManager.Instance.ControlModel.ScheduleTimes)
            {
                ListItems.Add(itm);
            }

            if (LocalDbManager.Instance.ControlModel.IsInterval)
                SelectedActionType = ActionType.Interval;
            else
                SelectedActionType = ActionType.Schedule;
        }

        private void FindUsb()
        {
            int idx = -1;
            bool find = false;
            foreach (ComPortItem itm in UsbItems)
            {
                if (itm.Display == LocalDbManager.Instance.ControlModel.SelectedUSB)
                {
                    find = true;
                }
                idx++;
                if (find)
                {
                    UsbSelectedIndex = idx;
                    break;
                }
            }
        }

        [RelayCommand]
        private async void OnSearch()
        {
            LoadUSB();
            FindUsb();
         
        }


        [RelayCommand]
        private async void OnRun()
        {
            LocalDbManager.Instance.ControlModel.IsRunning = true;
            LocalDbManager.Instance.Save();
            BtnRunEnabled = false;
            BtnStopEnabled = true;
            _messenger.Send(new ScheduleMessage { Sender = Sender.BtnRun});
        }

        [RelayCommand]
        private async void OnStop()
        {
            LocalDbManager.Instance.ControlModel.IsRunning = false;
            LocalDbManager.Instance.Save();
            BtnRunEnabled = true;
            BtnStopEnabled = false;
        }


        [RelayCommand]
        private async void OnOn()
        {
            _messenger.Send(new DiffuserMessage { Sender = Sender.btnDiffuserOn });
        }


        [RelayCommand]
        private async void OnOff()
        {
            _messenger.Send(new DiffuserMessage { Sender = Sender.btnDiffuserOff });
        }

        [RelayCommand]
        private async void OnAdd()
        {
            _messenger.Send(new ScheduleMessage { Sender = Sender.btnSchedulePopupOpen });
        }

        [RelayCommand]
        private async void OnDel()
        {
            if (string.IsNullOrEmpty(SelectedListItem) == false)
            {
                ListItems.Remove(SelectedListItem);
                SaveSettings();
            }
        }

     

        partial void OnSelectedActionTypeChanged(ActionType type)
        {
            VisibleInterval = VisibleSchedule = Visibility.Collapsed;
            if (type == ActionType.Interval)
                VisibleInterval = Visibility.Visible;
            else if(type == ActionType.Schedule)
                VisibleSchedule = Visibility.Visible;
            SaveSettings();
        }

        partial void OnUsbSelectedIndexChanged(int value)
        {
            string displayValue = "";
            if (UsbSelectedIndex >= 0)
                displayValue = UsbItems[UsbSelectedIndex].Display;
            SaveSettings();
        }
        
        internal void SaveSettings()
        {
            if (InitLoad == false) 
                return;

            LocalDbManager.Instance.ControlModel.IsInterval = SelectedActionType == ActionType.Interval;
            LocalDbManager.Instance.ControlModel.IsSchedule = SelectedActionType == ActionType.Schedule;
            LocalDbManager.Instance.ControlModel.StartAt = new DateTime(2000, 1, 1, int.Parse(SelectedDtStartH), int.Parse(SelectedDtStartM), 0);
            LocalDbManager.Instance.ControlModel.EndAt = new DateTime(2000, 1, 1, int.Parse(SelectedDtEndH), int.Parse(SelectedDtEndM), 0); 
            LocalDbManager.Instance.ControlModel.Term = new DateTime(2000, 1, 1, int.Parse(SelectedDtTermH), int.Parse(SelectedDtTermM), int.Parse(SelectedDtTermS));
            LocalDbManager.Instance.ControlModel.MaintainSecond = int.Parse(SelectedDtTermInterval);
            LocalDbManager.Instance.ControlModel.ScheduleTimes = ListItems.ToList();
            LocalDbManager.Instance.ControlModel.SelectedUSB = UsbSelectedIndex >= 0 ? UsbItems[UsbSelectedIndex].Display : "";
            if (UsbSelectedIndex >= 0)
            {
                ComPortItem cpi = new ComPortItem();
                cpi.Name = UsbItems[UsbSelectedIndex].Name;
                cpi.ComPort = UsbItems[UsbSelectedIndex].ComPort;
                cpi.Display = UsbItems[UsbSelectedIndex].Display;
                LocalDbManager.Instance.SelectedComPort = cpi;
            }
            LocalDbManager.Instance.Save();
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DiffuserControllerNew.Db;
using DiffuserControllerNew.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        [ObservableProperty] private ObservableCollection<ComPortItem> _usbItems = new(); 
        [ObservableProperty] private ObservableCollection<string> _listItems = new();
        [ObservableProperty] private string _selectedListItem;
        [ObservableProperty] private ComPortItem _selectedComPort;
        [ObservableProperty] private int _dtStartH;
        [ObservableProperty] private int _dtStartM;
        [ObservableProperty] private int _dtEndH;
        [ObservableProperty] private int _dtEndM;
        [ObservableProperty] private int _dtTermInterval;
        [ObservableProperty] private int _dtTermH;
        [ObservableProperty] private int _dtTermM;
        [ObservableProperty] private int _dtTermS;
        //[ObservableProperty] private ObservableCollection<SystemEventDataModel> _systemEventCollection = new();
        //[ObservableProperty] private DateTime _startDate;
        [ObservableProperty] private DateTime _endDate;
        [ObservableProperty] private bool _rbInterval;
        [ObservableProperty] private bool _rbSchedule;
        [ObservableProperty] private Visibility _visibleInterval = Visibility.Collapsed;
        [ObservableProperty] private Visibility _visibleSchedule = Visibility.Collapsed;
        [ObservableProperty] private int _usbSelectedIndex = -1;
        public ControlViewModel(IMessenger messenger)
        {
            _messenger = messenger;
            LoadUSB();
            LoadSetting(); 
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

            DtStartH = LocalDbManager.Instance.ControlModel.StartAt.Hour;
            DtStartM = LocalDbManager.Instance.ControlModel.StartAt.Minute;

            DtEndH = LocalDbManager.Instance.ControlModel.EndAt.Hour;
            DtEndM = LocalDbManager.Instance.ControlModel.EndAt.Minute;

            DtTermH = DtTermM = DtTermS = 0;

            int h = LocalDbManager.Instance.ControlModel.IntervalSecond / 3600;
            int m = (LocalDbManager.Instance.ControlModel.IntervalSecond % 3600) / 60;
            int s = (LocalDbManager.Instance.ControlModel.IntervalSecond % 3600) % 60;
            DtTermH = h;
            DtTermM = m;
            DtTermS = s;
            DtTermInterval = LocalDbManager.Instance.ControlModel.IntervalMaintainSecond;

            LocalDbManager.Instance.ControlModel.IsSchedule = !RbInterval;
            foreach (var itm in LocalDbManager.Instance.ControlModel.ScheduleTimes)
            {
                ListItems.Add(itm);
            }

            if (LocalDbManager.Instance.ControlModel.IsInterval)
                RbInterval = true;
            else
                RbSchedule = true;
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

        }

        partial void OnRbIntervalChanged(bool value)
        {
            if (value)
            {
                VisibleInterval = Visibility.Visible;
                VisibleSchedule = Visibility.Collapsed;
            }
        }

        partial void OnRbScheduleChanged(bool value)
        {
            if (value)
            {
                VisibleInterval = Visibility.Collapsed;
                VisibleSchedule = Visibility.Visible;
            }
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DiffuserControllerNew.Db;
using DiffuserControllerNew.Factory;
using DiffuserControllerNew.Interface;
using DiffuserControllerNew.Message;
using DiffuserControllerNew.Models;
using DiffuserControllerNew.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Shapes;
using System.Windows.Threading;
using Wpf.Ui;

namespace DiffuserControllerNew.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty] private bool _isModalOpen;
        [ObservableProperty] private UIElement? _modalContent;
        [ObservableProperty] private Visibility _statusbarVisible = Visibility.Collapsed;
        [ObservableProperty] private string _visibleMenu = "Hidden";
        [ObservableProperty] private string _statusbarUserTimeInfo = "";
        [ObservableProperty] private string _statusbarSessionTime = "";
        [ObservableProperty] private string _lbLeft = "";
        [ObservableProperty] private string _lbTime = "";
        [ObservableProperty] private string _lbRunning = "";
        DateTime dtToday = DateTime.Now;
        List<DateTime> lstTargetDatetime = new List<DateTime>();
        int runningSec = 0;
        [ObservableProperty] private UIElement? _currentContent;

        [ObservableProperty] private string _userName;
        [ObservableProperty] private string _ContentHorizontal;
        [ObservableProperty] private string _ContentVertical;

        private readonly IServiceProvider _provider;
        private readonly INavigationService _navService;
        private readonly IMessenger _messenger;
        private readonly IIgnoreDateAddContinuePopupViewFactory _ignoreDateAddContinuePopupViewFactory;
        private SerialPort? _port;

        private DispatcherTimer _timer;
        private DispatcherTimer _runningTimer;
        private DateTime sessionTimeLeft;
        private readonly DateTime sessionDefDate = new DateTime(2000, 1, 1, 0, 0, 0);


        public MainViewModel(MainViewModelConfiguration mainConf, IServiceProvider provider, INavigationService navService, IIgnoreDateAddContinuePopupViewFactory ignoreDateAddContinuePopupViewFactory)
        {
            _provider = provider;
            _navService = navService;
            _messenger = mainConf.Messenger;
            _ignoreDateAddContinuePopupViewFactory = ignoreDateAddContinuePopupViewFactory;
            SettingTargetDatetime();
            InitializeMessaging();
            LocalDbManager.Instance.ControlModel.IsRunning = true;
            LocalDbManager.Instance.Save();
        }

        private void LoadData()
        {
            _messenger.Send(new ControlDataMessage { Sender = Sender.None, Args = null });
            _messenger.Send(new IgnoreDateMessage { Sender = Sender.None, Args = null });
        }

        private void TimerInit()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += OnTimerTick;
            _timer.Start();

            _runningTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _runningTimer.Tick += _runningTimer_Tick;
        }


        private void OnTimerTick(object sender, EventArgs e)
        {
            LbTime = $"{DateTime.Now.ToString("yy-MM-dd (ddd) HH:mm:ss")}";
            if (dtToday.Day != DateTime.Now.Day)
            {
                SettingTargetDatetime();
                dtToday = DateTime.Now;
            }

            DateModel dm = LocalDbManager.Instance.Dates.FirstOrDefault(x => x.Date == DateOnly.FromDateTime(DateTime.Now));
            if (dm == null)
            {//동작해야 하는 날
                if (LocalDbManager.Instance.ControlModel.IsRunning)
                {
                    DateTime? target = lstTargetDatetime.Where(x => x >= DateTime.Now).FirstOrDefault();
                    if (target != DateTime.MinValue)
                    {
                        TimeSpan ts = target.Value - DateTime.Now;
                        if (ts.Hours > 0)
                            LbLeft = $"{ts.Hours}시간 {ts.Minutes}분 {ts.Seconds}초 뒤 {LocalDbManager.Instance.ControlModel.MaintainSecond}초간 분사 예정..";
                        else if (ts.Minutes > 0)
                            LbLeft = $"{ts.Minutes}분 {ts.Seconds}초 뒤 {LocalDbManager.Instance.ControlModel.MaintainSecond}초간 분사 예정..";
                        else if (ts.Seconds > 0)
                            LbLeft = $"{ts.Seconds}초 뒤 {LocalDbManager.Instance.ControlModel.MaintainSecond}초간 분사 예정..";
                        else
                        {
                            runningSec = LocalDbManager.Instance.ControlModel.MaintainSecond;
                            _runningTimer.IsEnabled = true;
                            _runningTimer.Start();
                            DiffuserExecute(true);
                        }
                    }
                    else
                    {
                        LbLeft = $"오늘 동작 일정 종료";
                    }
                }
                else
                {
                    LbLeft = $"중지됨";
                }
            }
            else
            {
                LbLeft = $"오늘은 동작 제외 날짜 입니다. ( {dm.Message} )";
            }
        }

        private void DiffuserExecute(bool v)
        {
            if (v)
            {
                if (_port != null && _port.IsOpen)
                    _port?.Write(new byte[] { 0xA0, 0x01, 0x01, 0xA2 }, 0, 4);
                else
                {
                    _port = new SerialPort(LocalDbManager.Instance.SelectedComPort.ComPort, 9600);
                    _port.Open();
                    _port?.Write(new byte[] { 0xA0, 0x01, 0x01, 0xA2 }, 0, 4);
                }
            }
            else
            {
                _port?.Write(new byte[] { 0xA0, 0x01, 0x00, 0xA1 }, 0, 4);
            }
        }

        private void _runningTimer_Tick(object? sender, EventArgs e)
        {
            if (runningSec == 0)
            {
                LbRunning = "";
                _runningTimer.Stop();
                _runningTimer.IsEnabled = false;
                DiffuserExecute(false);
            }
            else
            {
                LbRunning = $" ( 분사 중... 남은 시간: {runningSec}초 )";
                runningSec--;
            }
        }

        private void SettingTargetDatetime()
        {
            lstTargetDatetime = new List<DateTime>();
            int y = DateTime.Now.Year;
            int m = DateTime.Now.Month;
            int d = DateTime.Now.Day;
            if (LocalDbManager.Instance.ControlModel.IsInterval)
            {
                
                DateTime dt = new DateTime(y, m, d, LocalDbManager.Instance.ControlModel.StartAt.Hour, LocalDbManager.Instance.ControlModel.StartAt.Minute, 0);
                DateTime edt = new DateTime(y, m, d, LocalDbManager.Instance.ControlModel.EndAt.Hour, LocalDbManager.Instance.ControlModel.EndAt.Minute, 0);

                while (true)
                {
                    if (dt > edt)
                        break;
                    lstTargetDatetime.Add(dt);
                    dt = dt.AddHours(LocalDbManager.Instance.ControlModel.Term.Hour);
                    dt = dt.AddMinutes(LocalDbManager.Instance.ControlModel.Term.Minute);
                    dt = dt.AddSeconds(LocalDbManager.Instance.ControlModel.Term.Second);
                }
            }
            else
            {
                foreach (string item in LocalDbManager.Instance.ControlModel.ScheduleTimes)
                {
                    var parts = item.Split(':');
                    int h = int.Parse(parts[0]);
                    int mm = int.Parse(parts[1]);
                    DateTime dt = new DateTime(y, m, d, h, mm, 0);
                    lstTargetDatetime.Add(dt);
                }
            }
        }

        private void InitializeMessaging()
        {
            _messenger.Register<ScheduleMessage>(this, (r, m) => ServerSettingHandler(m));
            _messenger.Register<DiffuserMessage>(this, (r, m) => DiffuserMessageHandler(m));
            
            _messenger.RegisterAll(this);
        }

        private void DiffuserMessageHandler(DiffuserMessage m)
        {
            if (m.Sender == Sender.btnDiffuserOn)
                DiffuserExecute(true);
            else if(m.Sender == Sender.btnDiffuserOff)
                DiffuserExecute(false);
        }

        private void ServerSettingHandler(ScheduleMessage m)
        {
            if (m.Sender == Sender.BtnClose || m.Sender == Sender.btnSchedulePopupClose)
                CloseModal();
            else if(m.Sender == Sender.btnSchedulePopupAddClose)
            {
                CloseModal();
                SettingTargetDatetime();
            }   
            else if (m.Sender == Sender.btnSchedulePopupOpen)
            {
                var view = _provider.GetRequiredService<ScheduleAddPopupView>();
                ModalContent = view;
                IsModalOpen = true;
            }
            else if (m.Sender == Sender.btnScheduleDelete)
            {
                MessageBox.Show("리스트에서 선택된 항목 삭제");
            }
            else if(m.Sender == Sender.btnSchedulePopupAddContinue)
            {
                ScheduleMessageData d = m.Args as ScheduleMessageData;
                var view = _ignoreDateAddContinuePopupViewFactory.IgnoreDateAddContinuePopupView(d);
                ModalContent = view;
                IsModalOpen = true;
            }
            else if(m.Sender == Sender.btnSchedulePopupAddContinueApply)
            {
                CloseModal();
            }
            else if(m.Sender == Sender.BtnRun)
            {
                SettingTargetDatetime();
            }
        }
         

        internal void InitializeAsync()
        {
            LoadData();
            TimerInit();
        }

        [RelayCommand]
        private void CloseModal()
        {
            IsModalOpen = false;
            ModalContent = null;
        }

        [RelayCommand]
        private void OnSignout()
        {

        }


    }
}

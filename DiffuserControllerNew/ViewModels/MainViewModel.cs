using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DiffuserControllerNew.Interface;
using DiffuserControllerNew.Message;
using DiffuserControllerNew.Models;
using System;
using System.Collections.Generic;
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

        [ObservableProperty] private UIElement? _currentContent;

        [ObservableProperty] private string _userName;
        [ObservableProperty] private string _ContentHorizontal;
        [ObservableProperty] private string _ContentVertical;

        private readonly IServiceProvider _provider;
        private readonly INavigationService _navService;
        private readonly IMessenger _messenger; 

        private DispatcherTimer _timer;
        private DateTime sessionTimeLeft;
        private readonly DateTime sessionDefDate = new DateTime(2000, 1, 1, 0, 0, 0);


        public MainViewModel(MainViewModelConfiguration mainConf, IServiceProvider provider, INavigationService navService)
        {
            _provider = provider;
            _navService = navService;
            _messenger = mainConf.Messenger;
            InitializeMessaging();
            TimerInit();
        }




        private void TimerInit()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += OnTimerTick;
            _timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            
        }

        private void InitializeMessaging()
        {
            _messenger.Register<ScheduleMessage>(this, (r, m) => ServerSettingHandler(m));
            _messenger.RegisterAll(this);
        }


        private void ServerSettingHandler(ScheduleMessage m)
        {
            if (m.Sender == Sender.BtnClose)
                CloseModal();
        }
         

        internal void InitializeAsync()
        {
            
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

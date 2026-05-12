using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DiffuserControllerNew.Interface;
using DiffuserControllerNew.Message;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiffuserControllerNew.ViewModels
{ 
    public partial class ScheduleAddPopupViewModel : ObservableObject
    {
        private readonly IMessenger _messenger;
        [ObservableProperty] private int _usbSelectedIndex = -1;
        [ObservableProperty] private ObservableCollection<string> _dtStartH;
        [ObservableProperty] private ObservableCollection<string> _dtStartM;
        [ObservableProperty] private string _selectedDtStartH;
        [ObservableProperty] private string _selectedDtStartM;
        public ScheduleAddPopupViewModel(IMessenger messenger)
        {
            _messenger = messenger;
            DtStartH = new(Enumerable.Range(0, 24).Select(i => i.ToString("D2")));
            DtStartM = new(Enumerable.Range(0, 24).Select(i => i.ToString("D2")));
            SelectedDtStartH = "00";
            SelectedDtStartM = "00";
        }

        [RelayCommand]
        private void OnPopupHeaderClose()
        {
            _messenger.Send(new ScheduleMessage { Sender = Sender.btnSchedulePopupClose });
        }


        [RelayCommand]
        private async void OnAdd()
        {
            string time = $"{SelectedDtStartH}:{SelectedDtStartM}";
            _messenger.Send(new ScheduleMessage { Sender = Sender.btnSchedulePopupAddClose, Args = time });
        }

        [RelayCommand]
        private async void OnAddContinue()
        {
            MessageBox.Show("추가 후 초기화 로직 구현 필요");
        }
    }
}

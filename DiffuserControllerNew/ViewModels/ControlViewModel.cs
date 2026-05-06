using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffuserControllerNew.ViewModels
{
    public partial class ControlViewModel : ObservableObject
    {
        private readonly IMessenger _messenger;

        //[ObservableProperty] private ObservableCollection<AccountAcivityDataModel> _accountAcrivityCollection = new();
        //[ObservableProperty] private ObservableCollection<SystemEventDataModel> _systemEventCollection = new();
        //[ObservableProperty] private DateTime _startDate;
        [ObservableProperty] private DateTime _endDate;

        public ControlViewModel(IMessenger messenger)
        {
            _messenger = messenger;
        }

        [RelayCommand]
        private async void OnSearch()
        {
        }
    }
}

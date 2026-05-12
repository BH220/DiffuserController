using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DiffuserControllerNew.Common;
using DiffuserControllerNew.Interface;
using DiffuserControllerNew.Message;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace DiffuserControllerNew.ViewModels
{
    public partial class IgnoreDateAddContinuePopupViewModel : ObservableObject
    {
        private readonly IMessenger _messenger;
        [ObservableProperty] private string _selectedListItem; 
        [ObservableProperty] private string _startDateAt;
        [ObservableProperty] private string _rangeVal;
        [ObservableProperty] private DateTime _selectedEndDt;
        private DateTime startAt = DateTime.MinValue;
        private ScheduleMessageData _scheduleMessageData;


        public IgnoreDateAddContinuePopupViewModel(IMessenger messenger)
        {
            _messenger = messenger;

        }

        public void SetInitData(ScheduleMessageData scheduleMessageData)
        {
            _scheduleMessageData = scheduleMessageData;
            startAt = scheduleMessageData.Date;
            StartDateAt = startAt.ToString("yyyy-MM-dd");

        }

        partial void OnSelectedEndDtChanged(DateTime value)
        {
            _scheduleMessageData.EndDate = value;
            RangeVal = $"{(value.Date - startAt.Date).Days+1} 일";
        }

        [RelayCommand]
        private async void OnPopupHeaderClose()
        {
            _messenger.Send(new ScheduleMessage { Sender = Sender.btnSchedulePopupClose });
        }

        [RelayCommand]
        private async void OnPopupApply()
        {
            _messenger.Send(new ScheduleMessage { Sender = Sender.btnSchedulePopupAddContinueApply, Args = _scheduleMessageData });
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DiffuserControllerNew.Api;
using DiffuserControllerNew.Converter;
using DiffuserControllerNew.Db;
using DiffuserControllerNew.Interface;
using DiffuserControllerNew.Message;
using DiffuserControllerNew.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DiffuserControllerNew.ViewModels
{ 
    public partial class IgnoreDateViewModel : ObservableObject
    {
        string ColorRed = "#FCA5A5";
        string ColorBlue = "#93C5FD";
        private readonly IMessenger _messenger; 
        [ObservableProperty] private DateDataRow _selectedItem;
        [ObservableProperty] private int _selectedRowsCount = 0;
        [ObservableProperty] private ObservableCollection<DateDataRow> _dateCollection = new();
        [ObservableProperty] private DateTime _endDate;
        [ObservableProperty] private BitmapImage _headerCheckImage = new BitmapImage(new Uri("pack://application:,,,/Resources/unchecked.png"));
        [ObservableProperty] private int _targetYear;
        [ObservableProperty] private DateTime? _selectedDate;
        [ObservableProperty] private string _txtSelectedDate;
        [ObservableProperty] private string _txtContent; 

        public IgnoreDateViewModel(IMessenger messenger)
        {
            _messenger = messenger;
            TargetYear = DateTime.Now.Year;
            _messenger.Register<IgnoreDateMessage>(this, (r, m) => IgnoreDateMessageHandler(m));
            _messenger.Register<ScheduleMessage>(this, (r, m) => ScheduleMessageHandler(m));
            SelectedDate = DateTime.Now;

        }

        private void ScheduleMessageHandler(ScheduleMessage m)
        {
            if(m.Sender == Sender.btnSchedulePopupAddContinueApply)
            {
                ScheduleMessageData x= m.Args as ScheduleMessageData;
                if (x != null)
                {
                    while(x.Date.Date <= x.EndDate.Date)
                    {
                        var fd = LocalDbManager.Instance.Dates.FirstOrDefault(z => z.Date == DateOnly.FromDateTime(x.Date));
                        if(fd == null)
                        {
                            fd = new DateModel();
                            fd.Date = DateOnly.FromDateTime(x.Date);
                            fd.Message = x.Message;
                            fd.DateType = Common.DateTypes.SpecifiedDate;
                            LocalDbManager.Instance.Dates.Add(fd);
                            LocalDbManager.Instance.Save();
                        }
                        x.Date = x.Date.AddDays(1);
                    }
                }
            }
        }

        private void IgnoreDateMessageHandler(IgnoreDateMessage m)
        {
            if (m.Sender == Interface.Sender.None)
            {
                OnLoad();
            } 
        }

        private void OnLoad(DateTime? selectDt = null)
        {
            DateCollection.Clear();
            LocalDbManager.Instance.Refresh();
            DateOnly dto = DateOnly.MinValue;
            if (selectDt.HasValue)
                dto = DateOnly.FromDateTime(selectDt.Value);

            DateDataRow selectRow = null;
            foreach (DateModel x in LocalDbManager.Instance.Dates)
            {
                DateCollection.Add(new DateDataRow(x));
                if (x.Date == dto)
                    selectRow = DateCollection.Last();
            }
            if (selectRow != null)
                ScrollToItemAction?.Invoke(selectRow);
            else
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                var nearest = DateCollection
                    .Where(x => x.Data.Date <= today)
                    .OrderByDescending(x => x.Data.Date)
                    .FirstOrDefault();

                if (nearest != null)
                    ScrollToItemAction?.Invoke(nearest);
            }
        }
        public Action<object> ScrollToItemAction { get; set; }

        // SelectedItem 변경 시 호출
        partial void OnSelectedItemChanged(DateDataRow value)
        {
            ScrollToItemAction?.Invoke(value);
        }

        BitmapImage imgCheck = new BitmapImage(new Uri("pack://application:,,,/Resources/checked.png"));
        BitmapImage imgUnCheck = new BitmapImage(new Uri("pack://application:,,,/Resources/unchecked.png"));
        public void UpdateSelectRow()
        {
            SelectedRowsCount = DateCollection.Count(x => x.IsSelected);
            if (DateCollection.Count() == SelectedRowsCount)
                HeaderCheckImage = imgCheck;
            else
                HeaderCheckImage = imgUnCheck; 
        }

        partial void OnSelectedDateChanged(DateTime? value)
        {
            if (value == null)
            {
                TxtSelectedDate = "";
                TxtContent = "";
                return;
            }
            LoadDataValue(value.Value);
        }

        private void LoadDataValue(DateTime value)
        {
            TxtSelectedDate = value.ToString("yyyy-MM-dd");
            DateModel dt = LocalDbManager.Instance.Dates.FirstOrDefault(x => x.Date == DateOnly.FromDateTime(value));
            if (dt != null)
                TxtContent = dt.Message;
            else
                TxtContent = "";
        }

        [RelayCommand]
        private async void OnSelectedDel()
        { 
            var temp = DateCollection.Where(x => x.IsSelected);
            if (temp.Count() <= 0)
            {
                MessageBox.Show("선택된 항목이 없습니다.","", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else 
            {
                if (MessageBox.Show("선택된 일정을 모두 삭제 하시겠습니가?", "", MessageBoxButton.YesNo, MessageBoxImage.Question)
                    == MessageBoxResult.Yes)
                {
                    List<DateOnly> lst = new List<DateOnly>();
                    foreach (var item in temp)
                    {
                        var fi = LocalDbManager.Instance.Dates.FirstOrDefault(x => x.Date == item.Data.Date);
                        if (fi != null)
                            LocalDbManager.Instance.Dates.Remove(fi);
                    }
                    LocalDbManager.Instance.Save();
                    RefreshGridData();
                    TxtSelectedDate = "";
                    TxtContent = "";
                }
            }


        }

        private void RefreshGridData()
        {
            IgnoreDateMessageHandler(new IgnoreDateMessage() { Sender = Sender.None, Args = null });
        }

        [RelayCommand]
        private async void OnSundayLoad()
        {
            LoadSunSatDay(true, TargetYear);
            IgnoreDateMessageHandler(new IgnoreDateMessage() { Sender = Interface.Sender.None });
        }

        [RelayCommand]
        private async void OnSaturdayLoad()
        {
            LoadSunSatDay(false, TargetYear);
            IgnoreDateMessageHandler(new IgnoreDateMessage() { Sender = Interface.Sender.None });
        }

        private void LoadSunSatDay(bool isSunday, int year)
        {
            DateTime dt = new DateTime(year, 1, 1);
            while (true)
            {
                if (isSunday)
                {
                    if (dt.DayOfWeek == DayOfWeek.Sunday)
                    {
                        break;
                    }
                }
                else
                {
                    if (dt.DayOfWeek == DayOfWeek.Saturday)
                    {
                        break;
                    }
                }
                dt = dt.AddDays(1);
            }
            while (true)
            {
                if (dt.Year > year)
                {
                    break;
                }
                var dd = LocalDbManager.Instance.Dates.FirstOrDefault(x => x.Date == DateOnly.FromDateTime(dt));
                if (dd == null)
                { 
                    DateModel dm = new DateModel();
                    dm.Date = DateOnly.FromDateTime(dt);
                    dm.Message = $"쉬는날 - {(isSunday ? "일요일" : "토요일")}";
                    
                    dm.DateType = isSunday ? Common.DateTypes.Sunday : Common.DateTypes.Saturday;
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

                dt = dt.AddDays(7);
            }
            LocalDbManager.Instance.Save();
            RefreshGridData();
        }

        [RelayCommand]
        private async void OnHolidaydayLoad()
        {
            var holidays = await HolidayApi.GetHolidaysAsync(TargetYear);

            foreach (var h in holidays)
            {
                DateModel dm = new DateModel();
                dm.Date = h.Date;
                dm.Message = h.DateName;
                var find = LocalDbManager.Instance.Dates.FirstOrDefault(x => x.Date == dm.Date);
                if (find != null)
                {
                    find.Message = dm.Message;
                    find.DateType = Common.DateTypes.Holiday;
                    
                }
                else
                {
                    dm.DateType = Common.DateTypes.Holiday;
                    LocalDbManager.Instance.Dates.Add(dm);
                }
                LocalDbManager.Instance.Save();
            }
            RefreshGridData();
        }


        [RelayCommand]
        private async void OnReLoad()
        {
            OnLoad();
        }



        [RelayCommand]
        private async void OnIgnoreDateAdd()
        {
            if (SelectedDate.HasValue && string.IsNullOrEmpty(TxtContent) == false)
            {
                DateModel dm = new DateModel();
                dm.Date = DateOnly.FromDateTime(SelectedDate.Value);
                dm.Message = TxtContent;
                if (SelectedDate.Value.DayOfWeek == DayOfWeek.Sunday)
                    dm.DateType = Common.DateTypes.Sunday;
                else if (SelectedDate.Value.DayOfWeek == DayOfWeek.Saturday)
                    dm.DateType = Common.DateTypes.Saturday;
                else
                    dm.DateType = Common.DateTypes.Weekday;
                var find = LocalDbManager.Instance.Dates.FirstOrDefault(x => x.Date == dm.Date);
                if (find != null)
                {
                    find.Message = dm.Message;
                }
                else
                {
                    LocalDbManager.Instance.Dates.Add(dm);
                    LocalDbManager.Instance.Save();
                    OnLoad(SelectedDate.Value);
                    SelectedItem = DateCollection.FirstOrDefault(x => x.Data.Date == DateOnly.FromDateTime(SelectedDate.Value));
                    MessageBox.Show("반영 하였습니다.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("제외 날짜에 맞는 사유를 입력하세요", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        [RelayCommand]
        private async void OnIgnoreDateDel()
        {
            if (SelectedDate.HasValue && string.IsNullOrEmpty(TxtContent) == false)
            {
                if (MessageBox.Show("삭제하시겠습니까?", "", MessageBoxButton.YesNo, MessageBoxImage.Question)
                    == MessageBoxResult.Yes)
                {
                    var dt = LocalDbManager.Instance.Dates.FirstOrDefault(x => x.Date == DateOnly.FromDateTime(SelectedDate.Value));
                    if (dt != null)
                    {
                        LocalDbManager.Instance.Dates.Remove(dt);
                        LocalDbManager.Instance.Save();
                        MessageBox.Show("삭제하였습니다");
                        TxtContent = "";
                        OnLoad();
                    }
                }
            }
            else
            {
                MessageBox.Show("삭제할 데이터가 없습니다.", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        [RelayCommand]
        private async void OnIgnoreDateAddContinue()
        {
            if (SelectedDate.HasValue && string.IsNullOrEmpty(TxtContent) == false)
            {
                ScheduleMessageData data = new ScheduleMessageData();
                data.Date = SelectedDate.Value;
                data.Message = TxtContent;
                _messenger.Send(new ScheduleMessage { Sender = Sender.btnSchedulePopupAddContinue, Args = data });
            }
            else
            {
                MessageBox.Show("제외 날짜에 맞는 사유를 입력하세요", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // ViewModel
        public Action<DateTime> MoveCalendarAction { get; set; }

        internal void OnRowDoubleClick(DateDataRow row)
        {
            SelectedDate = row.Data.Date.ToDateTime(TimeOnly.MinValue);
            MoveCalendarAction?.Invoke(SelectedDate.Value);
            TxtSelectedDate = row.Data.Date.ToString("yyyy-MM-dd");
            TxtContent = row.Data.Message;
        }



    }
}

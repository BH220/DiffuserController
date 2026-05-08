using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DiffuserControllerNew.Db;
using DiffuserControllerNew.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DiffuserControllerNew.ViewModels
{ 
    public partial class IgnoreDateViewModel : ObservableObject
    {
        private readonly IMessenger _messenger;
        [ObservableProperty] private int _selectedRowsCount = 0;
        [ObservableProperty] private ObservableCollection<DateDataRow> _dateCollection = new();
        [ObservableProperty] private DateTime _endDate;
        [ObservableProperty] private BitmapImage _headerCheckImage = new BitmapImage(new Uri("pack://application:,,,/Resources/unchecked.png"));

        public IgnoreDateViewModel(IMessenger messenger)
        {
            _messenger = messenger;
            InitLoadData();
        }

        private void InitLoadData()
        {
            //dateModelBindingSource.DataSource = LocalDbManager.Instance.Dates;
            //grid.Refresh();
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

        [RelayCommand]
        private async void OnSearch()
        {
        }
    }
}

using DiffuserControllerNew.Models;
using DiffuserControllerNew.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DiffuserControllerNew.Views
{
    /// <summary>
    /// IgnoreDateView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class IgnoreDateView : UserControl
    { 
        public IgnoreDateView(IgnoreDateViewModel ignoreDateViewModel)
        {
            InitializeComponent();
            this.DataContext = ignoreDateViewModel;
            ignoreDateViewModel.ScrollToItemAction = (item) => ScrollToSelectedItem(item);
        }

        private void ChkAll_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image img && DataContext is IgnoreDateViewModel vm)
            {
                bool allSelected = vm.DateCollection.All(u => u.IsSelected);

                foreach (var item in vm.DateCollection)
                    item.IsSelected = !allSelected; 
                vm.UpdateSelectRow();
            }
        }

        private void ChkRow_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image img && img.DataContext is DateDataRow row)
            {
                row.IsSelected = !row.IsSelected;
                if (DataContext is IgnoreDateViewModel vm)
                    vm.UpdateSelectRow();
            }
        }

        public void ScrollToSelectedItem(object item)
        {
            if (item == null) return;
            Dispatcher.BeginInvoke(new Action(() =>
            {
                // 먼저 마지막 아이템으로 스크롤해서 선택 항목이 위로 오게 함
                var lastItem = grid.Items[grid.Items.Count - 1];
                grid.ScrollIntoView(lastItem);
                grid.ScrollIntoView(item);
            }), System.Windows.Threading.DispatcherPriority.ApplicationIdle);
        }

        private void Grid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is not IgnoreDateViewModel vm) return;
            var hit = e.OriginalSource as FrameworkElement;
            if (hit?.DataContext is DateDataRow row)
                vm.OnRowDoubleClick(row);
        }

        public void MoveCalendarToDate(DateTime date)
        {
            //calen.DisplayDate = date;
        }
    }
}

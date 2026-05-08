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
        }

        private void ChkAll_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image img && DataContext is IgnoreDateViewModel vm)
            {
                bool allSelected = vm.DateCollection.All(u => u.IsSelected);

                foreach (var item in vm.DateCollection)
                    item.IsSelected = !allSelected;

                //// allSelected 반전 후 이미지 교체
                //img.Source = new BitmapImage(new Uri(
                //    !allSelected  // ← 반전된 값으로 체크
                //        ? "pack://application:,,,/Resources/checked.png"
                //        : "pack://application:,,,/Resources/unchecked.png",
                //    UriKind.Absolute));
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
    }
}

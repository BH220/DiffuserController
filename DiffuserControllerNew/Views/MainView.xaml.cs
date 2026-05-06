using DiffuserControllerNew.ViewModels;
using Microsoft.Extensions.DependencyInjection;
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
using System.Windows.Shapes;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace DiffuserControllerNew.Views
{
    /// <summary>
    /// MainView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainView : Window
    {
        private readonly IServiceProvider _provider;
        public MainView(IServiceProvider provider, MainViewModel viewModel)
        {
            InitializeComponent();
            _provider = provider;
            DataContext = viewModel;
            viewModel.VisibleMenu = "Hidden";
            this.Loaded += MainView_Loaded;
            //this.menuBar.MenuSelected += (menuName) =>
            //{
            //    viewModel.MenuOpen(menuName);
            //};
            //this.menuBar.InformatinoClicked += () =>
            //{
            //    viewModel.OpenInformation();
            //};
        }

        private void MainView_Loaded(object sender, RoutedEventArgs e)
        {
            var snackbar = _provider.GetRequiredService<ISnackbarService>();
            snackbar.SetSnackbarPresenter(SnackbarPresenter);

            if (DataContext is not MainViewModel vm) return;

            vm.InitializeAsync();
            //Dispatcher.BeginInvoke(async () =>
            //{
            //    await vm.InitializeAsync();
            //}, DispatcherPriority.Loaded);
        }
    }
}

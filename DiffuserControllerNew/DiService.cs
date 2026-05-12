using CommunityToolkit.Mvvm.Messaging;
using DiffuserControllerNew.Factory;
using DiffuserControllerNew.Interface;
using DiffuserControllerNew.ViewModels;
using DiffuserControllerNew.Views;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui;
using Wpf.Ui.Abstractions;
using Wpf.Ui.DependencyInjection;

namespace DiffuserControllerNew
{
    public static class DiService
    {
        public static ServiceProvider ServicesRegister()
        {
            var services = new ServiceCollection();

            // ── WPF-UI 핵심 서비스 ──
            services.AddNavigationViewPageProvider();  // ← 이 한 줄로 INavigationViewPageProvider 등록
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<ISnackbarService, SnackbarService>();

            // ── 메신저 ──
            services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

            // ── Configurations ──
            services.AddSingleton<MainViewModelConfiguration>();

            // Factories
            services.AddSingleton<IIgnoreDateAddContinuePopupViewFactory, IgnoreDateAddContinuePopupViewFactory>();
            //services.AddSingleton<ILoginDuplicateViewFactory, LoginDuplicateViewFactory>();
            //services.AddSingleton<ILogDetailViewFactory, LogDetailViewFactory>();

            // ── ViewModels ──
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<IgnoreDateViewModel>();
            services.AddSingleton<ControlViewModel>();
            services.AddTransient<ScheduleAddPopupViewModel>();
            services.AddTransient<IgnoreDateAddContinuePopupViewModel>();

            // ── Views ──
            services.AddSingleton<MainView>();
            services.AddSingleton<IgnoreDateView>();
            services.AddSingleton<ControlView>();
            services.AddTransient<ScheduleAddPopupView>();
            services.AddTransient<IgnoreDateAddContinuePopupView>();


            return services.BuildServiceProvider();
        }
    }
}

using Prism.Ioc;
using CtcontrolMobile.ViewModels;
using CtcontrolMobile.Views;
using Xamarin.Forms;
using CtcontrolMobile.Dialog;

namespace CtcontrolMobile
{
    public partial class App
    {
        public App()
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            var result = await NavigationService.NavigateAsync("MainPage/NavigationPage/WelcomePage");

            if (!result.Success)
            {
                System.Diagnostics.Debugger.Break();
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<WelcomePage, WelcomePageViewModel>();
            containerRegistry.RegisterForNavigation<ConnectionMethodIVew, ConnectionMethodIVewViewModel>();
            containerRegistry.RegisterForNavigation<MainDataView, MainDataViewModel>();
            containerRegistry.RegisterForNavigation<PreView, PreViewViewModel>();
            containerRegistry.RegisterForNavigation<DataView, DataViewViewModel>();
            containerRegistry.RegisterForNavigation<CodeView, CodeViewViewModel>();
            containerRegistry.RegisterForNavigation<QRView, QRViewModel>();
            containerRegistry.RegisterForNavigation<HistoryView, HistoryViewModel>();
            containerRegistry.RegisterForNavigation<LaptopHistoryView, LaptopHistoryViewModel>();
            containerRegistry.RegisterForNavigation<DesctopHistoryView, DesctopHistoryViewModel>();
            containerRegistry.RegisterForNavigation<SettingView, SettingViewModel>();
            containerRegistry.RegisterDialog<ExitDialogView, ExitDialogViewModel>();
        }
    }
}

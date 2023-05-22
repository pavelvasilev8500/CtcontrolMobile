using CtcontrolMobile.Dictionary.Language;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace CtcontrolMobile.ViewModels
{
    public class ConnectionMethodIVewViewModel : BindableBase
    {
        private INavigationService _navigationService { get; }

        private string _title = AppResources.Connection;
        private string _mainText = AppResources.Please__connect_first;
        private string _conectionText = AppResources.Choose_connection_method;
        private string _qr = AppResources.Qr_code;
        private string _id = AppResources.Id_code;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string MainText
        {
            get { return _mainText; }
            set { SetProperty(ref _mainText, value); }
        }
        public string ConnectionText
        {
            get { return _conectionText; }
            set { SetProperty(ref _conectionText, value); }
        }
        public string Qr
        {
            get { return _qr; }
            set { SetProperty(ref _qr, value); }
        }
        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public DelegateCommand<string> NavigateCommand { get; set; }

        public ConnectionMethodIVewViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand<string>(NavigateCommandExecuted);
        }

        private async void NavigateCommandExecuted(string view)
        {
            var result = await _navigationService.NavigateAsync($"{view}");
            if (!result.Success)
            {
                System.Diagnostics.Debugger.Break();
            }
        }
    }
}

using CtcontrolMobile.Dictionary.Language;
using CtcontrolMobile.EventAgregator;
using CtcontrolMobile.MasterPage;
using CtcontrolMobile.Service;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CtcontrolMobile.ViewModels
{
    class QRViewModel : BindableBase
    {
        private INavigationService _navigationService { get; }
        IEventAggregator _eventAggregator;

        private string _title = AppResources.Connection_by_qr;
        private string _connect = AppResources.Connect;
        private Color _textColor = Color.Gray;
        private string _data;
        private bool _canExecute = false;


        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public string Connect
        {
            get { return _connect; }
            set { SetProperty(ref _connect, value); }
        }
        public Color TextColor
        {
            get { return _textColor; }
            set { SetProperty(ref _textColor, value); }
        }
        public string Data
        {
            get { return _data; }
            set 
            { 
                SetProperty(ref _data, value);
                Task c = new Task(async () =>
                {
                    var check = await _statusDataService.Get(Data);
                    Thread.Sleep(2000);
                    if (check.Id != "?")
                    {
                        CanExecute = true;
                        TextColor = Color.White;
                    }                    
                });
                c.Start();
            }
        }
        public bool CanExecute
        {
            get { return _canExecute; }
            set { SetProperty(ref _canExecute, value); }
        }

        public DelegateCommand Navigation { get; private set; }

        StatusDataService _statusDataService = new StatusDataService();

        public QRViewModel(IEventAggregator ea, INavigationService navigationService)
        {
            _eventAggregator = ea;
            _navigationService = navigationService;
            Navigation = new DelegateCommand(Navigate).ObservesCanExecute(() => CanExecute);
            ea.GetEvent<IStringEvent>().Subscribe(GetId, ThreadOption.UIThread);
        }

        private async void Navigate()
        {
            var data = new NavigationParameters
            {
                {   "Id", Data }
            };
            await _navigationService.NavigateAsync("MainDataView", data);
            MasterList.Update("MainDataView");
        }

        private void GetId(string id)
        {
            Data = id;
        }
    }
}

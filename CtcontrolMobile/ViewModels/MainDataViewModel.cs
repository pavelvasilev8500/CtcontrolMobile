using CtcontrolAPIService.Models;
using CtcontrolMobile.Dictionary.Language;
using CtcontrolMobile.EventAgregator;
using CtcontrolMobile.Models;
using CtcontrolMobile.Service;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Realms;
using System.Threading;
using Xamarin.Essentials;

namespace CtcontrolMobile.ViewModels
{
    class MainDataViewModel : BindableBase, INavigationAware
    {
        INavigationService _navigationService { get; }
        IEventAggregator _eventAggregator;

        Realm _realm;
        private string _title;
        private string _preview = AppResources.Preview;
        private StatusDataModel _data;


        private string _id { get; set; }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public string Preview
        {
            get { return _preview; }
            set { SetProperty(ref _preview, value); }
        }
        public StatusDataModel Data
        {
            get { return _data; }
            set { SetProperty(ref _data, value); }
        }

        private StatusDataService _statusDataService = new StatusDataService();

        public MainDataViewModel(IEventAggregator eventAggregator, INavigationService navigationService)
        {
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            _id = parameters.GetValue<string>("Id");
            Title = _id;
            var load = parameters.GetValue<string>("From");
            if(_id == null)
            {
                await _navigationService.NavigateAsync("ConnectionMethodIVew");
            }
            if (_id != null)
            {
                _eventAggregator.GetEvent<IStringEvent>().Publish(_id);
                Preferences.Set("Id", _id);
                if(load != "Load")
                {
                    Thread AddToHistoryThread = new Thread(ToHistoryList);
                    AddToHistoryThread.Name = "AddToHistory";
                    AddToHistoryThread.Start();
                }
            }
        }

        private async void ToHistoryList()
        {
            var data = await _statusDataService.Get(_id);
            _realm = Realm.GetInstance();
            switch (data.PCType)
            {
                case "laptop":
                    _realm.Write(() =>
                    {
                        _realm.Add(new HistoryDataModel
                        {
                            PcType = "laptop_white.svg",
                            PcName = $"{data.Id}"
                        }); ;
                    });
                    break;
                case "pc":
                    _realm.Write(() =>
                    {
                        _realm.Add(new HistoryDataModel
                        {
                            PcType = "desktop_white.svg",
                            PcName = $"{data.Id}"
                        });
                    });
                    break;
            }
        }
    }
}

using CtcontrolMobile.Dictionary.Language;
using CtcontrolMobile.EventAgregator;
using CtcontrolMobile.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CtcontrolMobile.MasterPage
{
    public static class MasterList
    {
        static IEventAggregator _eventAggregator;

        private static INavigationService _navigationService { get; set; }
        private static int _currentIndex { get; set; } = 0;
        private static int _newIndex { get; set; } = 0;

        private static ObservableCollection<MasterDetailElement> _masterList { get; set; } = new ObservableCollection<MasterDetailElement>();

        public static void Create(INavigationService navigationService, IEventAggregator ea)
        {
            _navigationService = navigationService;
            _eventAggregator = ea;
            var ll = new LanguageList();
            _masterList.Add(new MasterDetailElement() { visible = true, iamgePath = "home.svg", pageName = ll.Welcome, pagePath = "WelcomePage", color = Color.FromHex("#30FFFFFF"), NavigateCommand = new DelegateCommand<string>(NavigateCommandExecuted) });
            _masterList.Add(new MasterDetailElement() { visible = false, iamgePath = "laptop_white.svg", pageName = ll.Connection, pagePath = "MainDataView", color = Color.FromHex("#00000000"), NavigateCommand = new DelegateCommand<string>(NavigateCommandExecuted) });
        }

        private static async void NavigateCommandExecuted(string pagePath)
        {
            if (pagePath == "MainDataView")
            {
                var id = Preferences.Get("Id", "");
                if (id != "")
                {
                    var data = new NavigationParameters
                    {
                        { "Id", id },
                        { "From", "Load" }
                    };
                    await _navigationService.NavigateAsync($"NavigationPage/MainDataView", data);
                }
                else
                    await _navigationService.NavigateAsync($"NavigationPage/{pagePath}");
            }
            else
                await _navigationService.NavigateAsync($"NavigationPage/{pagePath}");
            Update(pagePath);
        }

        public static ObservableCollection<MasterDetailElement> GetAllItems()
        {
            return _masterList;
        }

        public static void Clear()
        {
            _masterList.Clear();
        }

        public static ObservableCollection<MasterDetailElement> Get()
        {
            var pagesList = new ObservableCollection<MasterDetailElement>();
            foreach (var o in _masterList)
                if (o.pageName != "")
                    pagesList.Add(o);
            return pagesList;
        }

        public static void Update(string pagePath)
        {
            _masterList.CollectionChanged += MasterList_CollectionChanged;
            _newIndex = _masterList.IndexOf(_masterList.FirstOrDefault(e => e.pagePath == pagePath));
            _masterList[_newIndex] = _masterList[_newIndex];
            _eventAggregator.GetEvent<IPageEvent>().Publish();
        }

        public static void UpdateList()
        {
            _masterList.Clear();
            Create(_navigationService, _eventAggregator);
            _eventAggregator.GetEvent<IPageLanguageEvent>().Publish();
        }

        private static void MasterList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Replace:
                    if (_masterList[_newIndex].pageName != "")
                    {
                        _masterList[_currentIndex].visible = false;
                        _masterList[_currentIndex].color = Color.Transparent;
                        _currentIndex = _newIndex;
                        _masterList[_currentIndex].visible = true;
                        _masterList[_currentIndex].color = Color.FromHex("#30FFFFFF");
                    }
                    break;
            }
        }
    }
}

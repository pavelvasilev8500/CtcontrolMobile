using System.Collections.ObjectModel;
using CtcontrolMobile.EventAgregator;
using CtcontrolMobile.MasterPage;
using CtcontrolMobile.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CtcontrolMobile.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        INavigationService _navigationService { get; }
        IEventAggregator _eventAggregator;

        private ObservableCollection<MasterDetailElement> _elements = new ObservableCollection<MasterDetailElement>();

        public ObservableCollection<MasterDetailElement> Elements
        {
            get { return _elements; }
            set { SetProperty(ref _elements, value); }
        }

        public DelegateCommand<string> NavigateSettingsCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService, IEventAggregator ea)
        {
            Preferences.Remove("Id");
            //Thread.Sleep(1000);
            _navigationService = navigationService;
            _eventAggregator = ea;
            ea.GetEvent<IPageEvent>().Subscribe(Update);
            ea.GetEvent<IPageLanguageEvent>().Subscribe(UpdateLanguage);
            //OnResume()
            if (MasterList.GetAllItems().Count != 0)
                MasterList.Clear();
            else
                MasterList.Create(_navigationService, _eventAggregator);
            Elements = new ObservableCollection<MasterDetailElement>(MasterList.Get());
            NavigateSettingsCommand = new DelegateCommand<string>(NavigateSettings);
        }

        private void UpdateLanguage()
        {
            Elements = new ObservableCollection<MasterDetailElement>(MasterList.Get());
            for (int i = 0; i < Elements.Count; i++)
            {
                Elements[i].color = Color.Transparent;
                Elements[i].visible = false;
            }
        }

        private void Update()
        {
            Elements = new ObservableCollection<MasterDetailElement>(MasterList.Get());
        }

        private async void NavigateSettings(string view)
        {
            await _navigationService.NavigateAsync($"NavigationPage/{view}");
            Elements = new ObservableCollection<MasterDetailElement>(MasterList.Get());
            for (int i = 0; i < Elements.Count; i++)
            {
                Elements[i].color = Color.Transparent;
                Elements[i].visible = false;
            }
        }
    }
}

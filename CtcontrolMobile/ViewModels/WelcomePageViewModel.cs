using CtcontrolMobile.Data;
using CtcontrolMobile.Dictionary.Language;
using CtcontrolMobile.MasterPage;
using CtcontrolMobile.Models;
using CtcontrolMobile.Service;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Realms;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CtcontrolMobile.ViewModels
{
    class WelcomePageViewModel : BindableBase, INavigationAware
    {
        private Realm _realm;
        private INavigationService _navigationService { get; }

        private string _title = AppResources.Welcome;
        private string _history = AppResources.History;
        private string _qrCode = AppResources.Qr_code;
        private string _idCode = AppResources.Id_code;
        private int _position = 0;
        private Color _color;

        private ObservableCollection<HistoryDataModel> _historyDataModel = new ObservableCollection<HistoryDataModel>();
        private ObservableCollection<CarouselDataModel> _carouselData = new ObservableCollection<CarouselDataModel>();

        private CancellationTokenSource _cts {get; set;}
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public string History
        {
            get { return _history = AppResources.History; }
            set { SetProperty(ref _history, value); }
        }
        public string QrCode
        {
            get { return _qrCode = AppResources.Qr_code; }
            set { SetProperty(ref _qrCode, value); }
        }
        public string IdCode
        {
            get { return _idCode; }
            set { SetProperty(ref _idCode, value); }
        }
        public int Position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }
        public Color Color
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }
        public ObservableCollection<HistoryDataModel> HistoryData
        {
            get { return _historyDataModel; }
            set { SetProperty(ref _historyDataModel, value); }
        }
        public ObservableCollection<CarouselDataModel> CarouselData
        {
            get { return _carouselData; }
            set { SetProperty(ref _carouselData, value); }
        }

        ClientDataService clientDataService = new ClientDataService();

        public DelegateCommand<string> NavigateCommand { get; private set; }

        public WelcomePageViewModel(INavigationService navigationService)
        {
            var status = Permissions.CheckStatusAsync<Permissions.Camera>();
            if (status.Result != PermissionStatus.Granted)
            {
                Permissions.RequestAsync<Permissions.Camera>();
            }
            _navigationService = navigationService;
            CarouselData.Add(new CarouselDataModel()
            {
                Title = AppResources.Explore_Ctcontrol,
                Subtitle = "",
                SubtitleVisible = false,
                ImageContent1 = "ctd.svg",
                ImageContent1Visible = true,
                ImageContent2 = "",
                ImageConten2Visible= false,
                ImageContent3 = "",
                ImageContent3Visible= false,
                ContentText = AppResources.Avalable_on,
                ContentTextVisible = true,
                PlatformImage1 = "windows10.svg",
                PlatformImage1Visible = false,
                PlatformImage2 = "windows11.svg",
                PlatformImage2Visible = true,
                SelectCommand = new DelegateCommand(WindowsSelect)
            }) ;
            CarouselData.Add(new CarouselDataModel()
            {
                Title = "Ctcontrol Mobile",
                Subtitle = AppResources.Control_your_desktop_laptop,
                SubtitleVisible = true,
                ImageContent1 = "laptop_black.svg",
                ImageContent1Visible = false,
                ImageContent2 = "desktop_black.svg",
                ImageConten2Visible = true,
                ImageContent3 = "",
                ImageContent3Visible = false,
                ContentText = AppResources.Avalable_on,
                ContentTextVisible = true,
                PlatformImage1 = "android.svg",
                PlatformImage1Visible = true,
                PlatformImage2 = "",
                PlatformImage2Visible = false,
                SelectCommand = new DelegateCommand(androidSelect)
            }); 
            CarouselData.Add(new CarouselDataModel()
            {
                Title = "Ctcontrol Mobile",
                Subtitle = AppResources.Connect___control,
                SubtitleVisible = true,
                ImageContent1 = "laptopsleep_white.svg",
                ImageContent1Visible = false,
                ImageContent2 = "laptoprestart_white.svg",
                ImageConten2Visible = false,
                ImageContent3 = "laptoppoweroff_white.svg",
                ImageContent3Visible = true,
                ContentText = "",
                ContentTextVisible = false,
                PlatformImage1 = "",
                PlatformImage1Visible = false,
                PlatformImage2 = "",
                PlatformImage2Visible = false,
                SelectCommand = new DelegateCommand(Select)
            });
            NavigateCommand = new DelegateCommand<string>(NavigateCommandExecuted);
        }

        private void SwitchCarousel()
        {
            do
            {
                Thread.Sleep(5000);
                switch (Position)
                {
                    case 0:
                        Position++;
                        break;
                    case 1:
                        Position++;
                        break;
                    case 2:
                        Position = 0;
                        break;
                }
            }
            while (!_cts.IsCancellationRequested);
        }

        private void WakeServer()
        {
            clientDataService.Get();
        }

        private void androidSelect()
        {
            Device.OpenUri(new Uri(ConnectionData.CtcontrolDesktop));
        }

        private void WindowsSelect()
        {
            Device.OpenUri(new Uri(ConnectionData.CtcontrolDesktop));
        }

        private async void Select()
        {
            await _navigationService.NavigateAsync($"ConnectionMethodIVew");
            MasterList.UpdateList();
        }

        private async void NavigateCommandExecuted(string view)
        {
            var result = await _navigationService.NavigateAsync($"{view}");
            if (!result.Success)
            {
                System.Diagnostics.Debugger.Break();
            }
        }

        private async void SelectLast(string id)
        {
            var data = new NavigationParameters
            {
                { "Id", id },
                { "From", "Load" }
            };
            await _navigationService.NavigateAsync("MainDataView", data);
            MasterList.Update("MainDataView");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            _cts.Cancel();
            _cts.Dispose();
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            _cts = new CancellationTokenSource();
            Thread SwithcTread = new Thread(SwitchCarousel);
            SwithcTread.Name = "SwithcThread";
            SwithcTread.Priority = ThreadPriority.BelowNormal;
            SwithcTread.Start();
            Thread WakeThread = new Thread(WakeServer);
            WakeThread.Name = "WakeThread";
            WakeThread.Priority = ThreadPriority.Lowest;
            WakeThread.Start();
            _realm = Realm.GetInstance();
            var list = _realm.All<HistoryDataModel>().ToList();
            for (int i = 0; i < list.Count; i++)
            {
                _realm.Write(() =>
                {
                    list[i].SelectLastCommand = new DelegateCommand<string>(SelectLast);
                });
                if (i % 2 == 0)
                    list[i].color = Color.FromHex("#FFFFFF");
                else
                {
                    if (list[i].PcType == "laptop_white.svg")
                        _realm.Write(() =>
                        {
                            list[i].PcType = "laptop_red.svg";
                        });
                    else if(list[i].PcType == "desktop_white.svg")
                        _realm.Write(() =>
                        {
                            list[i].PcType = "desktop_red.svg";
                        });
                    list[i].color = Color.FromHex("#C72C47");
                }
            }
            HistoryData.Clear();
            HistoryData = new ObservableCollection<HistoryDataModel>(list);
        }
    }
}

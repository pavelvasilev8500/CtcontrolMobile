using CtcontrolMobile.Dictionary.Language;
using CtcontrolMobile.Interfaces;
using CtcontrolMobile.MasterPage;
using CtcontrolMobile.Service;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using Xamarin.Forms;

namespace CtcontrolMobile.ViewModels
{
    class CodeViewViewModel : BindableBase
    {
        private ISpeechToText _speechRecongnitionInstance;
        private INavigationService _navigationService { get; }

        private string _title = AppResources.Connection_by_id;
        private string _connect = AppResources.Connect;
        private string _holder = AppResources.Id_Here;
        private string _pctype = "laptop_input.svg";
        private string _pageView;
        private string _id;

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
        public string Holder
        {
            get { return _holder; }
            set { SetProperty(ref _holder, value); }
        }
        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        public string PcType
        {
            get { return _pctype; }
            set { SetProperty(ref _pctype, value); }
        }
        public string PageView
        {
            get { return _pageView; }
            set { SetProperty(ref _pageView, value); }
        }

        public DelegateCommand<string> NavigateCommand { get; private set; }
        public DelegateCommand<string> SpeechToTextCommand { get; private set; }

        StatusDataService _statusDataService = new StatusDataService();

        public CodeViewViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            try
            {
                _speechRecongnitionInstance = DependencyService.Get<ISpeechToText>();
            }
            catch (Exception ex)
            {
                Id = ex.Message;
            }
            SpeechToTextCommand = new DelegateCommand<string>(SpeechToText);
            NavigateCommand = new DelegateCommand<string>(NavigateCommandExecuted);
        }

        private async void SpeechToText(string view)
        {
            try
            {
                PageView = view;
                _speechRecongnitionInstance.StartSpeechToText();
                MessagingCenter.Subscribe<IMessageSender, string>(this, "STT", (sender, args) =>
                {
                    SpeechToTextFinalResultRecieved(args);

                });
            }
            catch (Exception ex)
            {
                Id = ex.Message;
            }
        }

        private async void SpeechToTextFinalResultRecieved(string args)
        {
            Id = args.Replace(" ", "");
            if (Id == "One")
                Id = "1";
            var check = await _statusDataService.Get(Id);
            if (check.Id != "?")
            {
                var data = new NavigationParameters
                {
                    { "Id", Id }
                };
                var result = await _navigationService.NavigateAsync($"{PageView}", data);
                MasterList.Update(PageView);
                if (!result.Success)
                {
                    System.Diagnostics.Debugger.Break();
                }
            }
        }

        private async void NavigateCommandExecuted(string view)
        {
            var check = await _statusDataService.Get(Id);
            if(check.Id != "?")
            {
                var data = new NavigationParameters
                {
                    { "Id", Id }
                };
                var result = await _navigationService.NavigateAsync($"{view}", data);
                MasterList.Update(view);
                if (!result.Success)
                {
                    System.Diagnostics.Debugger.Break();
                }
            }
        }

    }
}

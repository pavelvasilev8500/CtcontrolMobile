using Prism.Commands;
using Prism.Mvvm;
using Xamarin.CommunityToolkit.Helpers;
using CtcontrolMobile.Dictionary.Language;
using System.Globalization;
using CtcontrolMobile.MasterPage;
using Xamarin.Essentials;

namespace CtcontrolMobile.ViewModels
{
    public class SettingViewModel : BindableBase
    {
        private string _title = AppResources.Settings;
        private string _currentLanguage;
        private string _russian = AppResources.Russian;
        private string _english = AppResources.English;
        private bool _visibility = false;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public string CurrentLanguage
        {
            get { return _currentLanguage; }
            set { SetProperty(ref _currentLanguage, value); }
        }
        public string Russian
        {
            get { return _russian; }
            set { SetProperty(ref _russian, value); }
        }
        public string English
        {
            get { return _english ; }
            set { SetProperty(ref _english , value); }
        }
        public bool Visibility
        {
            get { return _visibility; }
            set { SetProperty(ref _visibility, value); }
        }

        public DelegateCommand ShowLanguageCommand { get; private set; }
        public DelegateCommand<string> ChangeLanguageCommand { get; private set; }

        public SettingViewModel()
        {
            switch (Preferences.Get("lang", ""))
            {
                case "Russian":
                    CurrentLanguage = AppResources.Russian;
                    break;
                case "English":
                    CurrentLanguage = AppResources.English;
                    break;
                case "":
                    if (LocalizationResourceManager.Current.CurrentCulture.Name == "ru-RU")
                        CurrentLanguage = AppResources.Russian;
                    else
                        CurrentLanguage = AppResources.English;
                    break;
            }
            ShowLanguageCommand = new DelegateCommand(ShowLanguage);
            ChangeLanguageCommand = new DelegateCommand<string>(ChangeLanguage);
        }

        private void ShowLanguage()
        {
            if (!Visibility)
                Visibility = true;
            else Visibility = false;
        }

        private void ChangeLanguage(string lang)
        {
            switch(lang)
            {
                case "Russian":
                    LocalizationResourceManager.Current.CurrentCulture = new CultureInfo("ru-RU");
                    CultureInfo.CurrentUICulture = LocalizationResourceManager.Current.CurrentCulture;
                    CurrentLanguage = AppResources.Russian;
                    Preferences.Set("lang", "Russian");
                    break;
                case "English":
                    LocalizationResourceManager.Current.CurrentCulture = new CultureInfo("en-US");
                    CultureInfo.CurrentUICulture = LocalizationResourceManager.Current.CurrentCulture;
                    CurrentLanguage = AppResources.English;
                    Preferences.Set("lang", "English");
                    break;
            }
            Title = AppResources.Settings;
            Russian = AppResources.Russian;
            English = AppResources.English;
            Visibility = false;
            MasterList.UpdateList();
        }
    }
}

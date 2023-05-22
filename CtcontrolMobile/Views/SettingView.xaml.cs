using CtcontrolMobile.Dictionary.Language;
using Prism.Navigation;
using Xamarin.Forms;

namespace CtcontrolMobile.Views
{
    public partial class SettingView : ContentPage
    {
        INavigationService _navigationService;

        public SettingView(INavigationService navigationService)
        {
            ChangeLanguage.SelLocale();
            InitializeComponent();
            _navigationService = navigationService;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}

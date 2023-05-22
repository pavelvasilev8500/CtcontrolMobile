using CtcontrolMobile.Dictionary.Language;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CtcontrolMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConnectionMethodIVew : ContentPage
    {
        public ConnectionMethodIVew()
        {
            ChangeLanguage.SelLocale();
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
using CtcontrolMobile.Dictionary.Language;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CtcontrolMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PreView : ContentPage
    {
        public PreView()
        {
            ChangeLanguage.SelLocale();
            InitializeComponent();
        }
    }
}
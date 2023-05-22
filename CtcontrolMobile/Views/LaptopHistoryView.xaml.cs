using CtcontrolMobile.Dictionary.Language;
using Xamarin.Forms;

namespace CtcontrolMobile.Views
{
    public partial class LaptopHistoryView : ContentPage
    {
        public LaptopHistoryView()
        {
            ChangeLanguage.SelLocale();
            InitializeComponent();
        }
    }
}

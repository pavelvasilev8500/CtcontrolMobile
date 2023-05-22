using CtcontrolMobile.Dictionary.Language;
using Xamarin.Forms;

namespace CtcontrolMobile.Views
{
    public partial class DesctopHistoryView : ContentPage
    {
        public DesctopHistoryView()
        {
            ChangeLanguage.SelLocale();
            InitializeComponent();
        }
    }
}

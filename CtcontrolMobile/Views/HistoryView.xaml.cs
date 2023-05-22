using CtcontrolMobile.Dictionary.Language;
using Xamarin.Forms;

namespace CtcontrolMobile.Views
{
    public partial class HistoryView : TabbedPage
    {
        public HistoryView()
        {
            ChangeLanguage.SelLocale();
            InitializeComponent();
        }
    }
}

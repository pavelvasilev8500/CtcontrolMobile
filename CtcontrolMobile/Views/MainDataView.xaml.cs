using CtcontrolMobile.Dictionary.Language;
using Prism.Services.Dialogs;
using Xamarin.Forms;

namespace CtcontrolMobile.Views
{
    public partial class MainDataView : TabbedPage
    {
        DialogService _dialogService;

        public MainDataView(DialogService dialogService)
        {
            ChangeLanguage.SelLocale();
            InitializeComponent();
            _dialogService = dialogService;
        }

        protected override bool OnBackButtonPressed()
        {
            _dialogService.ShowDialog("ExitDialogView");
            return true;
        }
    }
}
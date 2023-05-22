using CtcontrolMobile.Dictionary.Language;
using Prism.Services.Dialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CtcontrolMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WelcomePage : ContentPage
	{
        DialogService _dialogService;

		public WelcomePage (DialogService dialogService)
		{
            ChangeLanguage.SelLocale();
			InitializeComponent ();
            _dialogService = dialogService;
        }

        protected override bool OnBackButtonPressed()
        {
            _dialogService.ShowDialog("ExitDialogView");
            return true;
        }
    }
}

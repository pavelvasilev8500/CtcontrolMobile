using CtcontrolMobile.Dictionary.Language;
using CtcontrolMobile.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using Xamarin.Forms;

namespace CtcontrolMobile.Dialog
{
    public class ExitDialogViewModel : BindableBase, IDialogAware
    {
        private string _title = AppResources.Exit;
        private string _yes = AppResources.Yes;
        private string _no = AppResources.No;

        public event Action<IDialogParameters> RequestClose;

        public DelegateCommand CloseCommand { get; private set; }
        public DelegateCommand CloseAppCommand { get; private set; }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public string Yes
        {
            get { return _yes; }
            set { SetProperty(ref _yes, value); }
        }
        public string No
        {
            get { return _no; }
            set { SetProperty(ref _no, value); }
        }

        public ExitDialogViewModel()
        {
            CloseCommand = new DelegateCommand(() => RequestClose(null));
            CloseAppCommand = new DelegateCommand(CloseApp);
        }

        private void CloseApp()
        {
            DependencyService.Get<ICloseApplication>().CloseApp();
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}

using Prism.Mvvm;

namespace CtcontrolMobile.Dictionary.Language
{
    public class LanguageList : BindableBase
    {
        private string _welcome = AppResources.Welcome;
        public string Welcome
        {
            get { return _welcome; }
            set { SetProperty(ref _welcome, value); }
        }

        private string _connection = AppResources.Connection;
        public string Connection
        {
            get { return _connection; }
            set { SetProperty(ref _connection, value); }
        }

    }
}

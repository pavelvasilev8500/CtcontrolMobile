using CtcontrolMobile.Dictionary.Language;
using Prism.Mvvm;

namespace CtcontrolMobile.ViewModels
{
    public class HistoryViewModel : BindableBase
    {
        private string _title = AppResources.History;
        private string _laptop = AppResources.Laptop;
        private string _desktop = AppResources.Desktop;

        public string Title
        {
            get { return _title ; }
            set { SetProperty(ref _title, value); }
        }
        public string Laptop
        {
            get { return _laptop; }
            set { SetProperty(ref _laptop, value); }
        }
        public string Desktop
        {
            get { return _desktop; }
            set { SetProperty(ref _desktop, value); }
        }

        public HistoryViewModel()
        {
        }
    }
}

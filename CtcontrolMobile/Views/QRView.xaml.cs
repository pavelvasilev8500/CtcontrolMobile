using CtcontrolMobile.Dictionary.Language;
using CtcontrolMobile.EventAgregator;
using Prism.Events;

namespace CtcontrolMobile.Views
{
    public partial class QRView
    {
        IEventAggregator _eventAggregator;
        public QRView(IEventAggregator eventAggregator)
        {
            ChangeLanguage.SelLocale();
            InitializeComponent();
            _eventAggregator = eventAggregator;
        }

        private void scanView_OnScanResult(ZXing.Result result)
        {
            _eventAggregator.GetEvent<IStringEvent>().Publish(result.Text);
        }
    }
}
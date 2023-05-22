using Prism.Commands;
using Xamarin.Forms;

namespace CtcontrolMobile.Models
{
    public class MasterDetailElement
    {
        public bool visible { get; set; }
        public string iamgePath { get; set; }
        public string pageName { get; set; }
        public string pagePath { get; set; }
        public Color color { get; set; }
        public DelegateCommand<string> NavigateCommand { get; set; }
    }
}

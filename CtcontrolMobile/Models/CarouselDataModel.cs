using Prism.Commands;

namespace CtcontrolMobile.Models
{
    public class CarouselDataModel
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public bool SubtitleVisible { get; set; }
        public string ImageContent1 { get; set; }
        public bool ImageContent1Visible { get; set; }
        public string ImageContent2 { get; set; }
        public bool ImageConten2Visible { get; set; }
        public string ImageContent3 { get; set; }
        public bool ImageContent3Visible { get; set; }
        public string ContentText { get; set; }
        public bool ContentTextVisible { get; set; }
        public string PlatformImage1 { get; set; }
        public bool PlatformImage1Visible { get; set; }
        public string PlatformImage2 { get; set; }
        public bool PlatformImage2Visible { get; set; }
        public DelegateCommand SelectCommand { get; set; }
    }
}

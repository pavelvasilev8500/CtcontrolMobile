using Xamarin.Forms;

namespace CtcontrolMobile.Models
{
    public class ShadowModel : RoutingEffect
    {
        public float Radius { get; set; }

        public Color Color { get; set; }

        public float DistanceX { get; set; }

        public float DistanceY { get; set; }

        public ShadowModel() : base("MyCompany.LabelShadowEffect")
        {
        }
    }
}

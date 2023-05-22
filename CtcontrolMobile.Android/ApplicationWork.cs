using CtcontrolMobile.Interfaces;
using CtcontrolMobile.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidMethods))]
namespace CtcontrolMobile.Droid
{
    public class AndroidMethods : ICloseApplication
    {
        public void CloseApp()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}
using System.Globalization;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Essentials;

namespace CtcontrolMobile.Dictionary.Language
{
    public static class ChangeLanguage
    {
        public static void SelLocale()
        {
            string lang = Preferences.Get("lang", $"{LocalizationResourceManager.Current.CurrentCulture}");
            switch (lang)
            {
                case "Russian":
                    LocalizationResourceManager.Current.CurrentCulture = new CultureInfo("ru-RU");
                    CultureInfo.CurrentUICulture = new CultureInfo("ru-RU");
                    break;
                case "English":
                    LocalizationResourceManager.Current.CurrentCulture = new CultureInfo("en-US");
                    CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                    break;
            }
        }
    }
}

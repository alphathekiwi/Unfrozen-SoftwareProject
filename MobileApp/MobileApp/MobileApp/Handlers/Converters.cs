using System;
using System.Globalization;
using Xamarin.Forms;

namespace MobileApp.Handlers
{
    class ConvertUserId : IValueConverter
    {
        public object Convert(object val, Type tT, object pram, CultureInfo culture)
        {
            if (val.GetType() == typeof(int) && (int)val > 0)
            {
                Models.User user = App.Database.GetUser((int)val);
                return user.ToString();
            }
            return "Unkown";
        }
        public object ConvertBack(object val, Type tT, object pram, CultureInfo culture)
        {
            if (val.GetType() == typeof(string) && (string)val != "")
            {
                Models.User user = App.Database.GetUser((string)val);
                return user.Id;
            }
            return 0;
        }
    }
    class ConvertVisibility : IValueConverter
    {
        public object Convert(object val, Type tT, object pram, CultureInfo culture)
        {
            if (val.GetType() == typeof(bool))
                return (bool)val ? "Make Private" : "Make Public";
            return "Error";
        }

        public object ConvertBack(object val, Type tT, object pram, CultureInfo culture)
        {
            if (val.GetType() == typeof(string))
                return (string)val == "Make Public";
            return false;
        }
    }
    class InvertBool : IValueConverter
    {
        public object Convert(object val, Type tT, object pram, CultureInfo culture) => val.GetType() == typeof(bool) ? !(bool)val : false;

        public object ConvertBack(object val, Type tT, object pram, CultureInfo culture) => val.GetType() == typeof(bool) ? !(bool)val : false;
    }
}

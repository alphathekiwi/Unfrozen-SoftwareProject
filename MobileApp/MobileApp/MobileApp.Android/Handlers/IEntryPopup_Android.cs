using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileApp.Droid.Handlers;
using MobileApp.Handlers;
using Xamarin.Forms;

[assembly: Dependency(typeof(IEntryPopup_Android))]
namespace MobileApp.Droid.Handlers
{
    class IEntryPopup_Android : IEntryPopup
    {
        public void ShowPopup(EntryPopup popup)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(MainActivity.Instance);

            EditText edit = new EditText(MainActivity.Instance) { Text = popup.Text };
            edit.SetSingleLine(false);
            edit.InputType |= Android.Text.InputTypes.TextFlagMultiLine;
            edit.SetLines(2);
            edit.SetMaxLines(5);
            edit.Gravity = GravityFlags.Left | GravityFlags.Top;
            edit.VerticalScrollBarEnabled = false;

            alert.SetView(edit);
            alert.SetTitle(popup.Title);
            alert.SetPositiveButton("OK", (senderAlert, args) =>
            {
                popup.OnPopupClosed(new EntryPopupClosedArgs
                {
                    Button = "OK",
                    Text = edit.Text
                });
            });

            alert.SetNegativeButton("Cancel", (senderAlert, args) =>
            {
                popup.OnPopupClosed(new EntryPopupClosedArgs
                {
                    Button = "Cancel",
                    Text = edit.Text
                });
            });
            alert.Show();
        }
    }
}
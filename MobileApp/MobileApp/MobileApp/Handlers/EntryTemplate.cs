using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.Handlers
{
    public class EntryPopup
    {
        public string Text { get; set; }
        public string Title { get; set; }
        public List<string> Buttons { get; set; }

        public EntryPopup(string title, string text, params string[] buttons)
        {
            Title = title;
            Text = text;
            Buttons = buttons.ToList();
        }

        public EntryPopup(string title, string text) : this(title, text, "OK", "Cancel")
        {
        }

        public event EventHandler<EntryPopupClosedArgs> PopupClosed;
        public void OnPopupClosed(EntryPopupClosedArgs e)
        {
            var handler = PopupClosed;
            if (handler != null)
                handler(this, e);
        }

        public void Show()
        {
            DependencyService.Get<IEntryPopup>().ShowPopup(this);
        }
    }
    public class EntryPopupClosedArgs : EventArgs
    {
        public string Text { get; set; }
        public string Button { get; set; }
    }
}

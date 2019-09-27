using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Navigation : MasterDetailPage
    {
        public Navigation()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var settings = new ToolbarItem
            {
                IconImageSource = "profile.png",
                Command = new Command(() => { Navigation.PushAsync(new Profile(App.CurrentUser)); }),
            };
            ToolbarItems.Add(settings);
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as NavigationMasterMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}
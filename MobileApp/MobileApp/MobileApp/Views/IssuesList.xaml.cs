using MobileApp.Models;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IssuesList : ContentPage
    {
        private int Id = 0;
        public ObservableCollection<Issue> Issues { get; set; }
        public Command NewPost { get; set; }
        public Command MyPosts { get; set; }
        public IssuesList(int id = 0)
        {
            var settings = new ToolbarItem
            {
                IconImageSource = "profile.png",
                Command = new Command(() => { Navigation.PushAsync(new Profile(App.CurrentUser)); }),
            }; Id = id;
            ToolbarItems.Add(settings);
            Title = "Products list view";
            BindingContext = this;
            NewPost = new Command(() => { Navigation.PushAsync(new IssueForm(new Issue())); });
            MyPosts = new Command(() => { Navigation.PushAsync(new IssuesList(App.CurrentUser.Id)); });
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Issues = new ObservableCollection<Issue>(App.Database.GetIssues(Id));
            IssuesListView.ItemsSource = Issues;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            await Navigation.PushAsync(new IssueDetails((Issue)((ListView)sender).SelectedItem));
        }
    }
}
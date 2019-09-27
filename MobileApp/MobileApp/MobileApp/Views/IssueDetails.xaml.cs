using MobileApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IssueDetails : ContentPage, INotifyPropertyChanged
    {
        public Issue Issue { get; set; }
        public Command LikeCommand { get; set; }
        public Command ProfileCommand { get; set; }
        public IssueDetails(Issue details)
        {
            Issue = details;
            Title = Issue.Title;
            BindingContext = this;
            LikeCommand = new Command(Like);
            ProfileCommand = new Command(() => { ViewProfile(Issue.Author); });
            InitializeComponent();
            DecorateLikeButton();
        }
        public event PropertyChangedEventHandler PropChanged;
        protected void OnPropChanged([CallerMemberName] string name = "") => PropChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        public void DecorateLikeButton()
        {
            bool liked = Issue.HasLiked(App.CurrentUser.Id);
            LikeButton.Text = $"{Issue.TotalLikes()} ❤️  Like{(liked ? 'd' : ' ')}";
            LikeButton.BackgroundColor = liked ? new Color(0.9, 0.8, 0.8) : new Color(0.7, 0.9, 0.9);
        }
        public void Like()
        {
            Issue.ManageFeedback(App.CurrentUser.Id, !Issue.HasLiked(App.CurrentUser.Id));
            App.Database.SaveIssue(Issue);
            DecorateLikeButton();
        }
        public void ViewProfile(int id) => Navigation.PushAsync(new Profile(App.Database.GetUser(id)));
    }
}
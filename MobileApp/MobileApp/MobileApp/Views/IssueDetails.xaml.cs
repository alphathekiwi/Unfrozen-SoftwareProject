using MobileApp.Handlers;
using MobileApp.Models;
using System;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Comment> Comments { get; set; }
        public Command LikeCommand { get; set; }
        public Command CommentCommand { get; set; }
        public Command ProfileCommand { get; set; }
        public IssueDetails(Issue details)
        {
            Issue = details;
            Title = Issue.Title;
            BindingContext = this;
            LikeCommand = new Command(Like);
            CommentCommand = new Command(Comment);
            ProfileCommand = new Command(() => { ViewProfile(Issue.Author); });
            InitializeComponent();
            DecorateLikeButton();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Comments = new ObservableCollection<Comment>(App.Database.GetComments(Issue.Id));
            IssuesListView.ItemsSource = Comments;
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
        public void Comment()
        {
            EntryPopup popup = new EntryPopup("Your Comment", string.Empty, "OK", "Cancel");
            popup.PopupClosed += (o, closedArgs) => PostComment(o, closedArgs);
            popup.Show();
        }

        private void PostComment(object o, EntryPopupClosedArgs closedArgs)
        {
            if (closedArgs.Button == "OK") {
                App.Database.SaveComment(new Comment(App.CurrentUser.Id, Issue.Id, closedArgs.Text));
                Comments = new ObservableCollection<Comment>(App.Database.GetComments(Issue.Id));
                IssuesListView.ItemsSource = Comments;
            }
        }
        public void ViewProfile(int id) => Navigation.PushAsync(new Profile(App.Database.GetUser(id)));
        async void CommentTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            Comment comment = (Comment)((ListView)sender).SelectedItem;
            await Navigation.PushAsync(new Profile(App.Database.GetUser(comment.Author)));
        }
    }
}
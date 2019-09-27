using MobileApp.Handlers;
using MobileApp.Models;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileForm : ContentPage, INotifyPropertyChanged
    {
        public bool NotUser { get; set; }
        public bool ImagePick;
        public User user { get; set; }
        public string PrivateName { get => user.PublicName ? user.UserName : $"{user.FirstName} {user.LastName}"; }
        public Command Logout { get; set; }
        public Command ToggleName { get; set; }
        public Command ToggleEmail { get; set; }
        public Command TogglePhone { get; set; }
        public ProfileForm(User user)
        {
            var settings = new ToolbarItem {
                IconImageSource = "edit.png",
                Command = new Command(() => { Navigation.PushAsync(new Profile(App.CurrentUser)); }),
            }; ToolbarItems.Add(settings);
            this.user = user;
            Title = user.ToString();
            BindingContext = this;
            NotUser = user.Id == App.CurrentUser.Id; ImagePick = NotUser;
            if (NotUser)
            {
                ToggleName = new Command(() => { user.PublicName ^= true; test(user.PublicName); });
                ToggleEmail = new Command(() => { user.PublicEmail ^= true; test(user.PublicEmail); });
                TogglePhone = new Command(() => { user.PublicPhone ^= true; test(user.PublicPhone); });
            }
            InitializeComponent();
            ProfilePic.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => { setPictureAsync(); }) });
        }
        public event PropertyChangedEventHandler PropChanged;
        protected void OnPropChanged([CallerMemberName] string name = "") => PropChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        private void test(bool val)
        {
            System.Console.Out.WriteLine($"Set public status to: {val}");
            App.Database.SaveUser(user);
            OnPropChanged("user");
        }
        private async void setPictureAsync()
        {
            if (!ImagePick) return;
            ImagePick = false;
            Stream stream = await DependencyService.Get<IPhotoPick>().GetImageStreamAsync();
            if (stream != null)
            {
                ProfilePic.Source = ImageSource.FromStream(() => stream);
                App.CurrentUser.Image = ProfilePic.Source.ToString();
                System.Console.Out.WriteLine("Saving image: " + ProfilePic.Source.ToString());
                App.Database.SaveUser(App.CurrentUser);
            }
            ImagePick = true;
        }
    }
}
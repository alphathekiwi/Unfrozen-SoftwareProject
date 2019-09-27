using MobileApp.Handlers;
using MobileApp.Models;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage, INotifyPropertyChanged
    {
        public bool NotUser { get; set; }
        public bool ImagePick;
        public User User { get; set; }
        public string PrivateName { get => User.PublicName ? User.UserName : $"{User.FirstName} {User.LastName}"; }
        public Command Logout { get; set; }
        public Command SaveProfile { get; set; }
        public Command ToggleName { get; set; }
        public Command ToggleEmail { get; set; }
        public Command TogglePhone { get; set; }
        public Profile(User user)
        {
            var settings = new ToolbarItem {
                IconImageSource = "edit.png",
                Command = new Command(SaveCommand),
            }; ToolbarItems.Add(settings);
            this.User = user;
            Title = user.ToString();
            BindingContext = this;
            NotUser = user.Id == App.CurrentUser.Id; ImagePick = NotUser;
            if (NotUser)
            {
                Logout = new Command(LogoutCommand);
                SaveProfile = new Command(SaveCommand);
                ToggleName = new Command(() => { user.PublicName ^= true; user.OnPropChanged("PublicName"); OnPropChanged("PrivateName"); });
                ToggleEmail = new Command(() => { user.PublicEmail ^= true; user.OnPropChanged("PublicEmail"); });
                TogglePhone = new Command(() => { user.PublicPhone ^= true; user.OnPropChanged("PublicPhone"); });
            }
            InitializeComponent();
            ProfilePic.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => { setPictureAsync(); }) });
        }

        public event PropertyChangedEventHandler PropChanged;
        protected void OnPropChanged([CallerMemberName] string name = "") => PropChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        private async void setPictureAsync()
        {
            if (!ImagePick) return;
            ImagePick = false;
            Stream stream = await DependencyService.Get<IPhotoPick>().GetImageStreamAsync();
            if (stream != null) {
                ProfilePic.Source = ImageSource.FromStream(() => stream);
                App.CurrentUser.Image = ProfilePic.Source.ToString();
                System.Console.Out.WriteLine("Saving image: " + ProfilePic.Source.ToString());
                App.Database.SaveUser(App.CurrentUser);
            }
            ImagePick = true;
        }

        void SaveCommand()
        {
            User.Email = userEmail.Text;
            User.PhoneNumber = userPhone.Text;
            if (!User.PublicName && userName.Text.Length > 2) {
                string[] names = userName.Text.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
                User.FirstName = names[0];
                User.LastName = names.Length > 1 ? names[names.Length - 1] : "Test";
            }    
            App.Database.SaveUser(User);
        }

        void LogoutCommand()
        {
            App.CurrentUser = null;
            App.Current.MainPage = new LoginPage();
        }
    }
}
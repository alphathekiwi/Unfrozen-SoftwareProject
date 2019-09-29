using MobileApp.Handlers;
using MobileApp.Models;
using PropertyChanged;
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
        public string PrivateName {
            get => User.PublicName ? User.UserName : $"{User.FirstName} {User.LastName}";
            set {
                if (!User.PublicName) {
                    string[] names = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    User.FirstName = names.Length > 0 ? names[0] : "First";
                    User.LastName = names.Length > 1 ? names[names.Length - 1] : "Last";
                }
            }
        }
        public Command Logout { get; set; }
        public Command SaveProfile { get; set; }
        public Command ToggleName { get; set; }
        public Command ToggleEmail { get; set; }
        public Command TogglePhone { get; set; }
        public Profile(User user)
        {
            //var settings = new ToolbarItem {
            //    IconImageSource = "edit.png",
            //    Command = new Command(SaveCommand),
            //}; ToolbarItems.Add(settings);
            BindingContext = this;
            this.User = user;
            NotUser = user.Id == App.CurrentUser.Id;
            ImagePick = NotUser;
            Title = user.ToString();
            if (NotUser) {
                Logout = new Command(LogoutCommand);
                SaveProfile = new Command(SaveCommand);
                ToggleName = new Command(() => { user.PublicName ^= true; Title = user.ToString(); userName.Text = PrivateName; });
                ToggleEmail = new Command(() => { user.PublicEmail ^= true; });
                TogglePhone = new Command(() => { user.PublicPhone ^= true; });
            }
            InitializeComponent();
            ProfilePic.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => { setPictureAsync(); }) });
            if (user.Image != "") SetPhotoFromData();
        }
        //public event PropertyChangedEventHandler PropChanged;
        //protected void OnPropChanged([CallerMemberName] string name = "") => PropChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        private void SetPhotoFromData()
        {
            System.Console.Out.WriteLine($"[Attempting to load] {User.Image}");
            //Image data was invalid: Xamarin.Forms.StreamImageSource
            Stream stream = new MemoryStream(Convert.FromBase64String(User.Image));
            //StreamImageSource stream = new StreamImageSource() { Stream = new MemoryStream(Convert.FromBase64String(User.Image)); };
            ProfilePic.Source = ImageSource.FromStream(() => stream);
        }
        private async void setPictureAsync()
        {
            if (!ImagePick) return;
            ImagePick = false;
            Stream stream = await DependencyService.Get<IPhotoPick>().GetImageStreamAsync();
            if (stream != null) {
                byte[] bytes = new byte[1024];
                string base64 = "";
                while(await stream.ReadAsync(bytes, 0, 512) > 0);
                    base64 += Convert.ToBase64String(bytes);
                System.Console.Out.WriteLine($"[String] {base64}");
                App.CurrentUser.Image = base64;
                App.Database.SaveUser(App.CurrentUser);
            }
            ImagePick = true;
        }

        void SaveCommand()
        {
            PrivateName = userName.Text; 
            User.Email = userEmail.Text;
            User.PhoneNumber = userPhone.Text;
            App.Database.SaveUser(User);
        }

        void LogoutCommand()
        {
            App.CurrentUser = null;
            App.Current.MainPage = new LoginPage();
        }
    }
}
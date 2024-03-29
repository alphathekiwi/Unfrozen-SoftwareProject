﻿using MobileApp.Models;
using System.ComponentModel;
using Xamarin.Forms;

namespace MobileApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class LoginPage : ContentPage
    {
        public Command LoginButton { get; }
        public LoginPage()
        {
            BindingContext = this;
            LoginButton = new Command(Login_Button);
            InitializeComponent();
        }
        void Login_Button()
        {
            User user = App.Database.VerifyUser(EntryUsername.Text, EntryPassword.Text);
            if (user != null)
            {
                App.CurrentUser = user;
                App.Current.MainPage = new NavigationPage(new IssuesList());
            }
            else
                this.DisplayAlert("Error", "Incorrect User Name or Password", "Okay");
        }
    }
}

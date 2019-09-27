using MobileApp.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IssueForm : ContentPage
    {

        public Issue Issue { get; set; }
        public Command Submit { get; set; }
        public Command Cancel { get; set; }
        public IssueForm(Issue issue)
        {
            Issue = issue;
            Title = Issue.Title ?? "New Post";
            BindingContext = this;
            Submit = new Command(SaveIssue);
            Cancel = new Command(() => { Navigation.PopAsync(); });
            InitializeComponent();
        }
        void SaveIssue()
        {
            if (Issue.Author == 0)
                Issue.Author = App.CurrentUser.Id;
            System.Console.Out.WriteLine($"Saving issue: {Issue.Id} by: {Issue.Author}");
            App.Database.SaveIssue(Issue);
            Navigation.PopAsync();
        }
    }
}
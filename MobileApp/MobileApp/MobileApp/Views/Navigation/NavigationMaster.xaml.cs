using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationMaster : ContentPage
    {
        public ListView ListView;

        public NavigationMaster()
        {
            InitializeComponent();

            BindingContext = new NavigationMasterViewModel();
            ListView = MenuItemsListView;
        }

        class NavigationMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<NavigationMasterMenuItem> MenuItems { get; set; }

            public NavigationMasterViewModel()
            {
                MenuItems = new ObservableCollection<NavigationMasterMenuItem>(new[]
                {
                    new NavigationMasterMenuItem { Id = 0, Title = "Page 1" },
                    new NavigationMasterMenuItem { Id = 1, Title = "Page 2" },
                    new NavigationMasterMenuItem { Id = 2, Title = "Page 3" },
                    new NavigationMasterMenuItem { Id = 3, Title = "Page 4" },
                    new NavigationMasterMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}
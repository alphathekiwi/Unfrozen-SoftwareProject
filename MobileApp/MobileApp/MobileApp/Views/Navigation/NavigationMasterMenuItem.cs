using System;

namespace MobileApp.Views.Navigation
{

    public class NavigationMasterMenuItem
    {
        public NavigationMasterMenuItem()
        {
            TargetType = typeof(NavigationMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}
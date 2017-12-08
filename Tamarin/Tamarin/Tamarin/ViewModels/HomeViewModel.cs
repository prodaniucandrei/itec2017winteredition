using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Tamarin.Models;
using Xamarin.Forms;

namespace Tamarin.ViewModels
{
    public class HomeViewModel : BindableBase, INavigationAware
    {
        private string _item;
        public string Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }
        public ImageSource UserImage { get; set; }
        public ObservableCollection<HomePageMenuItem> MenuItems { get; set; }
        public HomeViewModel()
        {
            MenuItems = new ObservableCollection<HomePageMenuItem>(new[] 
            {
                new HomePageMenuItem { Id = 0, Title = "Categories", Icon = ImageSource.FromFile("categories.png") },
                new HomePageMenuItem { Id = 1, Title = "Promotions", Icon = ImageSource.FromFile("promotions.png") },
                new HomePageMenuItem { Id = 2, Title = "Cart", Icon = ImageSource.FromFile("cart.png") },
                new HomePageMenuItem { Id = 3, Title = "Logout", Icon = ImageSource.FromFile("logout.png") },
            });
            UserImage = ImageSource.FromFile("user.png");
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("title"))
                Item = (string)parameters["title"] + " and Prism";



        }
    }
}

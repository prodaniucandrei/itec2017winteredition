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
    public class HomeViewModel : BaseViewModel
    {
        //private string _item;
        //public string Item
        //{
        //    get { return _item; }
        //    set { SetProperty(ref _item, value); }
        //}
        //public ImageSource UserImage { get; set; }
        //public ObservableCollection<HomePageMenuItem> MenuItems { get; set; }
        public HomeViewModel(INavigationService navigationService): base(navigationService)
        {
            //MenuItems = new ObservableCollection<HomePageMenuItem>(new[] 
            //{
            //    new HomePageMenuItem { Id = 0, Title = "Categories", Icon = ImageSource.FromFile("categories.png") },
            //    new HomePageMenuItem { Id = 1, Title = "Promotions", Icon = ImageSource.FromFile("promotions.png") },
            //    new HomePageMenuItem { Id = 2, Title = "Cart", Icon = ImageSource.FromFile("cart.png") },
            //    new HomePageMenuItem { Id = 3, Title = "Logout", Icon = ImageSource.FromFile("logout.png") },
            //});
            //UserImage = ImageSource.FromFile("user.png");
            NavigateCommand = new DelegateCommand<string>(OnNavigateCommandExecuted);
        }

        public DelegateCommand<string> NavigateCommand { get; }

        private async void OnNavigateCommandExecuted(string path)
        {
            await _navigationService.NavigateAsync(path);
        }
    }
}

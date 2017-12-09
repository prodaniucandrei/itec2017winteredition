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
        public ImageSource UserImage { get; set; }
        public ObservableCollection<HomePageMenuItem> MenuItems { get; set; }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        public HomeViewModel(INavigationService navigationService): base(navigationService)
        {
            MenuItems = new ObservableCollection<HomePageMenuItem>(new[]
            {
                new HomePageMenuItem { Id = 0, Title = "Orar", Icon = ImageSource.FromFile("orar.png") },
                new HomePageMenuItem { Id = 1, Title = "Colegi", Icon = ImageSource.FromFile("colegi.png") },
                new HomePageMenuItem { Id = 2, Title = "Logout", Icon = ImageSource.FromFile("logout.png") },
            });
            Username = App.Current.Properties["email"] as string;
            UserImage = ImageSource.FromFile("user.png");
            NavigateCommand = new DelegateCommand<string>(OnNavigateCommandExecuted);
            LogoutCommand = new DelegateCommand<string>(OnLogoutCommandExecuted);
        }

        public DelegateCommand<string> NavigateCommand { get; }

        private async void OnNavigateCommandExecuted(string path)
        {
            await _navigationService.NavigateAsync(path);
        }

        public DelegateCommand<string> LogoutCommand { get; }

        private async void OnLogoutCommandExecuted(string path)
        {
            App.Current.Properties["id"] = null;
            App.Current.Properties["email"] = null;
            App.Current.Properties["roles"] = null;
            App.Current.Properties["token"] = null;
            App.Current.Properties["isLoggedIn"] = "false";

            await _navigationService.NavigateAsync("/Login");
        }
    }
}

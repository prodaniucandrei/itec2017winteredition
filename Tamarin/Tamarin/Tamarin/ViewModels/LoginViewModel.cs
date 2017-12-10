using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Tamarin.Models;
using Tamarin.Services;
using Xamarin.Forms;

namespace Tamarin.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private double _elementsOpacity;
        private string _errorMessage;
        private string _username = "";
        private string _password ="";

        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public double ElementsOpacity
        {
            get { return _elementsOpacity; }
            set
            {
                SetProperty(ref _elementsOpacity, value);
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                SetProperty(ref _errorMessage, value);
            }
        }

        public DelegateCommand LoginCommand { get; set; }

        public LoginViewModel(INavigationService navigationService): base(navigationService)
        {
            LoginCommand = LoginCommand = new DelegateCommand(OnLoginCommandExecuted);

            ElementsOpacity = 1;
        }

        public async void OnLoginCommandExecuted()
        {
            IsBusy = true;
            ElementsOpacity = .2;

            var model = new LoginModel();
            model.Username = Username;
            model.Password = Password;

            var response = await AuthService.Login(model);

            if(response.IsSuccessStatusCode)
            {
                IsBusy = false;
                ElementsOpacity = 1;

                var content = await response.Content.ReadAsStringAsync();
                var message = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                Application.Current.Properties["id"] = message["id"];
                Application.Current.Properties["email"] = message["email"];
                Application.Current.Properties["roles"] = message["roles"];
                Application.Current.Properties["token"] = message["auth_token"];
                Application.Current.Properties["isLoggedIn"] = "true";

                await _navigationService.NavigateAsync("/Home/Navigation/Dashboard?message=Welcome");
            }
            else
            {
                IsBusy = false;
                ElementsOpacity = 1;

                //await DisplayAlert("Error", "Username or password is not corect", "OK");
            }
        }

        public override async void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}

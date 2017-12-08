using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Tamarin.ViewModels
{
    public class LoginViewModel : BindableBase, INavigationAware
    {
        private bool _isBusy;
        private double _elementsOpacity;
        private string _errorMessage;
        private string _username;
        private string _password;

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

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty(ref _isBusy, value);
            }
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

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        private INavigationService _navigationService;


        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            LoginCommand = new Command(LoginHandler);
            RegisterCommand = new Command(RegisterHandler);
        }

        public void LoginHandler()
        {
            _navigationService.NavigateAsync("/Home?title=hello");
        }

        public void RegisterHandler()
        {

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
                Username = (string)parameters["title"] + " and Prism";
            ElementsOpacity = 1;
        }
    }
}

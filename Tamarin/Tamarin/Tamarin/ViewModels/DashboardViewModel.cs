﻿using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamarin.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public DashboardViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Dashboard";
            LogoutCommand = new DelegateCommand(OnLogoutCommandExecuted);
            NavigateCommand = new DelegateCommand(OnNavigateExecuted);
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public DelegateCommand LogoutCommand { get; }
        public DelegateCommand NavigateCommand { get; }

        public async void OnNavigateExecuted()
        {
            await _navigationService.NavigateAsync("/Home/Navigation/Subject");
        }
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            Message = parameters.GetValue<string>("message");
        }

        public void OnLogoutCommandExecuted() {
            //_authenticationService.Logout();
        }
    }
}

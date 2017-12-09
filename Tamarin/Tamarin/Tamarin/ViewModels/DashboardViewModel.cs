﻿using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamarin.Models;
using Tamarin.Services;
using Xamarin.Forms;

namespace Tamarin.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public DashboardViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Dashboard";

            LogoutCommand = new DelegateCommand(OnLogoutCommandExecuted);
            NavigateCommand = new DelegateCommand(OnNavigateExecuted);

            GetDashboard();
        }

        private DashboardModel _dashboardModel;
        public DashboardModel DashboardModel
        {
            get { return _dashboardModel; }
            set { SetProperty(ref _dashboardModel, value); }
        }

        private async void GetDashboard()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var response = await DashboardService.GetDashboard();
                var content = await response.Content.ReadAsStringAsync();
                var message = JsonConvert.DeserializeObject<DashboardModel>(content);
                DashboardModel = message;
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new ErrorMessageModel
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public DelegateCommand LogoutCommand { get; }
        public DelegateCommand NavigateCommand { get; }

        public async void OnNavigateExecuted()
        {
            await _navigationService.NavigateAsync("/Home/Navigation/Subject");
        }
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            //Message = parameters.GetValue<string>("message");
        }

        public void OnLogoutCommandExecuted() {
            //_authenticationService.Logout();
        }
    }
}

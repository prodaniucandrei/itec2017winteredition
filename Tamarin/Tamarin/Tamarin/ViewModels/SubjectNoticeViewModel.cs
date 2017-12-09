using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tamarin.ViewModels
{
    public class SubjectNoticeViewModel : BaseViewModel
    {
        public SubjectNoticeViewModel(INavigationService navigationService):base(navigationService)
        {
            AddCommand = new DelegateCommand(OnItemAdded);
        }

        public DelegateCommand AddCommand { get; set; }

        public async void OnItemAdded()
        {
            //add
        }
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
    }
}

using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tamarin.ViewModels
{
    public class SubjectViewModel : BaseViewModel
    {
        public SubjectViewModel(INavigationService navigationService):base(navigationService)
        {

        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
        }
    }
}

using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tamarin.ViewModels
{
    public class OrarViewModel : BaseViewModel
    {
        public OrarViewModel(INavigationService navigationService):base(navigationService)
        {
            Title = "Orar";
            AddCommand = new DelegateCommand(OnItemAdded);
        }

        public DelegateCommand AddCommand { get; set; }

        public async void OnItemAdded()
        {
            //add
        }
        public override void OnNavigatedTo(NavigationParameters parameters)
        {

        }
    }
}

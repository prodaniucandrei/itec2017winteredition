using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Tamarin.ViewModels
{
    public class ColegiViewModel : BaseViewModel
    {
        private int _index;
        public int Index
        {
            get { return _index; }
            set
            {
                SetProperty(ref _index, value);
            }
        }

        public ObservableCollection<object> Colegi { get; set; }
        public Command<object> ItemClickedCommand { get; }
        public ColegiViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Colegi";
            Colegi = new ObservableCollection<object>()
            {
                new { Nume="Andrei"},
                new { Nume="Ghita" }
            };
            ItemClickedCommand = new Command<object>(OnItemClicked);


        }

        private void OnItemClicked(object coleg)
        {
            Colegi.Add(new { Nume = "Ronaldo" });
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {

        }
    }
}

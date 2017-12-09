using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tamarin.Helpers;
using Tamarin.Models;
using Tamarin.Services;
using Xamarin.Forms;

namespace Tamarin.ViewModels
{
    public class OrarNeconfirmateViewModel : BaseViewModel
    {

        public ObservableRangeCollection<object> Materii { get; set; }
        public Command LoadItemsCommand { get; }
        public Command<object> ItemClickedCommand { get; }
        public DelegateCommand AddCommand { get; set; }

        public OrarNeconfirmateViewModel(INavigationService navigationService) : base(navigationService)
        {
            AddCommand = new DelegateCommand(OnItemAdded);
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemClickedCommand = new Command<object>(OnItemClicked);
        }

        private void OnItemClicked(object coleg)
        {
            //Colegi.Add(new { Nume = "Ronaldo" });
        }
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Materii.Clear();
                var response = await StudentService.GetAll();
                var content = await response.Content.ReadAsStringAsync();
                var message = JsonConvert.DeserializeObject<List<object>>(content);
                Materii.ReplaceRange(message);
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
        public async void OnItemAdded()
        {
            //add
        }
    }
}

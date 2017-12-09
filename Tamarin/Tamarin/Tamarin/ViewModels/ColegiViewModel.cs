using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Tamarin.Helpers;
using Tamarin.Models;
using Tamarin.Services;
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
        //public ImageSource UserImage { get; set; }
        public ObservableRangeCollection<StudentModel> Colegi { get; set; }
        public Command<StudentModel> ItemClickedCommand { get; }
        public Command LoadItemsCommand { get; }
        public ColegiViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Colegi";
            //UserImage = ImageSource.FromFile("user.png");
            ItemClickedCommand = new Command<StudentModel>(OnItemClicked);
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            Colegi = new ObservableRangeCollection<StudentModel>();
            if (Colegi.Count == 0)
            {
                LoadItemsCommand.Execute(null);
            }
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
                Colegi.Clear();
                var response = await StudentService.GetAll();
                var content = await response.Content.ReadAsStringAsync();
                var message = JsonConvert.DeserializeObject<List<StudentModel>>(content);
                Colegi.ReplaceRange(message);
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

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {

        }
    }
}

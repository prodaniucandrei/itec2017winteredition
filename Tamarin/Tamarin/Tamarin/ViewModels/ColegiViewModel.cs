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
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }
        public ObservableRangeCollection<StudentModel> Colegi { get; set; }
        public ObservableRangeCollection<StudentModel> ColegiUnfiltered { get; set; }
        public Command<StudentModel> ItemClickedCommand { get; }
        public Command LoadItemsCommand { get; }
        public Command SearchCommand { get; }
        public ColegiViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Colegi";
            ItemClickedCommand = new Command<StudentModel>(OnItemClicked);
            SearchCommand = new Command((text) => Search());
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            Colegi = new ObservableRangeCollection<StudentModel>();
            ColegiUnfiltered = new ObservableRangeCollection<StudentModel>();
            if (Colegi.Count == 0)
            {
                LoadItemsCommand.Execute(null);
            }
        }

        private void Search()
        {
            var t = SearchText.ToString();
            if (string.IsNullOrEmpty(t))
            {
                Colegi.ReplaceRange(ColegiUnfiltered);
            }
            else
            {
                var temp = ColegiUnfiltered.Where(a => a.Nume.ToLower().StartsWith(t.ToLower()));
                Colegi.ReplaceRange(temp);
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
                ColegiUnfiltered.Clear();
                var response = await StudentService.GetAll();
                var content = await response.Content.ReadAsStringAsync();
                var message = JsonConvert.DeserializeObject<List<StudentModel>>(content);
                ColegiUnfiltered.ReplaceRange(message);
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

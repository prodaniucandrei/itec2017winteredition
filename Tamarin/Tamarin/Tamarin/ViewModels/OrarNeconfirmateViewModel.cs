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
    public class OrarNeconfirmateViewModel : BaseViewModel
    {
        private bool _isBusyNeconfirmat;
        public bool IsBusyNeconfirmat
        {
            get { return _isBusyNeconfirmat; }
            set { SetProperty(ref _isBusyNeconfirmat, value); }
        }

        public ObservableRangeCollection<SubjectModel> Materiiv { get; set; }
        public Command LoadItemsCommand { get; }
        public Command<SubjectModel> ItemClickedCommand { get; }
        public DelegateCommand AddCommand { get; set; }

        public OrarNeconfirmateViewModel(INavigationService navigationService) : base(navigationService)
        {
            Materiiv = new ObservableRangeCollection<SubjectModel>();
            AddCommand = new DelegateCommand(OnItemAdded);
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemClickedCommand = new Command<SubjectModel>(OnItemClicked);

            if (Materiiv.Count == 0)
            {
                //LoadItemsCommand.Execute(null);
                Task.Run(() => this.ExecuteLoadItemsCommand()).Wait();
            }
        }

        private async void OnItemClicked(SubjectModel subject)
        {
            //Colegi.Add(new { Nume = "Ronaldo" });
            var response = await SubjectService.ConfirmSubject(subject);
            if (response.IsSuccessStatusCode)
            {
                Task.Run(() => this.ExecuteLoadItemsCommand()).Wait();
            }
        }
        public ObservableCollection<Grouping<string, SubjectModel>> Materii { get; set; }
        public void GroupItems()
        {
            var sorted = from monkey in Materiiv
                         orderby monkey.Zi
                         group monkey by monkey.Zi into monkeyGroup
                         select new Grouping<string, SubjectModel>(monkeyGroup.Key, monkeyGroup);

            //create a new collection of groups
            Materii= new ObservableCollection<Grouping<string, SubjectModel>>(sorted);
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusyNeconfirmat)
                return;

            IsBusyNeconfirmat = true;

            try
            {
                Materiiv.Clear();
                var response = await SubjectService.GetAll(false);
                var content = await response.Content.ReadAsStringAsync();
                var message = JsonConvert.DeserializeObject<List<SubjectModel>>(content);
                Materiiv.ReplaceRange(message);
                GroupItems();
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
                IsBusyNeconfirmat = false;
            }
        }
        public async void OnItemAdded()
        {
            await _navigationService.NavigateAsync("/Home/Navigation/Orar/AddNewSubject");
        }
    }
}

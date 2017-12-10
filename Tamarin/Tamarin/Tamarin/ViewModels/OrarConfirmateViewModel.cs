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
    public class OrarConfirmateViewModel : BaseViewModel
    {
        private bool _isBusyConfirmat;
        public bool IsBusyConfirmat
        {
            get { return _isBusyConfirmat; }
            set { SetProperty(ref _isBusyConfirmat, value); }
        }

        public ObservableCollection<Grouping<string, SubjectModel>> Materii { get; set; }
        public ObservableRangeCollection<SubjectModel> Materiiv { get; set; }
        public Command LoadItemsCommand { get; }
        public Command<StudentModel> ItemClickedCommand { get; }
        public OrarConfirmateViewModel(INavigationService navigationService) : base(navigationService)
        {
            Materiiv = new ObservableRangeCollection<SubjectModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemClickedCommand = new Command<StudentModel>(OnItemClicked);

            if (Materiiv.Count == 0)
            {
                //LoadItemsCommand.Execute(null);
                Task.Run(() => this.ExecuteLoadItemsCommand()).Wait();
            }
        }

        public void GroupItems()
        {
            var sorted = from monkey in Materiiv
                         orderby monkey.Zi
                         group monkey by monkey.Zi into monkeyGroup
                         select new Grouping<string, SubjectModel>(monkeyGroup.Key, monkeyGroup);

            //create a new collection of groups
            Materii = new ObservableCollection<Grouping<string, SubjectModel>>(sorted);
        }

        private void OnItemClicked(object coleg)
        {
            //Colegi.Add(new { Nume = "Ronaldo" });
        }
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusyConfirmat)
                return;

            IsBusyConfirmat = true;

            try
            {
                Materiiv.Clear();
                var response = await SubjectService.GetAll(true);
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
                IsBusyConfirmat = false;
            }
        }
    }
}

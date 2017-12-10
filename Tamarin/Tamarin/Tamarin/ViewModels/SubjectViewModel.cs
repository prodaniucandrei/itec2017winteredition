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
    public class SubjectViewModel : BaseViewModel
    {
        #region detailsprops
        private string _nume;
        public string Nume
        {
            get { return _nume; }
            set { SetProperty(ref _nume, value); }
        }

        private string _locatie;
        public string Locatie
        {
            get { return _locatie; }
            set { SetProperty(ref _locatie, value); }
        }

        private int _confirmations;
        public int Confirmations
        {
            get { return _confirmations; }
            set { SetProperty(ref _confirmations, value); }
        }

        private string _facultate;
        public string Facultate
        {
            get { return _facultate; }
            set { SetProperty(ref _facultate, value); }
        }

        private string _serie;
        public string Serie
        {
            get { return _serie; }
            set { SetProperty(ref _serie, value); }
        }

        private string _date;
        public string Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        private string _startTime;
        public string StartTime
        {
            get { return _startTime; }
            set { SetProperty(ref _startTime, value); }
        }

        private string _tip;
        public string Tip
        {
            get { return _tip; }
            set { SetProperty(ref _tip, value); }
        }
        #endregion

        #region prezentaprops
        private int _index;
        public int Index
        {
            get { return _index; }
            set { SetProperty(ref _index, value); }
        }
        #endregion

        StudentModel student;

        public ObservableRangeCollection<StudentModel> StudentiPrezenti { get; set; }
        public ObservableRangeCollection<NoteModel> Notite { get; set; }
        public SubjectViewModel(INavigationService navigationService) : base(navigationService)
        {
            AddCommand = new DelegateCommand(OnItemAdded);
            PrezentCommand = new DelegateCommand(OnPrezentSelected);
            StudentiPrezenti = new ObservableRangeCollection<StudentModel>();
            LoadItemsCommand = new DelegateCommand(GetNote);
            Notite = new ObservableRangeCollection<NoteModel>();

        }

        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand PrezentCommand { get; set; }
        public DelegateCommand LoadItemsCommand { get; set; }


        public async void OnPrezentSelected()
        {
            if (student == null)
            {
                var response = await StudentService.GetById();
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    student = JsonConvert.DeserializeObject<StudentModel>(content);
                }
            }
            if (student != null && !StudentiPrezenti.Any(a => a.Nume == student.Nume))
                StudentiPrezenti.Add(student);

        }

        public async void GetNote()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                var notResponse = await NotiteService.GetNotite(Subject.Id);
                if (notResponse.IsSuccessStatusCode)
                {
                    Notite.Clear();
                    var content = await notResponse.Content.ReadAsStringAsync();
                    var message = JsonConvert.DeserializeObject<List<NoteModel>>(content);
                    Notite.ReplaceRange(message);
                }
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
            //add notita
        }
        public override void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {

            SubjectModel subject = parameters["subject"] as SubjectModel;
            Subject = subject;
            PopulateDetailsProps(subject);
            GetNote();
            try
            {
                StudentiPrezenti.Clear();
                var response = await StudentService.GetAllBySubject(Subject.Id);
                var content = await response.Content.ReadAsStringAsync();
                var message = JsonConvert.DeserializeObject<List<StudentModel>>(content);
                StudentiPrezenti.ReplaceRange(message);
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

        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {

        }


        private void PopulateDetailsProps(SubjectModel subj)
        {
            this.Nume = subj.Nume;
            this.Locatie = subj.Locatie;
            this.Confirmations = subj.Confirmations;
            this.Facultate = subj.Facultate;
            this.Serie = subj.Serie;
            this.Date = GetDay(subj.Date);
            this.StartTime = subj.StartTime;
            this.Tip = subj.Tip.ToString();
        }

        private string GetDay(int date)
        {
            return new System.Globalization.CultureInfo("ro-RO").DateTimeFormat.GetDayName((DayOfWeek)(date - 1));
        }
    }
}

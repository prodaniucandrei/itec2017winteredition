using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamarin.Models;

namespace Tamarin.ViewModels
{
    public class AddNewSubjectViewModel : BaseViewModel
    {
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public AddNewSubjectViewModel(INavigationService navigationService): base(navigationService)
        {
            Title = "New Subject";
            AddCommand = new DelegateCommand(OnItemAdded);
            CancelCommand = new DelegateCommand(OnItemAdded);
        }

        private SubjectModel _subjectModel;
        public SubjectModel SubjectModel
        {
            get { return _subjectModel; }
            set { SetProperty(ref _subjectModel, value); }
        }

        public async void OnItemAdded()
        {
            //await _navigationService.NavigateAsync("Navigation/AddNewSubject");
        }

        public async void OnCancel()
        {
            //await _navigationService.NavigateAsync()
        }
    }
}
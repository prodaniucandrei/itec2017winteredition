using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamarin.Models;
using Tamarin.Services;

namespace Tamarin.ViewModels
{
    public class AddNewSubjectViewModel : BaseViewModel
    {
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public AddNewSubjectViewModel(INavigationService navigationService): base(navigationService)
        {
            Title = "New Subject";
            SubjectModel = new SubjectModel();
            AddCommand = new DelegateCommand(OnItemAdded);
            CancelCommand = new DelegateCommand(OnCancel);
        }

        private SubjectModel _subjectModel;
        public SubjectModel SubjectModel
        {
            get { return _subjectModel; }
            set { SetProperty(ref _subjectModel, value); }
        }

        public async void OnItemAdded()
        {
            var response = await SubjectService.Add(SubjectModel);
            if (response.IsSuccessStatusCode)
            {
            }
        }

        public async void OnCancel()
        {
            await _navigationService.GoBackAsync();
        }
    }
}
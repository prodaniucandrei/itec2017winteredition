using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAppApi.Models;

namespace UniversityAppApi.ViewModels
{
    public class ProfesorViewModel: BaseViewModel
    {
        public string Nume { get; set; }
        public string Email { get; set; }

        public ICollection<SubjectViewModel> Subjects { get; set; }
    }
}

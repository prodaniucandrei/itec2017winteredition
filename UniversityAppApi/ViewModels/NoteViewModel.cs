using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAppApi.Models;

namespace UniversityAppApi.ViewModels
{
    public class NoteViewModel: BaseViewModel
    {
        public string Titlu { get; set; }
        public string Body { get; set; }

        public int? StudentId { get; set; }
        public int? SubjectId { get; set; }
    }
}

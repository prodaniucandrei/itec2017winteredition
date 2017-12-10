using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using UniversityAppApi.Models;
using UniversityAppApi.Models.Enums;

namespace UniversityAppApi.ViewModels
{
    public class SubjectViewModel: BaseViewModel
    {
        public string Nume { get; set; }
        public string Locatie { get; set; }
        public int Confirmations { get; set; }
        public string Facultate { get; set; }
        public string Serie { get; set; }
        public int Date { get; set; }
        public string StartTime { get; set; }
        public SubjectTypeEnum Tip { get; set; }

        public int? ProfesorId { get; set; }

        public ProfesorModel Profesor { get; set; }
    }
}

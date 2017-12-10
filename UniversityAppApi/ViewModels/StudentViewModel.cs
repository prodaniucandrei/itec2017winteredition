using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using UniversityAppApi.Models;

namespace UniversityAppApi.ViewModels
{
    public class StudentViewModel: BaseViewModel
    {
        public string Nume { get; set; }
        public string Facultate { get; set; }
        public int An { get; set; }
        public string Sectie { get; set; }
        public string Grupa { get; set; }
        public string Subgrupa { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }

        public Collection<NoteModel> Notes { get; set; }
        public Collection<PrezentaModel> Prezente { get; set; }
    }
}

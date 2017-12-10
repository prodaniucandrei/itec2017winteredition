using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAppApi.Models
{
    [Table("Students")]
    public class StudentModel: BaseModel
    {
        public string UserId { get; set; }
        public string Nume { get; set; }
        public string Facultate { get; set; }
        public int An { get; set; }
        public string Sectie { get; set; }
        public string Grupa { get; set; }
        public string Subgrupa { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }

        [NotMapped]
        public ICollection<NoteModel> Notes { get; set; }
        [NotMapped]
        public ICollection<PrezentaModel> Prezente { get; set; }
    }
}

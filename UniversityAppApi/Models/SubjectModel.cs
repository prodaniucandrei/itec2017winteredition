using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UniversityAppApi.Models.Enums;

namespace UniversityAppApi.Models
{
    [Table("Subjects")]
    public class SubjectModel: BaseModel
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

        [NotMapped]
        public ProfesorModel Profesor { get; set; }
        [NotMapped]
        public ICollection<NoteModel> Notes { get; set; }
        [NotMapped]
        public ICollection<PrezentaModel> Prezente { get; set; }
    }
}

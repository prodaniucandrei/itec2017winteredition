using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAppApi.Models
{
    [Table("Profesors")]
    public class ProfesorModel: BaseModel
    {
        public string Nume { get; set; }
        public string Email { get; set; }

        [NotMapped]
        public ICollection<SubjectModel> Subjects { get; set; }
    }
}

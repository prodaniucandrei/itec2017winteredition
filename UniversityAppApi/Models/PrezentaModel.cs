using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAppApi.Models
{
    [Table("Prezenta")]
    public class PrezentaModel: BaseModel
    {
        public DateTime Data { get; set; }

        public int? StudentId { get; set; }
        public int? SubjectId { get; set; }

        [NotMapped]
        public StudentModel Student { get; set; }
        [NotMapped]
        public SubjectModel Subject { get; set; }
    }
}

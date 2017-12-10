using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamarin.Models
{
    public class NoteModel
    {
        public string Titlu { get; set; }
        public string Body { get; set; }
        public int? SubjectId { get; set; }
        public int? StudentId { get; set; }
    }
}

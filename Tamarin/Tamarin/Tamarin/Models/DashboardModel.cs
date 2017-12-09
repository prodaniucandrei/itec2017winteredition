using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamarin.Models
{
    public class DashboardModel
    {
        public StudentModel Student { get; set; }
        public List<SubjectModel> Subjects { get; set; }
    }
}

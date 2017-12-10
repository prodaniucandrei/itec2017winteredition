using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAppApi.ViewModels
{
    public class DashboardViewModel
    {
        public StudentViewModel Student { get; set; }
        public List<SubjectViewModel> Subjects { get; set; }
    }
}

using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamarin.ViewModels
{
    public class ScheduleViewModel: BaseViewModel
    {
        public ScheduleViewModel(INavigationService navigationService): base(navigationService)
        {
            Title = "Schedule";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamarin.Models;
using Tamarin.Models.Enum;
using Xamarin.Forms;

namespace Tamarin.Helpers
{
    class SubjectDataTemplateSelector: DataTemplateSelector
    {
        public DataTemplate CourseTemplate { get; set; }
        public DataTemplate LabTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((SubjectModel)item).Tip == SubjectTypeEnum.Curs ? CourseTemplate : LabTemplate;
        }
    }
}

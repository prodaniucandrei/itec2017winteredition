using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamarin.Models.Enum;

namespace Tamarin.Models
{
    public class SubjectModel
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Locatie { get; set; }
        public int Confirmations { get; set; }
        public string Facultate { get; set; }
        public string Serie { get; set; }
        public int Date { get; set; }
        public string StartTime { get; set; }
        public SubjectTypeEnum Tip { get; set; }
        public string Zi =>GetDay(this.Date);
        private static string GetDay(int date)
        {
            return new System.Globalization.CultureInfo("ro-RO").DateTimeFormat.GetDayName((DayOfWeek)(date - 1));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Views
{
    public class VwVacancy
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string  EndDate { get; set; }
        public string VacancyGroupName { get; set; }
        public bool Status { get; set; }
        public string StatusName { get; set; }
        public int QuestionCount { get; set; }
    }
}

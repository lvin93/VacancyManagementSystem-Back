using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Views
{
    public class VwCandidate
    {
        public int Id { get; set; }                    
        public string FullName { get; set; }          
        public string Fin { get; set; }                 
        public string Email { get; set; }      
        public string Phone { get; set; }               
        public int CorrectAnswerCount { get; set; }    
        public int WrongAnswerCount { get; set; }   
        public decimal Percentage { get; set; }
        public int? ResumeId { get; set; }
        public int VacancyId { get; set; }
        public string VacancyName { get; set; }
    }
}

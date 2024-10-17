using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Command.Candidate
{
    public class CreateCandidateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Fin { get; set; }
        public string Phone { get; set; }
        public int? VacancyId { get; set; }
    }
}

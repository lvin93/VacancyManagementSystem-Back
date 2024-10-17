using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Command.Candidate
{
    public class UpdateCandidateResumeDto
    {
        public int CandidateId { get; set; }
        public int VacancyId { get; set; }
        public IFormFile Resume { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Command.Answer
{
    public class AcceptAnswerDto
    {
        public int CandidateId { get; set; }
        public int AnswerOptionId { get; set; }
        public int QuestionId { get; set; }
        public int VacancyId { get; set; }
    }
}

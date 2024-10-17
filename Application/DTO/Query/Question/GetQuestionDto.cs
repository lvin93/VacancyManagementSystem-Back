using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Query.Question
{
    public class GetQuestionDto
    {
        public int Id { get; set; }
        public int? DifficultyLevel { get; set; }
        public string QuestionText { get; set; }
        public int VacancyId { get; set; }
    }
}

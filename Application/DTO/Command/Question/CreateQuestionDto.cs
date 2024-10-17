using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Command.Question
{
    public class CreateQuestionDto
    {
        public int? DifficultyLevel { get; set; }
        public string QuestionText { get; set; }
        public int VacancyId { get; set; }
    }
}

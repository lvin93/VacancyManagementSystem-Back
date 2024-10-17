using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Views
{
    public class VwCandidateAnswers
    {
        public int QuestionId { get; set; }
        public int CandidateId { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public bool? IsCandidateAnswer { get; set; }
    }
}

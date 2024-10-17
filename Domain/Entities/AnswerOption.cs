using Common.Entities;

namespace Domain.Entities
{
    public class AnswerOption : AuditableEntity
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public ICollection<CandidateAnswer> CandidateAnswers { get; set; }
    }
}

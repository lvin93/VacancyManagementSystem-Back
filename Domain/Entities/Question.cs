using Common.Entities;

namespace Domain.Entities
{
    public class Question : AuditableEntity
    {
        public int? DifficultyLevel { get; set; }
        public string QuestionText { get; set; }
        public int VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }
        public ICollection<AnswerOption> AnswerOptions { get; set; }
        public ICollection<CandidateAnswer> CandidateAnswers { get; set; }

    }
}

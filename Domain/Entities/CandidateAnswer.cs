using Common.Entities;

namespace Domain.Entities
{
    public class CandidateAnswer : AuditableEntity
    {
        public int CandidateId { get; set; }
        public int AnswerOptionId { get; set; }
        public int QuestionId { get; set; }
        public int VacancyId { get; set; }
        public AnswerOption AnswerOption { get; set; }
        public Candidate Candidate { get; set; }
        public Question Question { get; set; }
        public Vacancy Vacancy { get; set; }

    }
}

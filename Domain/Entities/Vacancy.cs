using Common.Entities;

namespace Domain.Entities
{
    public class Vacancy : AuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
        public int VacancyGroupId { get; set; }
        public int QuestionCount { get; set; }
        public VacancyGroup? VacancyGroup { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<CandidateVacancy> CandidateVacancies { get; set; }
        public ICollection<CandidateAnswer> CandidateAnswers { get; set; }

    }
}

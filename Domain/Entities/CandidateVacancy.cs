using Common.Entities;

namespace Domain.Entities
{
    public class CandidateVacancy: AuditableEntity
    {
        public int CandidateId { get; set; }
        public int VacancyId { get; set; }
        public DateTime ExamBeginDate { get; set; }
        public int? ResumeId { get; set; }
        public int CorrectAnswerCount { get; set; }
        public int WrongAnswerCount { get; set; }
        public File? Resume { get; set; }
        public Vacancy Vacancy { get; set; }
        public Candidate Candidate { get; set; }
    }
}

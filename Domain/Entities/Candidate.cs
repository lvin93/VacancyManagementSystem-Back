using Common.Entities;

namespace Domain.Entities
{
    public class Candidate : AuditableEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Fin { get; set; }
        public string Phone { get; set; }
        public ICollection<CandidateAnswer> CandidateAnswers { get; set; }
        public ICollection<CandidateVacancy> CandidateVacancies { get; set; }
    }
}

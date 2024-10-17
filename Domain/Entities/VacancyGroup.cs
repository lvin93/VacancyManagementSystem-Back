using Common.Entities;

namespace Domain.Entities
{
    public class VacancyGroup : AuditableEntity
    {
        public string VacancyGroupName { get; set; }
        public ICollection<Vacancy> Vacancies { get; set; }
    }
}

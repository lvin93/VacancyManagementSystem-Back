using Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IUnitOfWork : IDisposable
    {
        public IVacancyRepository Vacancy { get; }
        public IVacancyGroupRepository VacancyGroup { get; }
        public ICandidateRepository Candidate { get; }
        public IQuestionRepository Question { get; }
        public IAnswerRepository Answer { get; }
        public ICandidateAnswerRepository CandidateAnswer { get; }
        public ICandidateVacancyRepository CandidateVacancy { get; }
        public IReadonlyRepository ReadonlyView { get; }
        public IFileRepository File { get; }
        Task<int> SaveChangesAsync();
    }
}

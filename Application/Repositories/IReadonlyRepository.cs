using Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IReadonlyRepository
    {
        public Task<IQueryable<VwVacancy>> GetVacancies();
        public Task<IQueryable<VwCandidate>> GetCandidates();
        public Task<IQueryable<VwCandidateAnswers>> GetCandidateAnswers();
    }
}

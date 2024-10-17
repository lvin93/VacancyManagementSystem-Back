using Application.Repositories;
using Domain.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Concrete
{
    public class ReadonlyRepository : IReadonlyRepository
    {
        private readonly DbContext _context;

        public ReadonlyRepository(DbContext contex)
        {
            _context = contex;
        }

        public async Task<IQueryable<VwCandidateAnswers>> GetCandidateAnswers()
        {
            return _context.Set<VwCandidateAnswers>();
        }

        public async Task<IQueryable<VwCandidate>> GetCandidates()
        {
            return _context.Set<VwCandidate>();
        }

        public async Task<IQueryable<VwVacancy>> GetVacancies()
        {
            return _context.Set<VwVacancy>();
        }
    }
}

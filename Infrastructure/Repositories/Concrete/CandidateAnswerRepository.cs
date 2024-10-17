using Domain.Entities;
using Application.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Concrete
{
    public class CandidateAnswerRepository : Repository<CandidateAnswer>, ICandidateAnswerRepository
    {
        public CandidateAnswerRepository(VacancyManagementDbContext context) : base(context)
        {
        }
    }
}

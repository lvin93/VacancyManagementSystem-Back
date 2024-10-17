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
    public class AnswerRepository : Repository<AnswerOption>, IAnswerRepository
    {
        public AnswerRepository(VacancyManagementDbContext context) : base(context)
        {
        }
    }
}

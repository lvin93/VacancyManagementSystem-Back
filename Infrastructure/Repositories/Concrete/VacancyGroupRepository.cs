using Domain.Entities;
using Application.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Concrete
{
    public class VacancyGroupRepository :Repository<VacancyGroup>,IVacancyGroupRepository
    {
        public VacancyGroupRepository(VacancyManagementDbContext context) : base(context)
        {
        }
    }
}

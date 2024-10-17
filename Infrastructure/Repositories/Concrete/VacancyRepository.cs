﻿using Domain.Entities;
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
    public class VacancyRepository : Repository<Vacancy> , IVacancyRepository
    {
        public VacancyRepository(VacancyManagementDbContext context) : base(context)
        {
        }
    }
}

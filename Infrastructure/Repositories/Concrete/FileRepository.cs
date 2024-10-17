using Application.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Concrete
{
    public class FileRepository : Repository<Domain.Entities.File>, IFileRepository
    {
        public FileRepository(VacancyManagementDbContext context) : base(context)
        {
        }
    }
}

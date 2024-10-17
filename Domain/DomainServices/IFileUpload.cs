using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainServices
{
    public interface IFileUpload
    {
        public Task<Domain.Entities.File> UploadFile(IFormFile file, string environmentPath);
    }
}

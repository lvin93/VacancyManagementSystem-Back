using Application.DTO.Query.File;
using Domain.DomainServices;
using Infrastructure.CustomExceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExternalServices
{
    public class FileUploadService : IFileUpload
    {
        public async Task<Domain.Entities.File> UploadFile(IFormFile file, string environmentPath)
        {
            var extension = Path.GetExtension(file.FileName);

            if (!(extension == ".docx" || extension == ".pdf"))
                throw new FileException((int)HttpStatusCode.BadRequest, FileException.NotCorrectExtensionException);
            if (file.Length > 5242880)
                throw new FileException((int)HttpStatusCode.BadRequest, FileException.OverLengthException);

            GetFileDto response = new GetFileDto();
            var filename = Path.GetFileNameWithoutExtension(file.FileName);
            string folderName = "resumes";
            Domain.Entities.File uploadFile = new Domain.Entities.File()
            {
                Path = @$"/files/{folderName}/",
                FileName = Guid.NewGuid().ToString(),
                Extension = extension,
                FileType = file.ContentType.Split('/')[0],
                MimeType = file.ContentType,
                Size = file.Length,
                OriginalFileName = filename
            };
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            string path = environmentPath + @$"/files/{folderName}/";
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                File.WriteAllBytes(path + uploadFile.FileName + uploadFile.Extension, ms.ToArray());

            }
            catch (Exception exception)
            {
                throw new FileException((int)HttpStatusCode.BadRequest, FileException.UploadFileException);
            }

            return uploadFile;
        }
    }
}

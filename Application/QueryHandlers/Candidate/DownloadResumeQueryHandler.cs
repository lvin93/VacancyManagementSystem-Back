using Application.DTO.Query.Candidate;
using Application.QueryHandlers.Vacancy;
using AutoMapper;
using Common.Models;
using CSharpFunctionalExtensions;
using Common.Queries;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.QueryHandlers.Candidate
{
    public record DownloadResumeResponse(FileContentDto File);
    public record DownloadResumeQuery(int Id, string EnvironmentPah) : IQuery<DownloadResumeResponse>;

    public class DownloadResumeQueryHandler : IQueryHandler<DownloadResumeQuery, DownloadResumeResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DownloadResumeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<DownloadResumeResponse> Handle(DownloadResumeQuery request, CancellationToken cancellationToken)
        {
            FileContentDto dto = new FileContentDto();
            var file = await _unitOfWork.File.GetAsync(x => x.Id == request.Id);
            string path = request.EnvironmentPah + file.Path + file.FileName + file.Extension;

            dto.ContentType = MimeTypes.GetMimeType(path);
            dto.FileName = file.OriginalFileName;
            dto.FileContent = await File.ReadAllBytesAsync(path);
            return new DownloadResumeResponse(dto);
        }
    }
}

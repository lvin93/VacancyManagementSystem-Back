using Application.DTO.Command.Candidate;
using AutoMapper;
using Common.Models;
using CSharpFunctionalExtensions;
using Common.Commands;
using Domain.DomainServices;
using Microsoft.AspNetCore.Http;

namespace Application.CommandHandlers.Candidate
{
    public record UpdateCandidateResumeCommand(UpdateCandidateResumeDto CandidateDto, string EnvironmentPath) : ICommand<IResult<UpdateCandidateResumeResponse, DomainError>> { }

    public record UpdateCandidateResumeResponse(int Id);
    public class UpdateCandidateResumeCommandHandler : ICommandHandler<UpdateCandidateResumeCommand, IResult<UpdateCandidateResumeResponse, DomainError>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileUpload _fileUpload;

        public UpdateCandidateResumeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileUpload fileUpload)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileUpload = fileUpload;
        }
        public async Task<IResult<UpdateCandidateResumeResponse, DomainError>> Handle(UpdateCandidateResumeCommand request, CancellationToken cancellationToken)
        {
            var candidateVacancy = await _unitOfWork.CandidateVacancy.GetAsync(x => x.VacancyId == request.CandidateDto.VacancyId && x.CandidateId == request.CandidateDto.CandidateId);
            if (candidateVacancy != null)
            {
                var uploadFile = await _fileUpload.UploadFile(request.CandidateDto.Resume, request.EnvironmentPath);

                var addedFile = await _unitOfWork.File.AddAsync(uploadFile);
                await _unitOfWork.SaveChangesAsync();

                candidateVacancy.ResumeId = addedFile.Id;
                candidateVacancy.UpdatedAt = DateTime.Now;
                await _unitOfWork.CandidateVacancy.UpdateAsync(candidateVacancy);
                await _unitOfWork.SaveChangesAsync();

                return Result.Success<UpdateCandidateResumeResponse, DomainError>
                (
                  new UpdateCandidateResumeResponse(
                   addedFile.Id
                ));
            }
            else
                return Result.Failure<UpdateCandidateResumeResponse, DomainError>
                          (new DomainError
                          {
                              ErrorMessage = "Siz vakansiyaya müraciət etməmisiniz!",
                              InnerException = null,
                              StatusCode = StatusCodes.Status400BadRequest
                          });
        }
    }
}

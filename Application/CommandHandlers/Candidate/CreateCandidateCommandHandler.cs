using Application.DTO.Command.Candidate;
using AutoMapper;
using Common.Models;
using CSharpFunctionalExtensions;
using Common.Commands;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.CommandHandlers.Candidate
{
    public record CreateCandidateCommand(CreateCandidateDto CandidateDto) : ICommand<IResult<CreateCandidateResponse, DomainError>> { }

    public record CreateCandidateResponse(int Id);

    public class CandidateCommandHandler : ICommandHandler<CreateCandidateCommand, IResult<CreateCandidateResponse, DomainError>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CandidateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResult<CreateCandidateResponse, DomainError>> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            var existCandidate = await _unitOfWork.Candidate.GetAsync(x => x.Fin == request.CandidateDto.Fin);
            var vacancy = await _unitOfWork.Vacancy.GetAsync(x => x.Id == request.CandidateDto.VacancyId);
            var notEndedExamVacancy = existCandidate != null ? await _unitOfWork.CandidateVacancy
                .GetAsync(x => x.CandidateId == existCandidate.Id && x.ExamBeginDate < DateTime.Now && DateTime.Now < x.ExamBeginDate.AddMinutes(vacancy.QuestionCount * 1)) : null;
            var exsistCandidateVacancy = existCandidate != null ? await _unitOfWork.CandidateVacancy.GetAsync((x => x.VacancyId == request.CandidateDto.VacancyId && x.CandidateId == existCandidate.Id)) : null;
            int candidateId = existCandidate != null ? existCandidate.Id : 0;

            if (existCandidate is null)
            {
                var candidate = new Domain.Entities.Candidate();
                _mapper.Map(request.CandidateDto, candidate);
                var createdCandidate = await _unitOfWork.Candidate.AddAsync(candidate);
                await _unitOfWork.SaveChangesAsync();
                candidateId = createdCandidate.Id;
            }
            if (exsistCandidateVacancy is null)
            {
                if (notEndedExamVacancy != null)

                    return Result.Failure<CreateCandidateResponse, DomainError>
                          (new DomainError
                          {
                              ErrorMessage = "Hal-hazırki imtahan bitmədən digər vakansiyaya müraciət edə bilməzsiniz!",
                              InnerException = null,
                              StatusCode = StatusCodes.Status400BadRequest
                          });

                var candidateVacancy = new CandidateVacancy()
                {
                    CandidateId = candidateId,
                    VacancyId = (int)request.CandidateDto.VacancyId,
                    ExamBeginDate = DateTime.Now
                };
                await _unitOfWork.CandidateVacancy.AddAsync(candidateVacancy);
                await _unitOfWork.SaveChangesAsync();
            }
            else
                return Result.Failure<CreateCandidateResponse, DomainError>
                           (new DomainError
                           {
                               ErrorMessage = "Siz artıq müraciət etmisiniz!",
                               InnerException = null,
                               StatusCode = StatusCodes.Status400BadRequest
                           });


            return Result.Success<CreateCandidateResponse, DomainError>
                  (new CreateCandidateResponse(
                      candidateId
                  ));

        }
    }
}

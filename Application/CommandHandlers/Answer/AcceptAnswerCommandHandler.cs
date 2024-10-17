using Application.DTO.Command.Answer;
using AutoMapper;
using Common.Models;
using CSharpFunctionalExtensions;
using Common.Commands;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.CommandHandlers.Answer
{
    public record AcceptAnswerCommand(AcceptAnswerDto AcceptAnswer) : ICommand<IResult<AcceptAnswerResponse, DomainError>> { }

    public record AcceptAnswerResponse(int Id);

    public class AcceptAnswerCommandHandler : ICommandHandler<AcceptAnswerCommand, IResult<AcceptAnswerResponse, DomainError>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AcceptAnswerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResult<AcceptAnswerResponse, DomainError>> Handle(AcceptAnswerCommand request, CancellationToken cancellationToken)
        {

            var exsistAnswer = await _unitOfWork.CandidateAnswer.GetAsync(x => x.CandidateId == request.AcceptAnswer.CandidateId && x.QuestionId == request.AcceptAnswer.QuestionId);

            if (exsistAnswer is null)
            {
                var candidateVacancy = await _unitOfWork.CandidateVacancy.GetAsync(x => x.CandidateId == request.AcceptAnswer.CandidateId && x.VacancyId == request.AcceptAnswer.VacancyId);
                var vacancy = await _unitOfWork.Vacancy.GetAsync(x => x.Id == request.AcceptAnswer.VacancyId);
                var examBeginDate = (DateTime)candidateVacancy.ExamBeginDate;
                if (DateTime.Now < examBeginDate.AddMinutes(vacancy.QuestionCount * 1))
                {
                    var candidateAnswer = new CandidateAnswer();
                    _mapper.Map(request.AcceptAnswer, candidateAnswer);
                    var createdCandidateAnswer = await _unitOfWork.CandidateAnswer.AddAsync(candidateAnswer);

                    //add score
                    var correctAnswer = await _unitOfWork.Answer.GetAsync(x => x.QuestionId == request.AcceptAnswer.QuestionId && x.IsCorrect == true);
                    if (request.AcceptAnswer.AnswerOptionId == correctAnswer.Id)
                        candidateVacancy.CorrectAnswerCount += 1;
                    else
                        candidateVacancy.WrongAnswerCount += 1;

                    await _unitOfWork.CandidateVacancy.UpdateAsync(candidateVacancy);

                    await _unitOfWork.SaveChangesAsync();

                    return Result.Success<AcceptAnswerResponse, DomainError>
                          (new AcceptAnswerResponse(
                              createdCandidateAnswer.Id
                          ));
                }
                else
                {
                    return Result.Failure<AcceptAnswerResponse, DomainError>
                           (new DomainError
                           {
                               ErrorMessage = "İmtahan vaxtı artıq bitib!",
                               InnerException = null,
                               StatusCode = StatusCodes.Status400BadRequest
                           });
                }
            }
            else
                return Result.Failure<AcceptAnswerResponse, DomainError>
                         (new DomainError
                         {
                             ErrorMessage = "Siz bu sualı artıq cavablandırmısınız!",
                             InnerException = null,
                             StatusCode = StatusCodes.Status400BadRequest
                         });
        }
    }
}

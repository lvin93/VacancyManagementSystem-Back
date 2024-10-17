using Application.DTO.Query.Answer;
using Application.DTO.Query.Question;
using Application.QueryHandlers.Question;
using AutoMapper;
using Common.Models;
using CSharpFunctionalExtensions;
using Common.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.QueryHandlers.Answer
{
    public record GetAnswersByQuestionIdForUserResponse(IEnumerable<GetAnswersForUserDto> Answers);
    public record GetAnswersByQuestionIdForUserQuery(int QuestionId) : IQuery<IResult<GetAnswersByQuestionIdForUserResponse, DomainError>>;
    public class GetAnswersByQuestionIdForUserHandler : IQueryHandler<GetAnswersByQuestionIdForUserQuery, IResult<GetAnswersByQuestionIdForUserResponse, DomainError>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAnswersByQuestionIdForUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult<GetAnswersByQuestionIdForUserResponse, DomainError>> Handle(GetAnswersByQuestionIdForUserQuery request, CancellationToken cancellationToken)
        {
            var answersEntity = await _unitOfWork.Answer.GetAllAsync(x => x.QuestionId == request.QuestionId);
            var answers = new List<GetAnswersForUserDto>();
            _mapper.Map(answersEntity, answers);
            return Result.Success<GetAnswersByQuestionIdForUserResponse, DomainError>
               (new GetAnswersByQuestionIdForUserResponse(
                   answers
               )) ;
        }
    }
}

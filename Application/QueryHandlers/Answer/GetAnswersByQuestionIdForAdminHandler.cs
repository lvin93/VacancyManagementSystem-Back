using Application.DTO.Query.Answer;
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
    public record GetAnswersByQuestionIdForAdminResponse(IEnumerable<GetAnswersForAdminDto> Answers);
    public record GetAnswersByQuestionIdForAdminQuery(int QuestionId) : IQuery<IResult<GetAnswersByQuestionIdForAdminResponse, DomainError>>;
    public class GetAnswersByQuestionIdForAdminHandler : IQueryHandler<GetAnswersByQuestionIdForAdminQuery, IResult<GetAnswersByQuestionIdForAdminResponse, DomainError>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAnswersByQuestionIdForAdminHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult<GetAnswersByQuestionIdForAdminResponse, DomainError>> Handle(GetAnswersByQuestionIdForAdminQuery request, CancellationToken cancellationToken)
        {
            var answersEntity = await _unitOfWork.Answer.GetAllAsync(x => x.QuestionId == request.QuestionId);
            var answers = new List<GetAnswersForAdminDto>();
            _mapper.Map(answersEntity, answers);
            return Result.Success<GetAnswersByQuestionIdForAdminResponse, DomainError>
               (new GetAnswersByQuestionIdForAdminResponse(
                   answers
               ));
        }
    }
}

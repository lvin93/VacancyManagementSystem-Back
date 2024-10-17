using AutoMapper;
using Common.Models;
using Common.Queries;
using CSharpFunctionalExtensions;
using Domain.Views;

namespace Application.QueryHandlers.Answer
{
    public record GetAnswerByQuestionIdAndCandidateIdResponse(IEnumerable<VwCandidateAnswers> Answers);
    public record GetAnswerByQuestionIdAndCandidateIdQuery(int QuestionId, int CandidateId) : IQuery<IResult<GetAnswerByQuestionIdAndCandidateIdResponse, DomainError>>;

    public class GetAnswerByQuestionIdAndCandidateIdHandler : IQueryHandler<GetAnswerByQuestionIdAndCandidateIdQuery, IResult<GetAnswerByQuestionIdAndCandidateIdResponse, DomainError>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAnswerByQuestionIdAndCandidateIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult<GetAnswerByQuestionIdAndCandidateIdResponse, DomainError>> Handle(GetAnswerByQuestionIdAndCandidateIdQuery request, CancellationToken cancellationToken)
        {
            var answers = await _unitOfWork.ReadonlyView.GetCandidateAnswers();
            var filteredAnswer = answers.Where(x => x.CandidateId == request.CandidateId && x.QuestionId == request.QuestionId);
         
            return Result.Success<GetAnswerByQuestionIdAndCandidateIdResponse, DomainError>
               (new GetAnswerByQuestionIdAndCandidateIdResponse(
                   filteredAnswer
               ));
        }
    }
}

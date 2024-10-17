using Application.DTO.Query.Question;
using AutoMapper;
using Common.Models;
using CSharpFunctionalExtensions;
using Common.Queries;

namespace Application.QueryHandlers.Question
{
    public record GetQuestionsByVacancyResponse(IEnumerable<GetQuestionDto> Questions);
    public record GetQuestionsByVacancyQuery(int VacancyId) : IQuery<IResult<GetQuestionsByVacancyResponse, DomainError>>;

    public class GetQuestionsByVacancyIdHandler : IQueryHandler<GetQuestionsByVacancyQuery, IResult<GetQuestionsByVacancyResponse, DomainError>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetQuestionsByVacancyIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult<GetQuestionsByVacancyResponse, DomainError>> Handle(GetQuestionsByVacancyQuery request, CancellationToken cancellationToken)
        {
            var vacancy = await _unitOfWork.Vacancy.GetAsync(x => x.Id == request.VacancyId);
            var questionsEntity = await _unitOfWork.Question.GetAllAsync(x => x.VacancyId == request.VacancyId);
            Random random = new Random();
            var randomQuestion = questionsEntity.AsEnumerable().OrderBy(q => Guid.NewGuid()).Take(vacancy.QuestionCount).ToList();
            var questions = new List<GetQuestionDto>();
            _mapper.Map(randomQuestion, questions);
            return Result.Success<GetQuestionsByVacancyResponse, DomainError>
               (new GetQuestionsByVacancyResponse(
                   questions
               ));
        }
    }
}

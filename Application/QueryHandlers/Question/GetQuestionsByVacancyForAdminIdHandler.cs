using Application.DTO.Query.Question;
using AutoMapper;
using Common.Models;
using CSharpFunctionalExtensions;
using Common.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.QueryHandlers.Question
{
    public record GetQuestionsByVacancyForAdminResponse(IEnumerable<GetQuestionDto> Questions);
    public record GetQuestionsByVacancyForAdminQuery(int VacancyId) : IQuery<IResult<GetQuestionsByVacancyForAdminResponse, DomainError>>;

    public class GetQuestionsByVacancyForAdminIdHandler : IQueryHandler<GetQuestionsByVacancyForAdminQuery, IResult<GetQuestionsByVacancyForAdminResponse, DomainError>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetQuestionsByVacancyForAdminIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult<GetQuestionsByVacancyForAdminResponse, DomainError>> Handle(GetQuestionsByVacancyForAdminQuery request, CancellationToken cancellationToken)
        {
            var vacancy = await _unitOfWork.Vacancy.GetAsync(x => x.Id == request.VacancyId);
            var questionsEntity = await _unitOfWork.Question.GetAllAsync(x => x.VacancyId == request.VacancyId);
            var questions = new List<GetQuestionDto>();
            _mapper.Map(questionsEntity, questions);
            return Result.Success<GetQuestionsByVacancyForAdminResponse, DomainError>
               (new GetQuestionsByVacancyForAdminResponse(
                   questions
               ));
        }
    }
}

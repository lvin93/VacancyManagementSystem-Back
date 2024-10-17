using Application.DTO.Query.Vacancy;
using Application.QueryHandlers.Answer;
using AutoMapper;
using Common.Models;
using CSharpFunctionalExtensions;
using Common.Queries;
using Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.QueryHandlers.Vacancy
{
    public record GetAllVacanciesResponse(IEnumerable<VwVacancy> Vacancies);
    public record GetAllVacanciesQuery() : IQuery<IResult<GetAllVacanciesResponse, DomainError>>;

    public class GetVacanciesQueryHandler : IQueryHandler<GetAllVacanciesQuery, IResult<GetAllVacanciesResponse, DomainError>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetVacanciesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult<GetAllVacanciesResponse, DomainError>> Handle(GetAllVacanciesQuery request, CancellationToken cancellationToken)
        {
            var vacancies = await _unitOfWork.ReadonlyView.GetVacancies();
            return Result.Success<GetAllVacanciesResponse, DomainError>
               (new GetAllVacanciesResponse(
                   vacancies.ToList()
               ));
        }
    }
}


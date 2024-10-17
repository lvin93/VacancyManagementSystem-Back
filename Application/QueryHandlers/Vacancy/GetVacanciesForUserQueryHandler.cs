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
    public record GetAllVacanciesForUserResponse(IEnumerable<VwVacancy> VacanciesForUser);
    public record GetAllVacanciesForUserQuery() : IQuery<IResult<GetAllVacanciesForUserResponse, DomainError>>;

    public class GetVacanciesForUserQueryHandler : IQueryHandler<GetAllVacanciesForUserQuery, IResult<GetAllVacanciesForUserResponse, DomainError>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetVacanciesForUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult<GetAllVacanciesForUserResponse, DomainError>> Handle(GetAllVacanciesForUserQuery request, CancellationToken cancellationToken)
        {
            var VacanciesForUser = await _unitOfWork.ReadonlyView.GetVacancies();
            return Result.Success<GetAllVacanciesForUserResponse, DomainError>
               (new GetAllVacanciesForUserResponse(
                   VacanciesForUser.Where(x => x.Status == true)
               ));
        }
    }
}

using Application.DTO.Query.Vacancy;
using AutoMapper;
using Common.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.QueryHandlers.Vacancy
{
    public record GetVacancyResponse(GetVacancyDto Vacancy);
    public record GetVacancyQuery(int Id) : IQuery<GetVacancyResponse>;

    public class GetVacancyQueryHandler : IQueryHandler<GetVacancyQuery, GetVacancyResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetVacancyQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetVacancyResponse> Handle(GetVacancyQuery request, CancellationToken cancellationToken)
        {
            var vacancyEntity = await _unitOfWork.Vacancy.GetAsync(x => x.Id == request.Id);
            var vacancy = new GetVacancyDto();
            _mapper.Map(vacancyEntity, vacancy);
            return new GetVacancyResponse(vacancy);
        }
    }
}

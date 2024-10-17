using Application.DTO.Query.VacancyGroup;
using AutoMapper;
using Common.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.QueryHandlers.VacancyGroup
{
    public class GetAllVacancyGroupQueryHandler
    {
        public record GetVacancyGroupsResponse(IEnumerable<GetVacancyGroupDto> VacancyGroups);
        public record GetVacancyGroupsQuery() : IQuery<GetVacancyGroupsResponse>;

        public class GetVacancyGroupsQueryHandler : IQueryHandler<GetVacancyGroupsQuery, GetVacancyGroupsResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public GetVacancyGroupsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<GetVacancyGroupsResponse> Handle(GetVacancyGroupsQuery request, CancellationToken cancellationToken)
            {
                var vacancyGroupEntity = await _unitOfWork.VacancyGroup.GetAllAsync();
                var vacancyGroups = new List<GetVacancyGroupDto>();
                _mapper.Map(vacancyGroupEntity, vacancyGroups);
                return new GetVacancyGroupsResponse(vacancyGroups);
            }
        }
    }
}

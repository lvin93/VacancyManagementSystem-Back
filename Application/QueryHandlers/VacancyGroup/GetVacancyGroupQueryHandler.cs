using Application.DTO.Query.VacancyGroup;
using AutoMapper;
using Common.Queries;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.QueryHandlers.VacancyGroup
{
    public record GetVacancyGroupResponse(GetVacancyGroupDto VacancyGroup);
    public record GetVacancyGroupQuery(int Id) : IQuery<GetVacancyGroupResponse>;

    public class GetVacancyGroupQueryHandler : IQueryHandler<GetVacancyGroupQuery, GetVacancyGroupResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetVacancyGroupQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetVacancyGroupResponse> Handle(GetVacancyGroupQuery request, CancellationToken cancellationToken)
        {
            var vacancyGroupEntity = await _unitOfWork.VacancyGroup.GetAsync(x => x.Id == request.Id);
            var vacancyGroup = new GetVacancyGroupDto();
            _mapper.Map(vacancyGroupEntity, vacancyGroup);
            return new GetVacancyGroupResponse(vacancyGroup);
        }
    }
}

using Application.DTO.Command.VacancyGroup;
using AutoMapper;
using Common.Commands;

namespace Application.CommandHandlers.VacancyGroup
{
    public class CreateVacancyGroupCommandHandler
    {
        public record CreateVacancyGroupCommand(CreateVacancyGroupDto VacancyGroupDto) : ICommand<CreateVacancyGroupResponse> { }

        public record CreateVacancyGroupResponse(int Id);

        public class VacancyGroupCommandHandler : ICommandHandler<CreateVacancyGroupCommand, CreateVacancyGroupResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public VacancyGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<CreateVacancyGroupResponse> Handle(CreateVacancyGroupCommand request, CancellationToken cancellationToken)
            {
                var vacancyGroup = new Domain.Entities.VacancyGroup();
                _mapper.Map(request.VacancyGroupDto, vacancyGroup);
                var createdVacancyGroup = await _unitOfWork.VacancyGroup.AddAsync(vacancyGroup);
                await _unitOfWork.SaveChangesAsync();
                return new CreateVacancyGroupResponse(createdVacancyGroup.Id);
            }
        }
    }
}

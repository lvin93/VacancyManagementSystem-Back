using Application.DTO.Command.VacancyGroup;
using AutoMapper;
using Common.Commands;

namespace Application.CommandHandlers.VacancyGroup
{
    public record UpdateVacancyGroupCommand(UpdateVacancyGroupDto VacancyGroupDto) : ICommand<UpdateVacancyGroupResponse> { }

    public record UpdateVacancyGroupResponse(int Id);

    public class UpdateVacancyGroupCommandHandler : ICommandHandler<UpdateVacancyGroupCommand, UpdateVacancyGroupResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateVacancyGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UpdateVacancyGroupResponse> Handle(UpdateVacancyGroupCommand request, CancellationToken cancellationToken)
        {
            var vacancyGroup = new Domain.Entities.VacancyGroup();
            _mapper.Map(request.VacancyGroupDto, vacancyGroup);
            vacancyGroup.UpdatedAt = DateTime.Now;
            var updatedVacancyGroup = await _unitOfWork.VacancyGroup.UpdateAsync(vacancyGroup);
            await _unitOfWork.SaveChangesAsync();
            return new UpdateVacancyGroupResponse(updatedVacancyGroup.Id);
        }
    }
}

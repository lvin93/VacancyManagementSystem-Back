using AutoMapper;
using Common.Commands;

namespace Application.CommandHandlers.VacancyGroup
{

    public record DeleteVacancyGroupCommand(int Id) : ICommand<DeleteVacancyGroupResponse> { }

    public record DeleteVacancyGroupResponse(int Id);

    public class DeleteVacancyGroupCommandHandler : ICommandHandler<DeleteVacancyGroupCommand, DeleteVacancyGroupResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteVacancyGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DeleteVacancyGroupResponse> Handle(DeleteVacancyGroupCommand request, CancellationToken cancellationToken)
        {
            var vacancy = await _unitOfWork.VacancyGroup.GetAsync(x => x.Id == request.Id);
            var deletedVacancyGroup = await _unitOfWork.VacancyGroup.DeleteAsync(vacancy);
            await _unitOfWork.SaveChangesAsync();
            return new DeleteVacancyGroupResponse(deletedVacancyGroup.Id);
        }
    }
}

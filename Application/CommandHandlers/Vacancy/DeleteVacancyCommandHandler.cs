using AutoMapper;
using Common.Commands;

namespace Application.CommandHandlers.Vacancy
{
    public record DeleteVacancyCommand(int Id) : ICommand<DeleteVacancyResponse> { }

    public record DeleteVacancyResponse(int Id);

    public class DeleteVacancyCommandHandler : ICommandHandler<DeleteVacancyCommand, DeleteVacancyResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteVacancyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DeleteVacancyResponse> Handle(DeleteVacancyCommand request, CancellationToken cancellationToken)
        {var vacancy=await _unitOfWork.Vacancy.GetAsync(x=>x.Id==request.Id);
            var deletedVacancy = await _unitOfWork.Vacancy.DeleteAsync(vacancy);
            await _unitOfWork.SaveChangesAsync();
            return new DeleteVacancyResponse(deletedVacancy.Id);
        }
    }
}

using Application.DTO.Command.Vacancy;
using AutoMapper;
using Common.Commands;

namespace Application.CommandHandlers.Vacancy
{

    public record UpdateVacancyCommand(UpdateVacancyDto VacancyDto) : ICommand<UpdateVacancyResponse> { }

    public record UpdateVacancyResponse(int Id);

    public class UpdateVacancyCommandHandler : ICommandHandler<UpdateVacancyCommand, UpdateVacancyResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateVacancyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UpdateVacancyResponse> Handle(UpdateVacancyCommand request, CancellationToken cancellationToken)
        {
            var vacancy = new Domain.Entities.Vacancy();
            _mapper.Map(request.VacancyDto, vacancy);
            vacancy.UpdatedAt = DateTime.Now;
            var updatedVacancy = await _unitOfWork.Vacancy.UpdateAsync(vacancy);
            await _unitOfWork.SaveChangesAsync();
            return new UpdateVacancyResponse(updatedVacancy.Id);
        }
    }
}


using Application.DTO.Command.Vacancy;
using AutoMapper;
using Common.Commands;

namespace Application.CommandHandlers.Vacancy
{
    public record CreateVacancyCommand(CreateVacancyDto VacancyDto) : ICommand<CreateVacancyResponse> { }

    public record CreateVacancyResponse(int Id);

    public class CreateVacancyCommandHandler : ICommandHandler<CreateVacancyCommand, CreateVacancyResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateVacancyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreateVacancyResponse> Handle(CreateVacancyCommand request, CancellationToken cancellationToken)
        {
            var vacancy = new Domain.Entities.Vacancy();
            _mapper.Map(request.VacancyDto,vacancy);
            var createdVacancy = await _unitOfWork.Vacancy.AddAsync(vacancy);
            await _unitOfWork.SaveChangesAsync();
            return new CreateVacancyResponse(createdVacancy.Id);
        }
    }
}


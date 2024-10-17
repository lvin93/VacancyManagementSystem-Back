using Application.DTO.Command.Candidate;
using AutoMapper;
using Common.Commands;

namespace Application.CommandHandlers.Candidate
{
    public record UpdateCandidateCommand(UpdateCandidateDto CandidateDto) : ICommand<UpdateCandidateResponse> { }

    public record UpdateCandidateResponse(int Id);

    public class UpdateCandidateCommandHandler : ICommandHandler<UpdateCandidateCommand, UpdateCandidateResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateCandidateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UpdateCandidateResponse> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = new Domain.Entities.Candidate();
            _mapper.Map(request.CandidateDto, candidate);
            candidate.UpdatedAt = DateTime.Now;
            var updatedCandidate = await _unitOfWork.Candidate.UpdateAsync(candidate);
            await _unitOfWork.SaveChangesAsync();
            return new UpdateCandidateResponse(updatedCandidate.Id);
        }
    }
}

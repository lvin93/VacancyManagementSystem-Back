using AutoMapper;
using Common.Commands;

namespace Application.CommandHandlers.Candidate
{
    public record DeleteCandidateCommand(int Id) : ICommand<DeleteCandidateResponse> { }

    public record DeleteCandidateResponse(int Id);

    public class DeleteCandidateCommandHandler : ICommandHandler<DeleteCandidateCommand, DeleteCandidateResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteCandidateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DeleteCandidateResponse> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _unitOfWork.Candidate.GetAsync(x => x.Id == request.Id);
            var deletedCandidate = await _unitOfWork.Candidate.DeleteAsync(candidate);
            await _unitOfWork.SaveChangesAsync();
            return new DeleteCandidateResponse(deletedCandidate.Id);
        }
    }
}

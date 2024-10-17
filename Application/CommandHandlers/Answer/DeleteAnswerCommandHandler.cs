using Application.CommandHandlers.Candidate;
using AutoMapper;
using Common.Commands;
using Common.Models;
using CSharpFunctionalExtensions;

namespace Application.CommandHandlers.Answer
{
    public record DeleteAnswerCommand(int Id) : ICommand<IResult<DeleteAnswerResponse, DomainError>> { }

    public record DeleteAnswerResponse(int Id);

    public class DeleteAnswerCommandHandler : ICommandHandler<DeleteAnswerCommand, IResult<DeleteAnswerResponse, DomainError>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteAnswerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResult<DeleteAnswerResponse, DomainError>> Handle(DeleteAnswerCommand request, CancellationToken cancellationToken)
        {
            var Answer = await _unitOfWork.Answer.GetAsync(x => x.Id == request.Id);
            var deletedAnswer = await _unitOfWork.Answer.DeleteAsync(Answer);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success<DeleteAnswerResponse, DomainError>
                          (new DeleteAnswerResponse(
                              deletedAnswer.Id
                          ));

        }
    }
}

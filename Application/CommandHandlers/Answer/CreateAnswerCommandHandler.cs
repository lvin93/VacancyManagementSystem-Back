using Application.DTO.Command.Answer;
using AutoMapper;
using Common.Models;
using CSharpFunctionalExtensions;
using Common.Commands;

namespace Application.CommandHandlers.Answer
{
    public record CreateAnswerCommand(CreateAnswerDto AnswerDto) : ICommand<IResult<CreateAnswerResponse, DomainError>> { }

    public record CreateAnswerResponse(int Id);

    public class CreateAnswerCommandHandler : ICommandHandler<CreateAnswerCommand, IResult<CreateAnswerResponse, DomainError>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateAnswerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResult<CreateAnswerResponse, DomainError>> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            var answer = new Domain.Entities.AnswerOption();
            _mapper.Map(request.AnswerDto, answer);
            var createdAnswer = await _unitOfWork.Answer.AddAsync(answer);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success<CreateAnswerResponse, DomainError>
                (new CreateAnswerResponse(
                    createdAnswer.Id
                ));
        }
    }
}

using Application.DTO.Command.Answer;
using AutoMapper;
using Common.Models;
using CSharpFunctionalExtensions;
using Common.Commands;
using Domain.Entities;

namespace Application.CommandHandlers.Answer
{
    public record UpdateAnswerCommand(UpdateAnswerDto AnswerDto) : ICommand<IResult<UpdateAnswerResponse, DomainError>> { }

    public record UpdateAnswerResponse(int Id);

    public class UpdateAnswerCommandHandler : ICommandHandler<UpdateAnswerCommand, IResult<UpdateAnswerResponse, DomainError>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateAnswerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResult<UpdateAnswerResponse, DomainError>> Handle(UpdateAnswerCommand request, CancellationToken cancellationToken)
        {
            var answer = new AnswerOption();
            _mapper.Map(request.AnswerDto, answer);
            answer.UpdatedAt = DateTime.Now;
            var updatedAnswer = await _unitOfWork.Answer.UpdateAsync(answer);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success<UpdateAnswerResponse, DomainError>
                 (new UpdateAnswerResponse(
                     updatedAnswer.Id
                 ));
        }
    }
}

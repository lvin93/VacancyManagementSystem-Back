using Application.DTO.Command.Question;
using AutoMapper;
using Common.Models;
using CSharpFunctionalExtensions;
using Common.Commands;

namespace Application.CommandHandlers.Question
{
    public record CreateQuestionCommand(CreateQuestionDto QuestionDto) : ICommand<IResult<CreateQuestionResponse, DomainError>> { }

    public record CreateQuestionResponse(int Id);

    public class CreateQuestionCommandHandler : ICommandHandler<CreateQuestionCommand, IResult<CreateQuestionResponse, DomainError>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateQuestionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResult<CreateQuestionResponse, DomainError>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = new Domain.Entities.Question();
            _mapper.Map(request.QuestionDto, question);
            var createdQuestion = await _unitOfWork.Question.AddAsync(question);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success<CreateQuestionResponse, DomainError>
                (new CreateQuestionResponse(
                    createdQuestion.Id
                ));
        }
    }
}

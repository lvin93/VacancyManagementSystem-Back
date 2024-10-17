using Application.DTO.Command.Question;
using AutoMapper;
using Common.Models;
using CSharpFunctionalExtensions;
using Common.Commands;

namespace Application.CommandHandlers.Question
{
    public record UpdateQuestionCommand(UpdateQuestionDto QuestionDto) : ICommand<IResult<UpdateQuestionResponse,DomainError>> { }

    public record UpdateQuestionResponse(int Id);

    public class UpdateQuestionCommandHandler : ICommandHandler<UpdateQuestionCommand, IResult<UpdateQuestionResponse, DomainError>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateQuestionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResult<UpdateQuestionResponse, DomainError>> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = new Domain.Entities.Question();
            _mapper.Map(request.QuestionDto, question);
            question.UpdatedAt = DateTime.Now;
            var updatedQuestion = await _unitOfWork.Question.UpdateAsync(question);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success<UpdateQuestionResponse, DomainError>
                 (new UpdateQuestionResponse(
                     updatedQuestion.Id
                 ));
        }
    }
}

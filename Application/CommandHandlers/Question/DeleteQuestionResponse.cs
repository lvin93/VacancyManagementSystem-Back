using AutoMapper;
using Common.Commands;
using Common.Models;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandHandlers.Question
{
    public record DeleteQuestionCommand(int Id) : ICommand<IResult<DeleteQuestionResponse, DomainError>> { }

    public record DeleteQuestionResponse(int Id);

    public class DeleteQuestionCommandHandler : ICommandHandler<DeleteQuestionCommand, IResult<DeleteQuestionResponse, DomainError>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteQuestionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResult<DeleteQuestionResponse, DomainError>> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var Question = await _unitOfWork.Question.GetAsync(x => x.Id == request.Id);
            var deletedQuestion = await _unitOfWork.Question.DeleteAsync(Question);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success<DeleteQuestionResponse, DomainError>
                          (new DeleteQuestionResponse(
                              deletedQuestion.Id
                          ));

        }
    }
}

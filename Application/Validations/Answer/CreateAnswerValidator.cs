using Application.CommandHandlers.Answer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations.Answer
{
    public class CreateAnswerValidator : AbstractValidator<CreateAnswerCommand>
    {
        public CreateAnswerValidator()
        {
            RuleFor(x => x.AnswerDto.AnswerText).NotEmpty().NotNull().WithMessage("Cavab mətni xanası məcburidir!"); ;
            RuleFor(x => x.AnswerDto.IsCorrect).NotNull().WithMessage("Düzgün cavab olub-olmadığı xanası məcburidir!"); ;
            RuleFor(x => x.AnswerDto.QuestionId).NotEmpty().NotNull().WithMessage("Sual seçilməyib!");
        }
    }
}

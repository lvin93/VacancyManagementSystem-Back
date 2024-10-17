using Application.CommandHandlers.Answer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations.Answer
{
    public class AcceptAnswerValidator : AbstractValidator<AcceptAnswerCommand>
    {
        public AcceptAnswerValidator() {
            RuleFor(x => x.AcceptAnswer.QuestionId).NotEmpty().NotNull().WithMessage("Sual seçilməyib!"); ;
            RuleFor(x => x.AcceptAnswer.AnswerOptionId).NotNull().WithMessage("Düzgün cavab seçilməyib!"); ;
        }
    }
}

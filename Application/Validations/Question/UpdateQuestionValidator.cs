using Application.CommandHandlers.Question;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations.Question
{
    public class UpdateQuestionValidator : AbstractValidator<UpdateQuestionCommand>
    {
        public UpdateQuestionValidator()
        {
            RuleFor(question => question.QuestionDto.DifficultyLevel)
                 .NotNull().WithMessage("Çətinlik səviyyəsi boş olmamalıdır.")
                 .InclusiveBetween(1, 5).WithMessage("Çətinlik səviyyəsi 1-dən 5-ə qədər olmalıdır.");

            RuleFor(question => question.QuestionDto.QuestionText)
                .NotEmpty().WithMessage("Sual mətni boş olmamalıdır.");
        }
    }
}

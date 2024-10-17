using Application.CommandHandlers.Vacancy;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations.Vacancy
{
    public class CreateVacancyValidator : AbstractValidator<CreateVacancyCommand>
    {
        public CreateVacancyValidator()
        {
            RuleFor(vacancy => vacancy.VacancyDto.Title)
               .NotEmpty().NotNull().WithMessage("Vakansiya adı boş olmamalıdır.");

            RuleFor(vacancy => vacancy.VacancyDto.Description)
                .NotEmpty().WithMessage("Təsvir boş olmamalıdır.");

            RuleFor(vacancy => vacancy.VacancyDto.QuestionCount)
                .GreaterThan(0).WithMessage("Sual sayı 0-dan böyük olmalıdır.");

            RuleFor(vacancy => vacancy.VacancyDto.StartDate)
                .NotEmpty().WithMessage("Başlama tarixi boş olmamalıdır.")
                .LessThan(vacancy => vacancy.VacancyDto.EndDate).WithMessage("Başlama tarixi bitmə tarixindən əvvəl olmalıdır.");

            RuleFor(vacancy => vacancy.VacancyDto.EndDate)
                .NotEmpty().WithMessage("Bitmə tarixi boş olmamalıdır.")
                .GreaterThan(vacancy => vacancy.VacancyDto.StartDate).WithMessage("Bitmə tarixi başlama tarixindən sonra olmalıdır.");

            RuleFor(vacancy => vacancy.VacancyDto.VacancyGroupId).NotNull().NotEqual(0).WithMessage("Vakansiya seçilməyib!");
        }
    }
}

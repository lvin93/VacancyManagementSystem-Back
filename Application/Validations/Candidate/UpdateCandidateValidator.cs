using Application.CommandHandlers.Candidate;
using FluentValidation;

namespace Application.Validations.Candidate
{
    public class UpdateCandidateValidator : AbstractValidator<UpdateCandidateCommand>
    {
        public UpdateCandidateValidator()
        {
            RuleFor(x => x.CandidateDto.Name)
           .NotEmpty().WithMessage("Ad xanası məcburidir!");

            RuleFor(x => x.CandidateDto.Surname)
                .NotEmpty().WithMessage("Soyad xanası məcburidir!");

            RuleFor(x => x.CandidateDto.Email)
                .NotEmpty().WithMessage("Email xanası məcburidir!")
                .EmailAddress().WithMessage("Email formatı düzgün deyil!");

            RuleFor(x => x.CandidateDto.Fin)
                .NotEmpty().WithMessage("Fin xanası məcburidir")
                .MaximumLength(7).WithMessage("Fin formatı düzgün deyil!");

            RuleFor(x => x.CandidateDto.Phone)
                .NotEmpty().WithMessage("Telefon nömrəsi xanası məcburidir")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Telefon nömrəsi formatı düzgün deyil!");
        }
    }
}

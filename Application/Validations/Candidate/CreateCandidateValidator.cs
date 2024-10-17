using Application.CommandHandlers.Candidate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations.Candidate
{
    public class CreateCandidateValidator : AbstractValidator<CreateCandidateCommand>
    {
        public CreateCandidateValidator()
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
                              .Matches(@"^\+994(50|51|55|70|77|99|10)\d{7}$")
                              .WithMessage("Telefon nömrəsi formatı düzgün deyil!"); 


        }
    }
}

using Application.CommandHandlers.Candidate;
using FluentValidation;

namespace Application.Validations.Candidate
{
    public class UpdateCandidateResumeValidator : AbstractValidator<UpdateCandidateResumeCommand>
    {
        public UpdateCandidateResumeValidator()
        {
            RuleFor(x => x.CandidateDto.Resume).NotNull()
           .NotEmpty().WithMessage("Cv məcburidir!");
        }
    }
}

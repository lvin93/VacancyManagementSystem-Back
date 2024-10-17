using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.PipelineBehaviours.Validators
{
    public class ValidatorPipelineBehaviour<TRequest, TResponse>
       (IEnumerable<IValidator<TRequest>> validators)
       : IPipelineBehavior<TRequest, TResponse>
       where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationContext = new ValidationContext<TRequest>(request);
            var validationResponse = await Task.WhenAll(validators.Select(x => x.ValidateAsync(validationContext)));

            var validationErrors = validationResponse.Where(x => x.Errors.Any())
                .SelectMany(x => x.Errors)
                .ToList();

            if (validationErrors.Any())
                throw new ValidationException(validationErrors);

            return await next();
        }
    }
}

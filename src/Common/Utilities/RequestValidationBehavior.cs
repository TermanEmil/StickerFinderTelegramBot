using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Utilities
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken ct, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext(request);

            var failures = validators
                .SelectMany(validator => validator.Validate(context).Errors)
                .Where(failure => failure != null)
                .ToList();

            if (failures.Any())
                throw new Exceptions.ValidationException(failures);

            return next();
        }
    }
}

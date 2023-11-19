using Application.Authentication.Commands.Register;
using Application.Authentication.Common;
using ErrorOr;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Behaviours
{

    /// <summary>
    /// Checking the data from TRequest that is valid with the rule set from CommandValidator or QureryValidator
    /// </summary>
    /// <typeparam name="TRequest">Generic type of request</typeparam>
    /// <typeparam name="TResponse">Generic type of response</typeparam>
    public class ValidationBehaviours<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {
        private readonly IValidator<TRequest>? validator;

        public ValidationBehaviours(IValidator<TRequest>? validator = null)
        {
            this.validator = validator;
        }


        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {

            if (validator is null)
            {
                return await next();
            }

            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (validatorResult.IsValid)
            {
                return await next();
            }

            var errors = validatorResult.Errors
                .ConvertAll(validatorResult => Error.Validation(
                    validatorResult.PropertyName,
                    validatorResult.ErrorMessage));

            return (dynamic)errors;
        }
    }
}

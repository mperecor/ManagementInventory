using FluentValidation;
using ManagementInventory.Application.Exceptions;
using MediatR;
using System.Net;

namespace ManagementInventory.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            MessageResponseException responseError;
            string erroMsg = "";
            List<string> errors = new List<string>();
            string ErrorCode = "";

            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                foreach (var faillure in failures)
                {
                    errors.Add(faillure.ErrorMessage);
                    erroMsg += faillure.ErrorMessage + "|";
                    ErrorCode = faillure.ErrorCode;
                }
                // Borrar último|
                erroMsg = erroMsg != "" ? erroMsg[..^1] : "";

                responseError = new MessageResponseException
                {
                    ErrorCount = failures.Count,
                    Message = erroMsg,
                    Errors = errors
                };

                if (failures.Count != 0)
                {
                    throw new HttpResponseException(ErrorCode == ((int)HttpStatusCode.BadRequest).ToString() ?
                        HttpStatusCode.BadRequest
                        : HttpStatusCode.InternalServerError, responseError);
                }
            }

            return await next();
        }
    }
}
using ApplicationCore.BaseClasses;
using Domain.Exceptions;
using FluentValidation;
using MediatR;
using ValidationException = Domain.Exceptions.ValidationException;

namespace ApplicationCore.Behaviours;

public class ValidationPipelineBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : BaseCommand<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehaviour(IEnumerable<IValidator<TRequest>> validators) =>
        _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        if (!_validators.Any())
        {
            return await next();
        }

        var validationFailures = await Task.WhenAll(
            _validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

        var errors = validationFailures
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validatorResult => validatorResult.Errors)
            .Select(failure => new ValidationError(
                failure.PropertyName,
                failure.ErrorMessage))
            .ToArray();

        if (errors.Any())
        {
            throw new ValidationException(errors);
        }

        return await next();
    }
}

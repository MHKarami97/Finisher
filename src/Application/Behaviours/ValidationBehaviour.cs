using Finisher.Shared.Validate;
using Wolverine.Attributes;
using ValidationException = Finisher.Application.Exceptions.ValidationException;

namespace Finisher.Application.Behaviours;

public class ValidationMiddleware<T>(IEnumerable<IValidator<T>> validators)
{
    [Middleware]
    public async Task HandleAsync(T message, IMessageContext context, Func<Task> next)
    {
        if (validators.Any())
        {
            var validationContext = new ValidationContext<T>(message);

            var validationResults = await Task.WhenAll(
                validators.Select(v => v.ValidateAsync(validationContext))
            );

            var failures = validationResults
                .Where(r => r.Errors?.IsNotEmpty() == true)
                .SelectMany(r => r.Errors!)
                .ToList();

            if (failures.Count > 0)
            {
                throw new ValidationException(failures);
            }
        }

        await next();
    }
}

using System.ComponentModel.DataAnnotations;

namespace Todo.Api.Filters;

public class ValidationFilter<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var argument = context.Arguments
            .OfType<T>()
            .FirstOrDefault();

        if (argument is null)
        {
            return Results.BadRequest();
        }

        var validationResults = new List<ValidationResult>();

        var validationContext = new ValidationContext(argument);

        var isValid = Validator.TryValidateObject(
            argument,
            validationContext,
            validationResults,
            true);

        if (!isValid)
        {
            return Results.ValidationProblem(
                validationResults.ToDictionary(
                    x => x.MemberNames.FirstOrDefault() ?? "",
                    x => new[] { x.ErrorMessage ?? "Invalid value" }
                ));
        }

        return await next(context);
    }
}
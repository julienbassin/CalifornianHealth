using FluentValidation;

namespace CalendarApi.Domain.ModelValidator
{
    public class ValidatorBase<TEntity> : AbstractValidator<TEntity> where TEntity : class
    {
    }
}

using FluentValidation;
using OnboardingSIGDB1.Domain.Notification;
using System.Collections.Generic;

namespace OnboardingSIGDB1.Domain.Entities
{
    public abstract class BaseEntity<TKey, TEntity> : AbstractValidator<TEntity>
    {
        public TKey Id { get; set; }

        public List<DomainNotification> ValidationResult { get; private set; } = new List<DomainNotification>();

        public abstract void DefineRules();

        public void AddValidationResult(TEntity entity)
        {
            var results = Validate(entity);
            foreach (var item in results.Errors)
            {
                var propertyName = !string.IsNullOrEmpty(item.PropertyName) ? $"{item.PropertyName}" : null;

                ValidationResult.Add(new DomainNotification($"{propertyName}", item.ErrorMessage));
            }
        }
    }
}

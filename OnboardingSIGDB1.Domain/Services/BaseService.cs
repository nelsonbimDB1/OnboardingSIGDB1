using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces.Notification;
using OnboardingSIGDB1.Domain.Interfaces.UoW;
using System;

namespace OnboardingSIGDB1.Domain.Services
{
    public class BaseService<TKey, TEntity> where TEntity : BaseEntity<TKey, TEntity>
    {   
        private bool _disposed;
        protected readonly IDomainNotificationHandler Notification;
        protected readonly IUnitOfWork _UoW;

        protected BaseService(IDomainNotificationHandler notification, IUnitOfWork UoW)
        {
            Notification = notification;
            _UoW = UoW;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Notification?.Dispose();
                }

                _disposed = true;
            }
        }

        public void Manipulate(TEntity entity, Action<TEntity> action)
        {
            entity.DefineRules();
            entity.AddValidationResult(entity);

            if (entity.ValidationResult.Count > 0)
            {
                Notification.Adicionar(entity.ValidationResult);
                return;
            }

            action(entity);
            _UoW.Commit();
        }
    }
}

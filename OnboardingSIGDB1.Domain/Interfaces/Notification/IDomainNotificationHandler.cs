using OnboardingSIGDB1.Domain.Notification;
using System;
using System.Collections.Generic;

namespace OnboardingSIGDB1.Domain.Interfaces.Notification
{
    public interface IDomainNotificationHandler : IDisposable
    {
        bool HasNotifications { get; }

        IReadOnlyCollection<DomainNotification> GetNotifications();

        void Adicionar(string value);

        void Adicionar(string key, string value);

        void Adicionar(IReadOnlyCollection<DomainNotification> notifications);
    }
}

using OnboardingSIGDB1.Domain.Interfaces.Notification;
using System.Collections.Generic;
using System.Linq;

namespace OnboardingSIGDB1.Domain.Notification
{
    public class DomainNotificationHandler : IDomainNotificationHandler
    {
        private readonly List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public void Adicionar(string value) =>
            Adicionar(string.Empty, value);

        public void Adicionar(string key, string value) =>
            _notifications.Add(new DomainNotification(key, value));

        public void Adicionar(IReadOnlyCollection<DomainNotification> notifications) =>
            notifications?.ToList().ForEach(n => _notifications.Add(n));

        public void Dispose() =>
            _notifications.Clear();

        public IReadOnlyCollection<DomainNotification> GetNotifications() =>
            _notifications;

        public bool HasNotifications =>
            _notifications.Any();
    }
}

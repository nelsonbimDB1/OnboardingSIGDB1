using System;

namespace OnboardingSIGDB1.Domain.Notification
{
    public class DomainNotification
    {
        public string Key { get; private set; }
        public string Value { get; private set; }
        public DateTime DataOcorrencia { get; private set; }

        public DomainNotification(string key, string value)
        {
            Key = key;
            Value = value;
            DataOcorrencia = DateTime.Now;
        }
    }
}

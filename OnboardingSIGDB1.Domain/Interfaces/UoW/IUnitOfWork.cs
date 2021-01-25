using System;

namespace OnboardingSIGDB1.Domain.Interfaces.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();

        void CommitTransaction();

        bool Commit();
    }
}

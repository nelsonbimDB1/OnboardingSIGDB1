using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OnboardingSIGDB1.Data.Context;
using OnboardingSIGDB1.Domain.Interfaces.Notification;
using OnboardingSIGDB1.Domain.Interfaces.UoW;
using System;
using System.Linq;

namespace OnboardingSIGDB1.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SIGContext _context;
        private IDbContextTransaction _transaction;
        private readonly IDomainNotificationHandler _notification;

        public UnitOfWork(SIGContext context, IDomainNotificationHandler notification)
        {
            _context = context;
            _notification = notification;
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
        }

        public bool Commit()
        {
            if (_notification.HasNotifications)
            {
                return false;
            }

            _context.SaveChanges();

            ClearEFTracker();

            return true;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

        private void ClearEFTracker()
        {
            var entries = _context.ChangeTracker.Entries().ToList();

            foreach (var entry in entries)
                entry.State = EntityState.Detached;
        }
    }
}

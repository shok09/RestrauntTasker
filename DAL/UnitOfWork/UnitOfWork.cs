using DAL.Base;
using DAL.Entities;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork, IDisposable
    {
        public UnitOfWork(RestrauntTrackerContext context) =>
            _context = context;

        readonly RestrauntTrackerContext _context;

        IRepository<Order> orderRepository;
        IRepository<OrderTask> orderTaskRepository;
        IRepository<Staff> userRepository;
        IRepository<DateInfo> dateInfoRepository;
        IRepository<OrderTaskStatus> orderTaskStatusRepository;
        IRepository<UserContacts> userContactsRepository;

        public IRepository<Order> OrderRepository => 
            orderRepository ?? new Repository<Order>(_context);

        public IRepository<OrderTask> OrderTaskRepository =>
            orderTaskRepository ?? new Repository<OrderTask>(_context);

        public IRepository<Staff> UserRepository =>
            userRepository ?? new Repository<Staff>(_context);

        public IRepository<DateInfo> DateInfoRepository =>
            dateInfoRepository ?? new Repository<DateInfo>(_context);

        public IRepository<OrderTaskStatus> OrderTaskStatusRepository =>
            orderTaskStatusRepository ?? new Repository<OrderTaskStatus>(_context);

        public IRepository<UserContacts> UserContactsRepository =>
            userContactsRepository ?? new Repository<UserContacts>(_context);

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
             await _context.SaveChangesAsync();
        }

        bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}

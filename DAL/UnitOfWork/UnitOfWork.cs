﻿using DAL.Base;
using DAL.Entities;
using DAL.Entities.TokenModel;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public UnitOfWork(RestrauntTaskerContext context) =>
            _context = context;

        readonly RestrauntTaskerContext _context;

        IRepository<Order> orderRepository;
        IRepository<OrderTask> orderTaskRepository;
        IRepository<OrderUser> userRepository;
        IRepository<DateInfo> dateInfoRepository;
        IRepository<OrderTaskStatus> orderTaskStatusRepository;
        IRepository<UserContacts> userContactsRepository;

        public IRepository<Order> OrderRepository => 
            orderRepository ??= new Repository<Order>(_context);

        public IRepository<OrderTask> OrderTaskRepository =>
            orderTaskRepository ??= new Repository<OrderTask>(_context);

        public IRepository<OrderUser> UserRepository =>
            userRepository ??= new Repository<OrderUser>(_context);

        public IRepository<DateInfo> DateInfoRepository =>
            dateInfoRepository ??= new Repository<DateInfo>(_context);

        public IRepository<OrderTaskStatus> OrderTaskStatusRepository =>
            orderTaskStatusRepository ??= new Repository<OrderTaskStatus>(_context);

        public IRepository<UserContacts> UserContactsRepository =>
            userContactsRepository ??= new Repository<UserContacts>(_context);

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

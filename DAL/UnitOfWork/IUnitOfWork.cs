using DAL.Entities;
using DAL.Entities.TokenModel;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Order> OrderRepository { get; }
        IRepository<OrderTask> OrderTaskRepository { get; }
        IRepository<OrderUser> UserRepository { get; }
        IRepository<DateInfo> DateInfoRepository { get; }
        IRepository<OrderTaskStatus> OrderTaskStatusRepository { get; }
        IRepository<UserContacts> UserContactsRepository { get;}

        void SaveChanges();
        Task SaveChangesAsync();
    }
}

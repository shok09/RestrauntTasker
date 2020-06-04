using DAL.Entities.Abstract;
using DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Order : BaseEntity<int>
    {
        public Order()
        {
            Tasks = new HashSet<OrderTask>();
            Users = new HashSet<Staff>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public OrderStatus Status { get; set; }

        public virtual Staff Chef { get; set; }

        public virtual ICollection<OrderTask> Tasks { get; set; }
        public virtual ICollection<Staff> Users { get; set; }
        
    }
}

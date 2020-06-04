using DAL.Entities.Abstract;
using DAL.Entities.Enums;
using DAL.Entities.IdentityModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Staff : BaseEntity<int>
    {
        public Staff() => Tasks = new HashSet<OrderTask>();

        public string Name { get; set; }
        public string UserContactsId { get; set; }
        public UserRole Role { get; set; }
        public int OrderId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual UserContacts UserContacts { get; set; }
        public virtual Order Order { get; set; }
        public virtual ICollection<OrderTask> Tasks { get; set; }
    }
}

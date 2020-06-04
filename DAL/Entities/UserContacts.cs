using DAL.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class UserContacts : BaseEntity<int>
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}

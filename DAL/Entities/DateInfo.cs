using DAL.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class DateInfo : BaseEntity<int>
    {
        public DateTime BeginDate { get; set; }
        public DateTime Deadline { get; set; }
    }
}

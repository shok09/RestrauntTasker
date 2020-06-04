using DAL.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class OrderTask : BaseEntity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual OrderTaskStatus OrderStatus { get; set; }
        public virtual DateInfo DateInfo { get; set; }
        public virtual Staff Performer { get; set; }
    }
}

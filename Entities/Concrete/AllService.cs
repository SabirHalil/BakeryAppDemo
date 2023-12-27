using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public  class AllService :IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int ServiceProductId { get; set; }
        public int Quantity { get; set; }
        public int ServiceTypeId { get; set; }

    }
}

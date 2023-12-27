using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public  class ReceivedMoney :IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public int ServiceTypeId { get; set; }

    }
}

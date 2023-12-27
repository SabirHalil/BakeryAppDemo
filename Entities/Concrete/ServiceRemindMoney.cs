using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public  class ServiceRemindMoney :IEntity
    {
        public int Id { get; set; }
        public int MarketId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}

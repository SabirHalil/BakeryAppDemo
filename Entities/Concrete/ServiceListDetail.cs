using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public  class ServiceListDetail :IEntity
    {
        public int Id { get; set; }
        public int ServiceListId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int MarketContractId { get; set; }

    }
}

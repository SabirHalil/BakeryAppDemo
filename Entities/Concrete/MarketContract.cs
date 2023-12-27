using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public  class MarketContract :IEntity
    {
        

        public int Id { get; set; }
        public int ServiceProductId { get; set; }
        public decimal Price { get; set; }
        public int MarketId { get; set; }

    }
}

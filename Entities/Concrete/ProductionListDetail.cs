using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public  class ProductionListDetail :IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int ProductionListId { get; set; }
        public int Quantity { get; set; }

    }
}

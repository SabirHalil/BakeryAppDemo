using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public  class Product :IEntity
    {
        

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }

    }
}

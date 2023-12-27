using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public  class BreadPrice :IEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }
}

using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class BreadCounting :IEntity
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}

using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public  class Expense :IEntity
    {
        public int Id { get; set; }
        public string Detail { get; set; } = null!;
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}

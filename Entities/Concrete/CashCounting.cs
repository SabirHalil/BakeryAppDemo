using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public  class CashCounting :IEntity
    {
        public int Id { get; set; }
        public decimal TotalMoney { get; set; }
        public decimal RemainedMoney { get; set; }
        public decimal CreditCard { get; set; }
        public DateTime Date { get; set; }
    }
}

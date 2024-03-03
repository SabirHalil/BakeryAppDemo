using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ServiceListDetailDto :IDto
    {
        public int ServiceListId { get; set; }
        public int Quantity { get; set; }
        public int MarketContractId { get; set; }        

    }
}

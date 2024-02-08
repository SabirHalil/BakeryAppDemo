using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class GivenProductsToServiceTotalResultDto : IDto
    {
        public string ServiceTypeName { get; set; }
        public int TotalQuantity { get; set; }
    }
}

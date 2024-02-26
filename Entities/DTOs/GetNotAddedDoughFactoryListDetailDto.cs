using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class GetNotAddedDoughFactoryListDetailDto : IDto
    {
        public int DoughFactoryProductId { get; set; }
        public string DoughFactoryProductName { get; set; }
    }
}

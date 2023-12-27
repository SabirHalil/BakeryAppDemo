using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class StaleBreadDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public int DoughFactoryProductId { get; set; }
        public string DoughFactoryProductName { get; set; }
    }
}

using Core.Entities;

namespace Entities.DTOs
{
    public class ProductsAddedToServiceListDetailDto : IDto
    {
        public int ServiceListDetailId { get; set; }
        public string ServiceProductName { get; set; }
        public int Quantity { get; set; }

    }
}

using Core.Entities;

namespace Entities.DTOs
{
    public class ProductsCountingDto : IDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        
    }
}

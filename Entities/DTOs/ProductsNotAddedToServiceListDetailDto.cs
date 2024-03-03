using Core.Entities;

namespace Entities.DTOs
{
    public class ProductsNotAddedToServiceListDetailDto : IDto
    {
        public int MarketContractId { get; set; }
        public string ServiceProductName { get; set; }       

    }
}

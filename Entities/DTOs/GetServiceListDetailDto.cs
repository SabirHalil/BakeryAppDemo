using Core.Entities;

namespace Entities.DTOs
{
    public class GetServiceListDetailDto : IDto
    {
        public int ServiceListId { get; set; }       
        public int MarketId { get; set; }
        public string MarketName { get; set; }

    }
}

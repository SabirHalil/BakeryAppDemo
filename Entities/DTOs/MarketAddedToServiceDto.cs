using Core.Entities;

namespace Entities.DTOs
{
    public class MarketAddedToServiceDto : IDto
    {       
        public int MarketId { get; set; }
        public string MarketName { get; set; }

    }
}

using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public  class DoughFactoryListDetail : IEntity
    {
        public int Id { get; set; }
        public int DoughFactoryProductId { get; set; }
        public int Quantity { get; set; }
        public int DoughFactoryListId { get; set; }

    }
}

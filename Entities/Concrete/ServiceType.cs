using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public  class ServiceType :IEntity
    {
        
        public int Id { get; set; }
        public string Name { get; set; } = null!;

    }
}

using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public  class ServiceProduct :IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

    }
}

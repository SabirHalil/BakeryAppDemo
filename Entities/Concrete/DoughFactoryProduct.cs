using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public  class DoughFactoryProduct : IEntity
    {
        public int Id { get; set; }
        public double BreadEquivalent { get; set; }
        public string Name { get; set; } 
        public bool Status { get; set; } 

    }
}

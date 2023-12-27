using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public  class Category :IEntity
    {
        //public Category()
        //{
        //    Products = new HashSet<Product>();
        //}

        public int Id { get; set; }
        public string Name { get; set; } = null!;

       
    }
}

using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IDoughFactoryProductService
    {
        List<DoughFactoryProduct> GetAll();
        void Add(DoughFactoryProduct doughFactoryProduct);
        void Delete(DoughFactoryProduct doughFactoryProduct);
        void Update(DoughFactoryProduct doughFactoryProduct);
        DoughFactoryProduct GetById(int id);
        List<DoughFactoryProduct> GetAllProducts();
    }
}

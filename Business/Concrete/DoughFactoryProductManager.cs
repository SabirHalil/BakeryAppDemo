using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class DoughFactoryProductManager : IDoughFactoryProductService
    {
        IDoughFactoryProductDal _doughFactoryProductDal;

        public DoughFactoryProductManager(IDoughFactoryProductDal doughFactoryProductDal)
        {
            _doughFactoryProductDal = doughFactoryProductDal;

        }
      
        public void Add(DoughFactoryProduct doughFactoryProduct)
        {
            _doughFactoryProductDal.Add(doughFactoryProduct);
        }

        public void Delete(DoughFactoryProduct doughFactoryProduct)
        {
            _doughFactoryProductDal.Delete(doughFactoryProduct);
        }

        public List<DoughFactoryProduct> GetAll()
        {
            return _doughFactoryProductDal.GetAll(d=> d.Status == true);
        }

        public List<DoughFactoryProduct> GetAllProducts()
        {
            return _doughFactoryProductDal.GetAll();
        }

        public DoughFactoryProduct GetById(int id)
        {
            return _doughFactoryProductDal.Get(d=>d.Id == id);
        }

        public void Update(DoughFactoryProduct doughFactoryProduct)
        {
            _doughFactoryProductDal.Update(doughFactoryProduct);
        }
    }

}

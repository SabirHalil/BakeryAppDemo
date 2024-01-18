using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace Business.Concrete
{
    public class PurchasedProductListDetailManager : IPurchasedProductListDetailService
    {


        IPurchasedProductListDetailDal _purchasedProductListDetailDal;
        
        public PurchasedProductListDetailManager(IPurchasedProductListDetailDal purchasedProductListDetailDal)
        {
            _purchasedProductListDetailDal = purchasedProductListDetailDal;  
        }

        public void Add(PurchasedProductListDetail purchasedProductListDetail)
        {
            _purchasedProductListDetailDal.Add(purchasedProductListDetail);
        }

        public void DeleteById(int id, int userId)
        {
            _purchasedProductListDetailDal.DeleteById(id, userId);
        }

        public void Delete(PurchasedProductListDetail purchasedProductListDetail)
        {
            _purchasedProductListDetailDal.Delete(purchasedProductListDetail);
        }
        public List<PurchasedProductListDetail> GetAll()
        {
           return _purchasedProductListDetailDal.GetAll();
        }

        public PurchasedProductListDetail GetById(int id)
        {
            return _purchasedProductListDetailDal.Get(d => d.Id == id);
        }

        public void Update(PurchasedProductListDetail purchasedProductListDetail)
        {
            _purchasedProductListDetailDal.Update(purchasedProductListDetail);
        }

        public List<PurchasedProductListDetail> GetPurchasedProductListDetailByDate(DateTime date)
        {
            return _purchasedProductListDetailDal.GetAll(p => p.Date.Date == date.Date);
        }

        public void AddList(List<PurchasedProductListDetail> purchasedProductListDetails)
        {
            for(int i = 0; i<purchasedProductListDetails.Count; i++)
            {
                _purchasedProductListDetailDal.Add(purchasedProductListDetails[i]);
            }
        }

        public bool IsExist(int productId, DateTime date)
        {
            return _purchasedProductListDetailDal.Get(d => d.ProductId == productId && d.Date.Date == date.Date) == null ? false : true;
        }

        public void UpdateList(List<PurchasedProductListDetail> purchasedProductListDetails)
        {
            for (int i = 0; i < purchasedProductListDetails.Count; i++)
            {
                _purchasedProductListDetailDal.Add(purchasedProductListDetails[i]);
            }
        }

        public PurchasedProductListDetail GetPurchasedProductListDetailByDateAndProductId(DateTime date, int productId)
        {
            PurchasedProductListDetail purchasedProductListDetail = _purchasedProductListDetailDal.Get(d => d.ProductId == productId && d.Date.Date == date);
            return purchasedProductListDetail == null ? new PurchasedProductListDetail { Price =0, Quantity =0} : purchasedProductListDetail;
        }
    }
}

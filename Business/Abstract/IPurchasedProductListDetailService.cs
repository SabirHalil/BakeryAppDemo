using Entities.Concrete;

namespace Business.Abstract
{
    public interface IPurchasedProductListDetailService
    {
        List<PurchasedProductListDetail> GetAll();
        List<PurchasedProductListDetail> GetPurchasedProductListDetailByDate(DateTime date);
        void Add(PurchasedProductListDetail purchasedProductListDetail);
        void AddList(List<PurchasedProductListDetail> purchasedProductListDetails);
        void DeleteById(int id, int userId);
        void Delete(PurchasedProductListDetail purchasedProductListDetail);
        void Update(PurchasedProductListDetail purchasedProductListDetail);
        void UpdateList(List<PurchasedProductListDetail> purchasedProductListDetails);
        PurchasedProductListDetail GetById(int id);
        bool IsExist(int productId);
    }
}

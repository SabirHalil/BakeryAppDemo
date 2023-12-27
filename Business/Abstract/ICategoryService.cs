using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        void Add(Category category);
        void DeleteById(int id);
        void Delete(Category category);
        void Update(Category category);
        Category GetById(int id);
    }
}

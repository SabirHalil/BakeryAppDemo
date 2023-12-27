using Entities.Concrete;

namespace Business.Abstract
{
    public interface IExpenseService
    {
        List<Expense> GetAll();
        void Add(Expense expense);
        List<Expense> GetExpensesByDate(DateTime date);
        void DeleteById(int id);
        void Delete(Expense expense);
        void Update(Expense expense);
        Expense GetById(int id);
    }
}

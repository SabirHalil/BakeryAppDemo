using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ExpenseManager : IExpenseService
    {


        IExpenseDal _expenseDal;
        
        public ExpenseManager(IExpenseDal expenseDal)
        {
            _expenseDal = expenseDal;  
        }

        public void Add(Expense expense)
        {
            _expenseDal.Add(expense);
        }

        public void DeleteById(int id)
        {
            _expenseDal.DeleteById(id);
        }

        public void Delete(Expense expense)
        {
            _expenseDal.Delete(expense);
        }
        public List<Expense> GetAll()
        {
           return _expenseDal.GetAll();
        }

        public Expense GetById(int id)
        {
            return _expenseDal.Get(d => d.Id == id);
        }

        public void Update(Expense expense)
        {
            _expenseDal.Update(expense);
        }

        public List<Expense> GetExpensesByDate(DateTime date)
        {
            return _expenseDal.GetAll(e => e.Date.Date == date.Date);
        }
    }
}

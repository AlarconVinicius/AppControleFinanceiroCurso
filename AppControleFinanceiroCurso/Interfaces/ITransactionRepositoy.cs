using AppControleFinanceiroCurso.Models;

namespace AppControleFinanceiroCurso.Interfaces
{
    public interface ITransactionRepositoy
    {
        void Add(TransactionModel objeto);
        void Delete(TransactionModel objeto);
        TransactionModel GetTransactionById(int id);
        List<TransactionModel> GetAll();
        IEnumerable<TransactionModel> GetTransactionsByMonthAndYear(int month, int year);
        void Update(TransactionModel objeto);
    }
}
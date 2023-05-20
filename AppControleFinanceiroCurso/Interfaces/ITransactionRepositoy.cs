using AppControleFinanceiroCurso.Models;

namespace AppControleFinanceiroCurso.Interfaces
{
    public interface ITransactionRepositoy
    {
        void Add(TransactionModel objeto);
        void Delete(TransactionModel objeto);
        List<TransactionModel> GetAll();
        void Update(TransactionModel objeto);
    }
}
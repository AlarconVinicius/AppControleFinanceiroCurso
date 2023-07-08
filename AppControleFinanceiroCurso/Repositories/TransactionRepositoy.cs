using AppControleFinanceiroCurso.Interfaces;
using AppControleFinanceiroCurso.Models;
using LiteDB;

namespace AppControleFinanceiroCurso.Repositories
{
    public class TransactionRepositoy : ITransactionRepositoy
    {
        private readonly LiteDatabase _database;
        private readonly string _collectionName = "transactions";
        public TransactionRepositoy(LiteDatabase database)
        {
            _database = database;
        }

        public TransactionModel GetTransactionById(int id)
        {
            return _database.GetCollection<TransactionModel>(_collectionName).FindById(id);
        }
        public List<TransactionModel> GetAll()
        {
            return _database.GetCollection<TransactionModel>(_collectionName).Query().OrderByDescending(t => t.Date).ToList();
        }

        public IEnumerable<TransactionModel> GetTransactionsByMonthAndYear(int month, int year)
        {
            return _database.GetCollection<TransactionModel>("transactions")
                            .Find(t => t.Date.Month == month && t.Date.Year == year)
                            .OrderByDescending(t => t.Date)
                            .ToList();
        }
        public void Add(TransactionModel objeto)
        {
            objeto.CreatedAt = DateTime.UtcNow;
            objeto.ModifiedAt = DateTime.UtcNow;
            var coll =  _database.GetCollection<TransactionModel>(_collectionName);
           coll.Insert(objeto);
            coll.EnsureIndex(t => t.Date);
        }
        public void Update(TransactionModel objeto)
        {
            objeto.ModifiedAt = DateTime.UtcNow;
            var coll = _database.GetCollection<TransactionModel>(_collectionName);
            coll.Update(objeto);
        }
        public void Delete(TransactionModel objeto)
        {
            var coll = _database.GetCollection<TransactionModel>(_collectionName);
            coll.Delete(objeto.Id);
        }
    }
}

using AppControleFinanceiroCurso.Interfaces;
using AppControleFinanceiroCurso.Models;
using LiteDB;

namespace AppControleFinanceiroCurso.Repositories
{
    public class TransactionRepositoy : ITransactionRepositoy
    {
        private readonly LiteDatabase _database;
        private readonly string _collectionName = "transactions";
        public TransactionRepositoy()
        {
            _database = new LiteDatabase("Filename=C:/users/AppData/controle_financeiro_db.db;Connection=Shared");
        }

        public List<TransactionModel> GetAll()
        {
            return _database.GetCollection<TransactionModel>(_collectionName).Query().OrderByDescending(t => t.Date).ToList();
        }

        public void Add(TransactionModel objeto)
        {
           var coll =  _database.GetCollection<TransactionModel>(_collectionName);
           coll.Insert(objeto);
            coll.EnsureIndex(t => t.Date);
        }
        public void Update(TransactionModel objeto)
        {
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

using AppControleFinanceiroCurso.Models.Enums;
using LiteDB;

namespace AppControleFinanceiroCurso.Models
{
    public class TransactionModel
    {
        [BsonId]
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public String Name { get; set; }
        public DateTimeOffset Date { get; set; }
        public decimal Value { get; set; }
    }
}

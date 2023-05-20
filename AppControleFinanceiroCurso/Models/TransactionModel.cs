using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppControleFinanceiroCurso.Models.Enums;

namespace AppControleFinanceiroCurso.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public String Name { get; set; }
        public DateTimeOffset Date { get; set; }
        public decimal Value { get; set; }
    }
}

using AppControleFinanceiroCurso.Models;
using AppControleFinanceiroCurso.Models.Enums;
using System.Globalization;

namespace AppControleFinanceiroCurso.Libraries.Converters
{
    public class TransactionValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TransactionModel transaction = (TransactionModel)value;

            if (transaction == null) return string.Empty;

            if (transaction.Type == TransactionType.Income)
            {
                return transaction.Value.ToString("C");
            }
            else
            {

                return $"- {transaction.Value.ToString("C")}";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

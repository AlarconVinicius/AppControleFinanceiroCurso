using AppControleFinanceiroCurso.Models;
using AppControleFinanceiroCurso.Models.Enums;
using System.Globalization;

namespace AppControleFinanceiroCurso.Libraries.Converters
{
    public class TransactionValueColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TransactionModel transaction = (TransactionModel)value;

            if(transaction == null) return Colors.Black;

            if (transaction.Type == TransactionType.Income)
            {
                return Color.FromArgb("#FF05B76B");
            }
            else
            {

                return Color.FromArgb("#FFC82501");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

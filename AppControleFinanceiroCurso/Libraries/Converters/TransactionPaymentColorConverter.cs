using AppControleFinanceiroCurso.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppControleFinanceiroCurso.Libraries.Converters
{
    class TransactionPaymentColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TransactionModel transaction = (TransactionModel)value;

            if (transaction == null) return Colors.Transparent;

            if (transaction.Paid)
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

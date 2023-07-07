using AppControleFinanceiroCurso.Views;
using Microsoft.Extensions.DependencyInjection;

namespace AppControleFinanceiroCurso;

public partial class App : Application
{
    public App(TransactionList listPage)
    {
        InitializeComponent();
        App.Current!.UserAppTheme = AppTheme.Light;

        MainPage = new NavigationPage(listPage);
    }
}

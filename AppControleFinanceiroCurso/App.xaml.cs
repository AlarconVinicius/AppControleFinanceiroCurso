using AppControleFinanceiroCurso.Views;

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

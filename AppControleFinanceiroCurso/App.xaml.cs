using AppControleFinanceiroCurso.Views;

namespace AppControleFinanceiroCurso;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new TransactionList();
	}
}

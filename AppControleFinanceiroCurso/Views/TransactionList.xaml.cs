using AppControleFinanceiroCurso.Extensions;
using AppControleFinanceiroCurso.Interfaces;
using AppControleFinanceiroCurso.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Globalization;

namespace AppControleFinanceiroCurso.Views;

public partial class TransactionList : ContentPage
{
    private readonly ITransactionRepositoy _transactionRepositoy;
    private Color _borderDefaultBackgroundColor;
    private string _labelDefaultText;
    private int Year;
    private int Month;
    public TransactionList(ITransactionRepositoy transactionRepositoy)
    {
        _transactionRepositoy = transactionRepositoy;
        InitializeComponent();
        this.Year = DateTime.UtcNow.Year;
        this.Month = DateTime.UtcNow.Month;
        Reload();
        WeakReferenceMessenger.Default.Register<string>(this, (e, msg) =>
        {
            Reload();
        });
    }

    private void Reload()
    {
        GetYearAndMounth();
        var items = _transactionRepositoy.GetTransactionsByMonthAndYear(this.Month, this.Year);
        CollectionViewTransactions.ItemsSource = items;
        BorderTransactions.IsVisible = items.Count() != 0 ? true : false;
        BorderTransactionsEmpty.IsVisible = items.Count() == 0 ? true : false;
        double income = items.Where(a => a.Type == Models.Enums.TransactionType.Income).Sum(a => a.Value);
        double expense = items.Where(a => a.Type == Models.Enums.TransactionType.Expense).Sum(a => a.Value);
        double balance = income - expense;
        double paid = items.Where(a => a.Paid == true).Sum(a => a.Value);
        double pending = items.Where(a => a.Paid == false).Sum(a => a.Value);

        LabelIncome.Text = income.ToString("C");
        LabelExpense.Text = expense.ToString("C");
        LabelBalance.Text = balance.ToString("C");
        LabelPaid.Text = paid.ToString("C");
        LabelPending.Text = pending.ToString("C");
    }

    private void OnButtonClicked_To_TransactionAdd(object sender, EventArgs args)
    {

        var transactionAdd = Handler.MauiContext.Services.GetService<TransactionAdd>();
        Navigation.PushModalAsync(transactionAdd);
    }

    private void TapGestureRecognizer_Tapped_To_TransactionEdit(object sender, TappedEventArgs e)
    {
        var grid = (Grid)sender;
        var gesture = (TapGestureRecognizer)grid.GestureRecognizers[0];

        TransactionModel transaction = (TransactionModel)gesture.CommandParameter;

        var transactionEdit = Handler.MauiContext.Services.GetService<TransactionEdit>();
        transactionEdit.SetTransactionToEdit(transaction);
        Navigation.PushModalAsync(transactionEdit);
    }

    private async void TapGestureRecognizer_Tapped_ToDelete(object sender, TappedEventArgs e)
    {
        await AnimationBorder((Border)sender, true);
        bool result = await App.Current.MainPage.DisplayAlert("Excluir!", "Tem certeza que deseja excluir?", "Sim", "Não");

        if (result)
        {
            TransactionModel transaction = (TransactionModel)e.Parameter;
            _transactionRepositoy.Delete(transaction);
            Reload();
        }
        else
        {
            await AnimationBorder((Border)sender, false);
        }
    }
    private async Task AnimationBorder(Border border, bool IsDeleteAnimation)
    {
        var label = (Label)border.Content;
        if (IsDeleteAnimation)
        {
            _borderDefaultBackgroundColor = border.BackgroundColor;
            _labelDefaultText = label.Text;
            await border.RotateYTo(90, 250);
            border.BackgroundColor = Colors.Red;
            label.TextColor = Colors.White;
            label.Text = "X";
            await border.RotateYTo(180, 250);
        }
        else
        {
            await border.RotateYTo(90, 250);
            border.BackgroundColor = _borderDefaultBackgroundColor;
            label.TextColor = Colors.Black;
            label.Text = _labelDefaultText;
            await border.RotateYTo(0, 250);
        }
    }

    private void DecreaseMonthClicked(object sender, EventArgs e)
    {
        this.Month--;
        if (this.Month < 1)
        {
            this.Month = 12;
            this.Year--;
        }

        Reload();
    }

    private void IncreaseMonthClicked(object sender, EventArgs e)
    {
        this.Month++;
        if (this.Month > 12)
        {
            this.Month = 1;
            this.Year++;
        }

        Reload();
    }

    private void GetYearAndMounth()
    {
        string monthName = new DateTime(this.Year, this.Month, 1).ToString("MMMM", CultureInfo.GetCultureInfo("pt-BR"));
        LabelMonthYear.Text = $"{monthName.ToCapitalize()} {this.Year}";

    }
}
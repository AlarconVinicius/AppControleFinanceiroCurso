using AppControleFinanceiroCurso.Interfaces;
using AppControleFinanceiroCurso.Models;
using AppControleFinanceiroCurso.Models.Enums;
using CommunityToolkit.Mvvm.Messaging;
using System.Text;

namespace AppControleFinanceiroCurso.Views;

public partial class TransactionEdit : ContentPage
{
    private TransactionModel _transactionModel;
    private ITransactionRepositoy _repository;
    public TransactionEdit(ITransactionRepositoy repository)
    {
        InitializeComponent();
        _repository = repository;
    }
    public void SetTransactionToEdit(TransactionModel transaction)
    {
        _transactionModel = transaction;
        if(transaction.Type == TransactionType.Income)
        {
            RadioIncome.IsChecked = true;
        }
        else
        {
            RadioExpense.IsChecked = true;
        }
        if (transaction.Paid)
        {
            CheckBoxPaid.IsChecked = true;
        }
        else
        {
            CheckBoxPaid.IsChecked = false;
        }
        EntryName.Text = transaction.Name;
        DatePickerDate.Date = transaction.Date.Date;
        EntryValue.Text = transaction.Value.ToString();
    }
    private void TapGestureRecognizer_Tapped_ToClose(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private void OnButtonClicked_Save(object sender, EventArgs e)
    {
        if (IsValidData() == false)
        {
            return;
        }
        SaveTransactionInDatabase();
        Navigation.PopModalAsync();
        WeakReferenceMessenger.Default.Send<string>(string.Empty);
    }

    private void SaveTransactionInDatabase()
    {
        TransactionModel transaction = new TransactionModel()
        {
            Id = _transactionModel.Id,
            Type = RadioIncome.IsChecked ? TransactionType.Income : TransactionType.Expense,
            Name = EntryName.Text,
            Date = DatePickerDate.Date,
            Value = Math.Abs(double.Parse(EntryValue.Text)),
            Paid = CheckBoxPaid.IsChecked ? true : false
        };
        _repository.Update(transaction);
    }

    private bool IsValidData()
    {
        bool valid = true;
        StringBuilder stringBuilder = new StringBuilder();
        double result;
        if (string.IsNullOrEmpty(EntryName.Text) || string.IsNullOrWhiteSpace(EntryName.Text))
        {
            stringBuilder.AppendLine("O campo 'Nome' deve ser preenchido!");
            valid = false;
        }
        if (string.IsNullOrEmpty(EntryValue.Text) || string.IsNullOrWhiteSpace(EntryValue.Text))
        {
            stringBuilder.AppendLine("O campo 'Valor' deve ser preenchido!");
            valid = false;
        }
        if (!string.IsNullOrEmpty(EntryValue.Text) && !double.TryParse(EntryValue.Text, out result))
        {
            stringBuilder.AppendLine("O campo 'Valor' é inválido!");
            valid = false;
        }
        if (valid == false)
        {
            LabelError.IsVisible = true;
            LabelError.Text = stringBuilder.ToString();
        }
        return valid;
    }
}
using AppControleFinanceiroCurso.Interfaces;
using AppControleFinanceiroCurso.Models;
using AppControleFinanceiroCurso.Models.Enums;
using CommunityToolkit.Mvvm.Messaging;
using System.Text;

namespace AppControleFinanceiroCurso.Views;

public partial class TransactionAdd : ContentPage
{
    private ITransactionRepositoy _repository;
    private int qtdRepete;
    public TransactionAdd(ITransactionRepositoy repository)
    {
        InitializeComponent();
        _repository = repository;
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
        var count = _repository.GetAll().Count;
        App.Current.MainPage.DisplayAlert("Mensagem!", $"Existem {count} registro(s) no banco!", "OK");
    }

    private void SaveTransactionInDatabase()
    {
        TransactionModel transaction = new TransactionModel()
        {
            Type = RadioIncome.IsChecked ? TransactionType.Income : TransactionType.Expense,
            Name = EntryName.Text,
            Date = DatePickerDate.Date,
            Value = double.Parse(EntryValue.Text),
            Paid = CheckBoxPaid.IsChecked ? true : false,
            Repete = CheckBoxRepete.IsChecked ? true : false,
            QtdRepete = this.qtdRepete
        };
        _repository.Add(transaction);
    }

    private bool IsValidData()
    {
        bool valid = true;
        StringBuilder stringBuilder = new StringBuilder();
        double result;
        int repeteResult;
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
        if(!string.IsNullOrEmpty(EntryValue.Text) && !double.TryParse(EntryValue.Text, out result))
        {
            stringBuilder.AppendLine("O campo 'Valor' é inválido!");
            valid = false;
        }
        if(CheckBoxRepete.IsChecked)
        {
            if (string.IsNullOrEmpty(EntryQtdRepete.Text) || string.IsNullOrWhiteSpace(EntryQtdRepete.Text))
            {
                stringBuilder.AppendLine("O campo 'Repetir Mensalmente' deve ser preenchido!");
                valid = false;
            }
            if (!string.IsNullOrEmpty(EntryQtdRepete.Text) && !int.TryParse(EntryQtdRepete.Text, out repeteResult))
            {
                stringBuilder.AppendLine("O campo 'Repetir Mensalmente' é inválido!");
                valid = false;
            }
            else if (int.TryParse(EntryQtdRepete.Text, out repeteResult) && repeteResult <= 0)
            {
                stringBuilder.AppendLine("O campo 'Repetir Mensalmente' deve ser maior que 0!");
                valid = false;
            }
            else if (int.TryParse(EntryQtdRepete.Text, out repeteResult))
            {
                this.qtdRepete = repeteResult;
            }
        }
        else
        {
            this.qtdRepete = 0;
        }
        if(valid == false)
        {
            LabelError.IsVisible = true;
            LabelError.Text = stringBuilder.ToString();
        }
        return valid;
    }
}
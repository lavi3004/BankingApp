using BankingApp.Areas.Identity.Data;

namespace BankingApp.Models;

public class Transaction
{
    public int Id { get; set; }

    public int Amount { get; set; }

    public DateTime Date { get; set; }

    public BankAccount Account { get; set; } = new BankAccount();

    public User Sender { get; set; } = new User();

    public string Reciver { get; set; } = string.Empty;
}

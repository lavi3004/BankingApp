using BankingApp.Areas.Identity.Data;

namespace BankingApp.Models;

public class Transaction
{
    public int Id { get; set; }

    public int Amount { get; set; }

    public DateTime Date { get; set; }

    public User Sender { get; set; }

    public string Reciver { get; set; } = string.Empty;
}

using BankingApp.Areas.Identity.Data;

namespace BankingApp.Models;

public class Card
{
    public int Id { get; set; }

    public string CardNumber { get; set; } = string.Empty;

    public DateTime ExpirationDate { get; set; }

    public User CardHolder { get; set; } = new User();
}

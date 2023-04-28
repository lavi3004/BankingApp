using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model;

public class BankAccount: ModelEntity
{
    public string IBAN { get; set; } = string.Empty;

    public string SWIFT { get; set; } = string.Empty;

    public float Balance { get; set; }

    public string Currency { get; set; } = string.Empty;

    public ICollection<Transaction> Transactions { get; set; }

    public ICollection<Card> Cards { get; set; }
}

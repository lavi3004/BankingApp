using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model;

public class Transaction: ModelEntity
{
    public int Id { get; set; }

    public int Amount { get; set; }

    public DateOnly Date { get; set; }

    public User Sender { get; set; } 

    public string Reciver { get; set; } = string.Empty;
}

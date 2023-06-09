﻿using BankingApp.Areas.Identity.Data;

namespace BankingApp.Models;

public class BankAccount
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string IBAN { get; set; } 

    public string SWIFT { get; set; } = string.Empty;

    public float Balance { get; set; }

    public string Currency { get; set; } = string.Empty;

    public User User { get; set; } = new User();

    public ICollection<Card>? Cards { get; set; }
}

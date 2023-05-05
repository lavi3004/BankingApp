using BankingApp.Models;
using Microsoft.AspNetCore.Identity;

namespace BankingApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
    //public string FirstName {get; set;}

    //public string LastName { get; set;}

    //public string Adress { get; set; }

    //public string PhoneNumber { get; set; }

    public ICollection<BankAccount>? BankAccounts { get; set; }

}


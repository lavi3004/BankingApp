using DataAccess.Abstactisations;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic;

public class BankAccountService
{
    private readonly IBankAccountRepository bankAccountRepository;
    public BankAccountService(IBankAccountRepository bankAccountRepository)
    {
        this.bankAccountRepository = bankAccountRepository;
    }

    public IEnumerable<BankAccount> GetBankAccounts()
    {
        return bankAccountRepository.GetAll();
    }
}

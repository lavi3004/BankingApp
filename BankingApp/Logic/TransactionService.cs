using DataAccess.Abstactisations;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic;

public class TransactionService
{
    private readonly ITransactionRepository transactionRepository;
    public TransactionService(ITransactionRepository transactionRepository)
    {
        this.transactionRepository = transactionRepository;
    }

    public IEnumerable<Transaction> GetAll()
    {
        return transactionRepository.GetAll();
    }
}

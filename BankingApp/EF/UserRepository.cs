using DataAccess.Abstactisations;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EF;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DbContext dbContext) : base(dbContext) { }

    public void PerformTransaction(Transaction transaction)
    {
        this.dbContext.Add(transaction);
    }
}

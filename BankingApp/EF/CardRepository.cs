using DataAccess.Abstactisations;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EF;

public class CardRepository : BaseRepository<Card>, ICardRepository
{
    public CardRepository(DbContext dbContext) : base(dbContext) { }
}

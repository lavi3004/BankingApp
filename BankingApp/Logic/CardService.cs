using DataAccess.Abstactisations;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic;

public class CardService
{
    private readonly ICardRepository cardRepository;

    public CardService(ICardRepository cardRepository)
    {
        this.cardRepository = cardRepository;
    }
    public IEnumerable<Card> GetCards()
    {
       return this.cardRepository.GetAll();
    }
}

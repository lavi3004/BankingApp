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
    private readonly IUserRepository userRepository;

    public CardService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
}

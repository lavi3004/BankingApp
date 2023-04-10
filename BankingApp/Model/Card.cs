using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model;

public class Card: ModelEntity
{
    public string CardNumber { get; set; } = string.Empty;

    public DateOnly ExpirationDate { get; set; }

    public User CardHolder { get; set; } = new User();

}

using DataAccess.Abstactisations;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic;

public class UserService
{
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository; 
    }

    public IEnumerable<User> GetUsers()
    {
        return userRepository.GetAll();
    }
}

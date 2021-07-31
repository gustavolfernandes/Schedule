using Schedule.domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.domain.Interfaces.Repositories
{
    public interface IUserRepository
    {

        Task<List<User>> Get();
        Task<User> GetById(int id);
        Task<User> Post(User model);
        Task<dynamic> Authenticate(User model);
    }
}

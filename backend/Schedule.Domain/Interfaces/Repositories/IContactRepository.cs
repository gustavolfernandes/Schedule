using Schedule.domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.domain.Interfaces.Repositories
{
    public interface IContactRepository
    {

        Task<Contact> GetById(int id);
        Task<List<Contact>> GetByUserId(int id);
        Task<Contact> Post(Contact model);
        Task<Contact> Put(int id, Contact model);     
        Task<List<Contact>> Delete(int id);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using PersonManagerAPI.Models;

namespace PersonManagerAPI.Data
{
    public interface IAdultService
    {
        
        Task<Adult> AddAdult(Adult adult);
        Task RemoveAdult(int id);
        Task<Adult> UpdateAdult(Adult adult);
        Task<Adult> GetAdult(int id);
        Task<IList<Adult>> GetAll();
    }
}
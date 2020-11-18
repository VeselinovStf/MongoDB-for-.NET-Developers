using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFlix.Data.Abstraction
{
    public interface IRepository<T>
    {
        

        Task<ICollection<T>> GetAllAsync();
    }
}

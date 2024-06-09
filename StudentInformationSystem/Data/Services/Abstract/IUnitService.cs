using System.Collections.Generic;
using System.Threading.Tasks;
using StudentInformationSystem.Models;

namespace StudentInformationSystem.Services
{
    public interface IUnitService
    {
        Task<IEnumerable<Unit>> GetAllUnitsAsync();
        Task<Unit> GetUnitByIdAsync(int id);
        Task AddUnitAsync(Unit unit);
        Task UpdateUnitAsync(Unit unit);
        Task DeleteUnitAsync(int id);
    }
}

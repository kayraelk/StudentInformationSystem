using System.Collections.Generic;
using System.Threading.Tasks;
using StudentInformationSystem.Models;

namespace StudentInformationSystem.Services
{
    public interface IInstructorService
    {
        Task<IEnumerable<Instructor>> GetAllInstructorsAsync();
        Task<Instructor> GetInstructorByIdAsync(int id);
        Task AddInstructorAsync(Instructor instructor);
        Task UpdateInstructorAsync(Instructor instructor);
        Task DeleteInstructorAsync(int id);
    }
}

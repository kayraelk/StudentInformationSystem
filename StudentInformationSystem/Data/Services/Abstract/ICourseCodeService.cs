using System.Collections.Generic;
using System.Threading.Tasks;
using StudentInformationSystem.Models;

namespace StudentInformationSystem.Services
{
    public interface ICourseCodeService
    {
        Task<IEnumerable<CourseCode>> GetAllCourseCodesAsync();
        Task<CourseCode> GetCourseCodeByIdAsync(int id);
        Task AddCourseCodeAsync(CourseCode courseCode);
        Task UpdateCourseCodeAsync(CourseCode courseCode);
        Task DeleteCourseCodeAsync(int id);
    }
}

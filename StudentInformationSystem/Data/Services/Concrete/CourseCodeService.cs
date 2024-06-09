using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Data;
using StudentInformationSystem.Models;

namespace StudentInformationSystem.Services
{
    public class CourseCodeService : ICourseCodeService
    {
        private readonly ApplicationDbContext _context;

        public CourseCodeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseCode>> GetAllCourseCodesAsync()
        {
            return await _context.CourseCodes.ToListAsync();
        }

        public async Task<CourseCode> GetCourseCodeByIdAsync(int id)
        {
            return await _context.CourseCodes.FindAsync(id);
        }

        public async Task AddCourseCodeAsync(CourseCode courseCode)
        {
            _context.CourseCodes.Add(courseCode);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourseCodeAsync(CourseCode courseCode)
        {
            _context.CourseCodes.Update(courseCode);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourseCodeAsync(int id)
        {
            var courseCode = await _context.CourseCodes.FindAsync(id);
            if (courseCode != null)
            {
                _context.CourseCodes.Remove(courseCode);
                await _context.SaveChangesAsync();
            }
        }
    }
}

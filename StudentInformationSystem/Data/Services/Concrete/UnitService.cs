using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Data;
using StudentInformationSystem.Models;

namespace StudentInformationSystem.Services
{
    public class UnitService : IUnitService
    {
        private readonly ApplicationDbContext _context;

        public UnitService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Unit>> GetAllUnitsAsync()
        {
            return await _context.Units
                .Include(u => u.ParentUnit)
                .Include(u => u.ChildUnits)
                .Include(u => u.Courses)
                .ToListAsync();
        }

        public async Task<Unit> GetUnitByIdAsync(int id)
        {
            return await _context.Units
                .Include(u => u.ParentUnit)
                .Include(u => u.ChildUnits)
                .Include(u => u.Courses)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddUnitAsync(Unit unit)
        {
            _context.Units.Add(unit);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUnitAsync(Unit unit)
        {
            _context.Units.Update(unit);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUnitAsync(int id)
        {
            var unit = await _context.Units.FindAsync(id);
            if (unit != null)
            {
                _context.Units.Remove(unit);
                await _context.SaveChangesAsync();
            }
        }
    }
}

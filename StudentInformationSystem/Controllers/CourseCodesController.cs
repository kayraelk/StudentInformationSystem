using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Models;
using StudentInformationSystem.Services;

namespace StudentInformationSystem.Controllers
{
    public class CourseCodesController : Controller
    {
        private readonly ICourseCodeService _courseCodeService;

        public CourseCodesController(ICourseCodeService courseCodeService)
        {
            _courseCodeService = courseCodeService;
        }

        public async Task<IActionResult> Index()
        {
            var courseCodes = await _courseCodeService.GetAllCourseCodesAsync();
            return View(courseCodes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var courseCode = await _courseCodeService.GetCourseCodeByIdAsync(id);
            if (courseCode == null)
            {
                return NotFound();
            }
            return View(courseCode);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCode courseCode)
        {
            if (ModelState.IsValid)
            {
                await _courseCodeService.AddCourseCodeAsync(courseCode);
                return RedirectToAction(nameof(Index));
            }
            return View(courseCode);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var courseCode = await _courseCodeService.GetCourseCodeByIdAsync(id);
            if (courseCode == null)
            {
                return NotFound();
            }
            return View(courseCode);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseCode courseCode)
        {
            if (id != courseCode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _courseCodeService.UpdateCourseCodeAsync(courseCode);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _courseCodeService.GetCourseCodeByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(courseCode);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var courseCode = await _courseCodeService.GetCourseCodeByIdAsync(id);
            if (courseCode == null)
            {
                return NotFound();
            }
            return View(courseCode);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _courseCodeService.DeleteCourseCodeAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

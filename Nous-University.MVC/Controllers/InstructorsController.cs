using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nous_University.DataLayer.Data;
using Nous_University.DataLayer.Entities;

namespace Nous_University.MVC.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly NousUniversityDbContext _context;

        public InstructorsController(NousUniversityDbContext context)
        {
            _context = context;
        }

        // GET: Instructors
        public async Task<IActionResult> Index(int? id, int? courseID)
        {
            var viewModel = new InstructorIndexData
            {
                Instructors = await _context.Instructors
                    .Include(i => i.OfficeAssignment)
                    .Include(i => i.CourseAssignments)
                    .ThenInclude(i => i.Course)
                    .ThenInclude(i => i.Department)
                    .OrderBy(i => i.LastName)
                    .ToListAsync()
            };

            if (id != null)
            {
                ViewData["InstructorID"] = id.Value;
                var instructor = viewModel.Instructors.Single(i => i.ID == id.Value);
                viewModel.Courses = instructor.CourseAssignments.Select(s => s.Course);
            }

            if (courseID == null) return View(viewModel);
            ViewData["CourseID"] = courseID.Value;
            var selectedCourse = viewModel.Courses.Single(x => x.CourseID == courseID);
            await _context.Entry(selectedCourse).Collection(x => x.Enrollments).LoadAsync();
            foreach (var enrollment in selectedCourse.Enrollments)
            {
                await _context.Entry(enrollment).Reference(x => x.Student).LoadAsync();
            }

            viewModel.Enrollments = selectedCourse.Enrollments;

            return View(viewModel);
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // GET: Instructors/Create
        public IActionResult Create()
        {
            var instructor = new Instructor();
            instructor.CourseAssignments = new List<CourseAssignment>();
            PopulateAssignedCourseData(instructor);
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ID,LastName,FirstName,HireDate,OfficeAssignment")] Instructor instructor,
            string[] selectedCourses)
        {
            if (selectedCourses != null)
            {
                foreach (var course in selectedCourses)
                {
                    var courseToAdd = new CourseAssignment
                    {
                        InstructorID = instructor.ID,
                        CourseID = int.Parse(course)
                    };
                    instructor.CourseAssignments.Add(courseToAdd);
                }
            }

            if (!ModelState.IsValid)
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateAssignedCourseData(instructor);
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                .ThenInclude(i => i.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (instructor == null)
            {
                return NotFound();
            }

            PopulateAssignedCourseData(instructor);
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructorToUpdate = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                .ThenInclude(i => i.Course)
                .FirstOrDefaultAsync(s => s.ID == id);

            Debug.Assert(instructorToUpdate != null, nameof(instructorToUpdate) + " != null");
            if (!await TryUpdateModelAsync(instructorToUpdate,
                    "",
                    i => i.FirstName,
                    i => i.LastName,
                    i => i.HireDate,
                    i => i.OfficeAssignment))
            {
                if (string.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment?.Location))
                {
                    instructorToUpdate.OfficeAssignment = null;
                }

                UpdateInstructorCourses(selectedCourses, instructorToUpdate);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes." +
                                                 "Try again, and if the problem persists," +
                                                 "see your system administrator");
                }

                return RedirectToAction(nameof(Index));
            }

            UpdateInstructorCourses(selectedCourses, instructorToUpdate);
            PopulateAssignedCourseData(instructorToUpdate);
            return View(instructorToUpdate);
        }


        // GET: Instructors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructors
                .Include(i => i.CourseAssignments)
                .SingleAsync(i => i.ID == id);

            var departments = await _context.Departments
                .Where(d => d.InstructorID == id)
                .ToListAsync();
            departments.ForEach(d => d.InstructorID = null);

            _context.Instructors.Remove(instructor);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorExists(int id)
        {
            return (_context.Instructors?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        private void PopulateAssignedCourseData(Instructor instructor)
        {
            var allCourses = _context.Courses;
            var instructorCourses = new HashSet<int>(instructor.CourseAssignments
                .Select(c => c.CourseID));
            var viewModel = allCourses.Select(course => new AssignedCoursesData
            {
                CourseID = course.CourseID,
                Title = course.Title,
                Assigned = instructorCourses.Contains(course.CourseID)
            }).ToList();

            ViewData["Courses"] = viewModel;
        }

        private void UpdateInstructorCourses(string[] selectedCourses, Instructor instructorToUpdate)
        {
            if (selectedCourses == null)
            {
                instructorToUpdate.CourseAssignments = new List<CourseAssignment>();
            }

            var selectedCourseHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>(instructorToUpdate.CourseAssignments
                .Select(c => c.CourseID));
            foreach (var course in _context.Courses)
            {
                if (selectedCourseHS.Contains(course.CourseID.ToString()))
                {
                    if (!instructorCourses.Contains(course.CourseID))
                    {
                        instructorToUpdate.CourseAssignments.Add(new CourseAssignment
                        {
                            InstructorID = instructorToUpdate.ID,
                            CourseID = course.CourseID
                        });
                    }
                }
                else
                {
                    if (!instructorCourses.Contains(course.CourseID)) continue;
                    var courseToRemove = instructorToUpdate.CourseAssignments
                        .FirstOrDefault(i => i.CourseID == course.CourseID);
                    _context.Remove(courseToRemove);
                }
            }
        }
    }
}
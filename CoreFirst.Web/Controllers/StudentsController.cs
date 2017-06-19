using CoreFirst.Data.DAL;
using CoreFirst.Data.Entities;
using CoreFirst.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoreFirst.Web.Controllers
{
    public class StudentsController : Controller
    {
        #region >> Constructor <<
        private readonly SchoolContext _context;
        private readonly IStudentService _studentService;

        public StudentsController(SchoolContext context, IStudentService studentService)
        {
            _context = context;
            _studentService = studentService;
        }
        #endregion

        #region >> Studetn List <<
        public async Task<IActionResult> Index()
        {
            var data = await _studentService.GetStudents();
            return View(data);
        }
        #endregion

        #region >> Student Details <<
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }            
            var student = await _studentService.GetStudentDetails(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        } 
        #endregion

        #region >> Student Insert <<
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstMidName,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                bool isValid = await _studentService.CreateStudent(student);
                if (isValid)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(student);
        }
        #endregion
        
        #region >> Student Update <<
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentService.GetStudentById(id.Value);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstMidName,EnrollmentDate")] Student student)
        {
            if (id != student.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool isvalid = await _studentService.StudentEdit(student);
                if (isvalid)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(student);
        }
        #endregion
        
        #region >> Student Delete <<
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentService.GetStudentById(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool isvalid = await _studentService.StudentDelete(id);
            if (isvalid)
            {
                return RedirectToAction("Index");
            }
            //Error Msg:
            return RedirectToAction("Index");
        } 
        #endregion

    }
}

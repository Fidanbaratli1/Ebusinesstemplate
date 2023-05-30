using Ebusinesstemplate.DAL;
using Ebusinesstemplate.Models;
using Ebusinesstemplate.Utilities.FileExtentions;
using Ebusinesstemplate.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ebusinesstemplate.Areas.BusinessAdmin.Controllers
{

    [Area("BusinessAdmin")]
    [AutoValidateAntiforgeryToken]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await _context.Employees.Include(e => e.Position).ToListAsync();
            return View(employees);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Positions = await _context.Positions.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVM employeeVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Positions = await _context.Positions.ToListAsync();
                return View();
            }
            if (!employeeVM.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Photonun tipi uygun deyil");
                ViewBag.Positions = await _context.Positions.ToListAsync();
                return View();
            }
            if (!employeeVM.Photo.CheckFileSize(2048))
            {
                ModelState.AddModelError("Photo", "Photonun olcusu 2mbdan cox ola bilmez");
                ViewBag.Positions = await _context.Positions.ToListAsync();
                return View();
            }
            Employee employee = new Employee
            {
                Name = employeeVM.Name,
                Surname = employeeVM.Surname,
                PositionId = employeeVM.PositionId,

            };
            employee.Image = await employeeVM.Photo.CheckFileAsync(_env.WebRootPath, "assets/img/team");
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            Employee employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) return NotFound();
            employee.Image.DeleteFile(_env.WebRootPath, "assets/img/team");
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            Employee employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) return NotFound();
            UpdateEmployeeVM updateEmployee = new UpdateEmployeeVM
            {
                Name = employee.Name,
                Surname = employee.Surname,
                PositionId = employee.PositionId,
                Image= employee.Image,
            };
            ViewBag.Positions = await _context.Positions.ToListAsync();
            return View(updateEmployee);

        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateEmployeeVM updateEmployee)
        {
            if (id == null || id < 1) return BadRequest();
            Employee existed = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (existed == null) return NotFound();
            if (!ModelState.IsValid)
            {
                ViewBag.Positions = await _context.Positions.ToListAsync();
                return View();
            }
            if (updateEmployee != null)
            {
                if (!updateEmployee.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "Photonun tipi uygun deyil");
                    ViewBag.Positions = await _context.Positions.ToListAsync();
                    return View();
                }
                if (!updateEmployee.Photo.CheckFileSize(2048))
                {
                    ModelState.AddModelError("Photo", "Photonun olcusu 2mbdan cox ola bilmez");
                    ViewBag.Positions = await _context.Positions.ToListAsync();
                    return View();
                }

            }
            existed.Image.DeleteFile(_env.WebRootPath, "assets/img/team");
            existed.Image = await updateEmployee.Photo.CheckFileAsync(_env.WebRootPath, "assets/img/team");
            existed.Name = updateEmployee.Name;
            existed.Surname = updateEmployee.Surname;
            existed.PositionId = updateEmployee.PositionId;
            _context.Employees.Update(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}

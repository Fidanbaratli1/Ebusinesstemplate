using Ebusinesstemplate.DAL;
using Ebusinesstemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Ebusinesstemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
          List<Employee> employees = _context.Employees.Include(e=>e.Position).ToList();
            return View(employees);
        }


    }
}
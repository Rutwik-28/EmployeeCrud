using EmployeeCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeCrud.Controllers
{
    public class SearchController : Controller
    {
        private readonly EmployeeContext _context;

        // context of database
        public SearchController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Search
        public async Task<IActionResult> Index(string searchString)
        {
            // Store the current search string in ViewData to use it in the view
            ViewData["CurrentFilter"] = searchString;

            // If no search string is provided, return an empty list
            if (string.IsNullOrEmpty(searchString))
            {
                return View(new List<Employee>());
            }

            // Initialize the query to get all employees
            var employees = from e in _context.Employees
                            select e;

            // If a search string is provided, filter the employees by name
            if (!string.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(e => e.Name.Contains(searchString));
            }

            // Return the filtered list of employees to the view
            return View(await employees.ToListAsync());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class testsController : Controller
    {
        private readonly TestContext _context;

        public testsController(TestContext context)
        {
            _context = context;
        }

        // GET: tests
        public async Task<IActionResult> Index()
        {
            return View(await _context.tests.ToListAsync());
        }

        // GET: tests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.tests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // GET: tests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: tests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] test test)
        {
            if (ModelState.IsValid)
            {
                SqlConnection cnn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = cnn.CreateCommand();
                cnn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "CreateTest";
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 30).Value = test.Name;
                cmd.ExecuteNonQuery();
                cnn.Close();

                //_context.Add(test);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(test);
        }

        // GET: tests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.tests.FindAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            return View(test);
        }

        // POST: tests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] test test)
        {
            if (id != test.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    SqlConnection cnn = (SqlConnection)_context.Database.GetDbConnection();
                    SqlCommand cmd = cnn.CreateCommand();
                    cnn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "EditarTest";
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 30).Value = test.Name;
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                    //_context.Update(test);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!testExists(test.Id))
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
            return View(test);
        }

        // GET: tests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.tests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // POST: tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SqlConnection cnn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = cnn.CreateCommand();
            cnn.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTest";
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
            cmd.ExecuteNonQuery();
            cnn.Close();
            //var test = await _context.tests.FindAsync(id);
            //_context.tests.Remove(test);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool testExists(int id)
        {
            return _context.tests.Any(e => e.Id == id);
        }
    }
}

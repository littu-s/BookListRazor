using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;           //through DI, v get the val of db
        }

        public IEnumerable<Book> Books { get; set; }

        //Handler. SAME AS Action Meth in MVC
        //async allows u to run multiple tasks at a time until it is awaited
        //here, v need 2 await bcoz v need 2 assign all the books from BooksDB
        public async Task OnGet()
        {            
            Books = await _db.Book.ToListAsync();
        }

        // IActionResult is used bcoz v need 2 redirect the ctrl 2 another pg
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var BookFromDb = await _db.Book.FindAsync(id);
            if(BookFromDb == null)
            {
                return NotFound();
            }
            _db.Book.Remove(BookFromDb);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
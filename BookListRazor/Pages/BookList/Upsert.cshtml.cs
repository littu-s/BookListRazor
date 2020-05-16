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
    public class UpsertModel : PageModel
    {
        private ApplicationDbContext _db;

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;           //through DI, v get the val of db
        }

        [BindProperty]        //it binds the model returning from Create pg to this Book property; so that no need of parameter in meth definition 
        public Book Book { get; set; }


        public async Task<IActionResult> OnGet(int? id)
        {
            //create Page
            Book = new Book();
            if(id == null)
            {
                return Page();
            }

            //update Page
            Book = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);
            if(Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                // Create Page
               if(Book.Id == 0)
                {
                    _db.Book.Add(Book);
                }
               // Update Page
                else
                {
                    _db.Book.Update(Book);
                }

                await _db.SaveChangesAsync();
                return RedirectToPage("EnhancedIndex");
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
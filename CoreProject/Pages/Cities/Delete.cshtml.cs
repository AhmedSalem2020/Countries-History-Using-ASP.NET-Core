using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreProject.Data;

namespace CoreProject.Pages.Cities
{
    public class DeleteModel : PageModel
    {
        ApplicationDbContext db;
        public DeleteModel(ApplicationDbContext _db)
        {
            db = _db;
        }

        [BindProperty]
        public City City { get; set; }

       public IActionResult OnGet(int id)
        {
            City = db.Cities.Include(x => x.Country).FirstOrDefault(x => x.Id == id);
            db.Entry(City).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToPage("./Index");
            
        }

        

        
    }
}

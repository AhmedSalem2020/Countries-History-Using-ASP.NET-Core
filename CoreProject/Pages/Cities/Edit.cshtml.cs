using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreProject.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CoreProject.Pages.Cities
{
    public class EditModel : PageModel
    {
         ApplicationDbContext db;

        IHostingEnvironment _appEnvironment;

        public EditModel(ApplicationDbContext _db, IHostingEnvironment appEnvironment)
        {
            db = _db;
            _appEnvironment = appEnvironment;
        }

        [BindProperty]
        public City City { get; set; }

        public List<City> Cities { get; set; }

        public SelectList Countries { get; set; }

        public void OnGet(int id)
        {
            City = db.Cities.Include(c => c.Country).FirstOrDefault(m => m.Id == id);
            Cities = db.Cities.Include(c => c.Country).ToList();
            Countries = new SelectList(db.Countries.ToList(), "Id", "Name"); // fill in Countries
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var files = HttpContext.Request.Form.Files;
            foreach (var Image in files)
            {
                if (Image != null && Image.Length > 0)
                {
                    var file = Image;
                    //There is an error here
                    var uploads = System.IO.Path.Combine(_appEnvironment.WebRootPath, "uploads\\img");
                    if (file.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            City.Image = fileName;
                        }

                    }
                }
            }
                db.Entry(City).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToPage("./Index");
        }

    }
}

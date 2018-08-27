using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreProject.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CoreProject.Pages.Cities
{
    public class CreateModel : PageModel
    {
        ApplicationDbContext db;

        IHostingEnvironment _appEnvironment;
        

        public CreateModel(ApplicationDbContext _db, IHostingEnvironment appEnvironment)
        {
            db = _db;
            _appEnvironment = appEnvironment;
        }

        [BindProperty]
        public City City { get; set; }

        public SelectList Countries { get; set; }

        public void OnGet()
        {
            Countries = new SelectList(db.Countries, "Id", "Name"); 
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
                            City.Image= fileName;
                        }

                    }
                }
            }
            db.Cities.Add(City);
            db.SaveChanges();
            return RedirectToPage("./Index");
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreProject.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoreProject.Pages.Countries
{
    public class CountryCrudModel : PageModel
    {
        ApplicationDbContext db;
        IHostingEnvironment _appEnvironment;
        public CountryCrudModel(ApplicationDbContext _db, IHostingEnvironment appEnvironment)
        {
            db = _db;
            _appEnvironment = appEnvironment;
        }

        [BindProperty]
        public Country Country { get; set; }

        public List<Country> Countries { get; set; }

        public void OnGet()
        {
            Countries = db.Countries.ToList();

        }

        public void OnGetEdit(int id)
        {
            Country = db.Countries.FirstOrDefault(x => x.Id == id);
            Countries = db.Countries.ToList();
        }

        public async Task<IActionResult> OnPostEdit()
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
                            Country.Image = fileName;
                        }

                    }
                }
            }
            db.Entry(Country).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToPage("./CountryCrud");

            //   return RedirectToAction("OnGet");
            //return CreatedAtAction("ActionMethodName",dto);
            //return CreatedAtRoute( routeName: "GetPage",routeValues: new { id = page.PageId },value: page);
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
                            Country.Image = fileName;
                        }

                    }
                }
            }
            db.Countries.Add(Country);
            db.SaveChanges();
            return RedirectToPage("./CountryCrud");
        }
       
        public IActionResult OnGetDelete(int id)
        {
            Country = db.Countries.FirstOrDefault(x=>x.Id==id);
            db.Entry(Country).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToPage("./CountryCrud");



        }



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreProject.Pages.Countries
{
    public class detailsModel : PageModel
    {
        ApplicationDbContext db;

        public detailsModel(ApplicationDbContext _db)
        {
            db = _db;
        }

        [BindProperty]
        public Country Country { get; set; }

        //public List<Country> Countries { get; set; }

        public void OnGet(int id)
        {
                Country = db.Countries.FirstOrDefault(x => x.Id == id);

        }
        
    }
}
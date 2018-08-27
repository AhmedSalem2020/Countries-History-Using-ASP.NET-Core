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
    public class IndexModel : PageModel
    {
        ApplicationDbContext db;

        public IndexModel(ApplicationDbContext _db)
        {
            db = _db;
        }

        public List<City> Cities { get; set; }

        public void OnGet()
        {
            Cities = db.Cities.Include(c => c.Country).ToList();
        }

        


    }
}

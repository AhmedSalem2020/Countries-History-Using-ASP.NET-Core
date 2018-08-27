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
    public class DetailsModel : PageModel
    {
        ApplicationDbContext db;

        public DetailsModel(ApplicationDbContext _db)
        {
            db = _db;
        }
        public City City { get; set; }

        public List<City> Cities { get; set; }

        public void OnGet(int id)
        {
            City = db.Cities.Include(c => c.Country).FirstOrDefault(m => m.Id == id);
            Cities = db.Cities.Include(c => c.Country).ToList();
        }
    }
}

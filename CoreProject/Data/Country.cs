using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Data
{
    public class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Country Image")]
        public string Image { get; set; }


        public ICollection<City> Cities { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreProject.Data
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Country Image")]
        public string Image { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }


    }
}
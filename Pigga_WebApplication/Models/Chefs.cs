using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pigga_WebApplication.Models
{
    public class Chefs
    {
        public int Id { get; set; }
        [StringLength(25)]
        public string FullName { get; set; }
        [MinLength(10)]
        [MaxLength(50)]
        public string Description { get; set; }

        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile PhotoFile { get; set; }
    }
}

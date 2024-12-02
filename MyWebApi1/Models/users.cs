using System.ComponentModel.DataAnnotations;

namespace MyWebApi1.Models
{
    public class users
    {
        [Key]
        public int id {  get; set; }

        [Required]
        public string? name { get; set; }    
        [Required]
        public string? email { get; set; } 
        [Required]
        public string? mobile { get; set; }
        [Required]
        public string? password { get; set; }
        [Required]
        public string? city { get; set; }

        public string? photo {  get; set; }

    }
}

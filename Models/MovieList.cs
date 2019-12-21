using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace movie_project.Models
{
    public class MovieList
    {
        [Key]
        [Required]
        public int MovieListId { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserRefId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}

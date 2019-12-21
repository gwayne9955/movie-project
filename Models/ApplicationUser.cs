using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace movie_project.Models
{
    // The stuff inside here is added to the existing fields of AspNetUser
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<MovieList> MovieLists { get; set; }
    }
}

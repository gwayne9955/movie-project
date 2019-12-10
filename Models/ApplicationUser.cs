using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movie_project.Models
{
    // The stuff inside here is added to the existing fields of AspNetUser
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<MovieList> MovieLists { get; set; }
    }
}

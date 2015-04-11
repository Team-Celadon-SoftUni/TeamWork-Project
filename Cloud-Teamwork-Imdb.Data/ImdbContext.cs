using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloud_Teamwork_Imdb.Models.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Cloud_Teamwork_Imdb.Models;

namespace Cloud_Teamwork_Imdb.Data
{
    public class ImdbContext : IdentityDbContext<ApplicationUser>
    {
        public ImdbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<MovieType> MovieType { get; set; }
        public virtual DbSet<MovieGenre> MovieGenre { get; set; }
        public virtual DbSet<Actor> Actor { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<Vote> Vote { get; set; }

        public static ImdbContext Create()
        {
            return new ImdbContext();
        }
    }
}

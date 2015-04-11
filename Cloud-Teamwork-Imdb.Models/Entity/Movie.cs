using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_Teamwork_Imdb.Models.Entity
{
    public class Movie
    {
        public Movie()
        {
            this.Actors = new HashSet<Actor>();
            this.Genres = new HashSet<MovieGenre>();
            this.Votes = new HashSet<Vote>();
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        
        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }

        public virtual ICollection<MovieGenre> Genres { get; set; }

        public double Rating { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public virtual MovieType Type { get; set; }
    }
}
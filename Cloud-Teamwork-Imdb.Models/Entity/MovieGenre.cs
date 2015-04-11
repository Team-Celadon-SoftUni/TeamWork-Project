using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_Teamwork_Imdb.Models.Entity
{
    public class MovieGenre
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        public string Name { get; set; }
    }
}

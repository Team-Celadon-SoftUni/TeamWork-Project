using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Cloud_Teamwork_Imdb.Models.Entity
{
    public class MovieType
    {
        [Key]
        public int Id { get; set; }

        public string Type { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Cloud_Teamwork_Imdb.Models.Entity;

namespace Cloud_Teamwork_Imdb.Models.Entity
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }

        public virtual ApplicationUser Voter { get; set; }

        public virtual Movie Movie { get; set; }

        public int Value { get; set; }
    }
}

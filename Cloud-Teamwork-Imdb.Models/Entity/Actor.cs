using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloud_Teamwork_Imdb.Models.Entity;

namespace Cloud_Teamwork_Imdb.Models.Entity
{
    public class Actor : UserProfile
    {
        [Key]
        public int Id { get; set; }

        public string SceneName { get; set; }
        
    }
}

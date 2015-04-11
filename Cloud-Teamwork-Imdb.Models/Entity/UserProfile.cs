using System;

namespace Cloud_Teamwork_Imdb.Models.Entity
{
    public class UserProfile
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }

        public string BirthPlace { get; set; }

        public string Sex { get; set; }
    }
}

namespace SoftUniFAQSystem.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserBindingModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string FullName { get; set; }
        
        public string SoftUniStudentNumber { get; set; }

        public DateTime DateOfRegister { get; set; }
    }
}
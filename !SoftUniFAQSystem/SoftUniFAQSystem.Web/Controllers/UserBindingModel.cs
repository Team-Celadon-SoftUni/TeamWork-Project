namespace SoftUniFAQSystem.Web.Controllers
{
    using System;

    public class UserBindingModel
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string SoftUniStudentNumber { get; set; }
        public DateTime DateOfRegister { get; set; }
    }
}
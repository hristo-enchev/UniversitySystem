using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversitySystem.Entities
{
    public class User : BaseEntityWithID
    {
        public string Username { get; set; }
        
        public string Hash { get; set; }

        public string Salt { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public enum UserType
        {
            Admin,
            Teacher,
            Student
        }
    }
}
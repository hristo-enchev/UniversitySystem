using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Reflection;
using System.Web;
using UniversitySystem.Entities;
using UniversitySystem.Hasher;
using UniversitySystem.Repositories;


namespace UniversitySystem.Services
{
    public class AuthenticationService
    {
        public User LoggedUser { get; private set; }
        
        public bool AuthenticateUser(string username, string password, string type)
        {
            try
            {
                switch (type)
                {
                    case "Admin": UserRepository<Administrator> aRepo = new UserRepository<Administrator>();
                        LoggedUser = aRepo.GetAll(filter: u => u.Username == username).FirstOrDefault();
                        break;
                    case "Teacher": UserRepository<Teacher> tRepo = new UserRepository<Teacher>();
                        LoggedUser = tRepo.GetAll(filter: u => u.Username == username).FirstOrDefault();
                        break;
                    case "Student": UserRepository<Student> sRepo = new UserRepository<Student>();
                        LoggedUser = sRepo.GetAll(filter: u => u.Username == username).FirstOrDefault();
                        break;

                    default:
                        break;
                }
                if (password == null && username == null)
                {
                    LoggedUser = null;
                }
                else if (LoggedUser != null && PasswordHasher.Equals(password,LoggedUser.Salt,LoggedUser.Hash))
                {
                    return LoggedUser != null;
                }
                else
                {
                    LoggedUser = null;
                }
            }
            catch (Exception)
            {
                
                return LoggedUser == null;
            }



            return LoggedUser == null;
        }
    }
}
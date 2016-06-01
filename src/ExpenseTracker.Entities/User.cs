using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ExpenseTracker.Entities
{
    public class User :  EntityBase
    {

        public string Username { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public bool IsLocked { get; set; }
        public DateTime DateCreated { get; set; }

        public ICollection<UserRole> UserRoles
        {
            get;
            set;
        }
    }
}

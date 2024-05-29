using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Diagnostics;
using System;
using PhoneRegister.Models;

namespace PhoneRegister.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<PhoneList> PhoneLists { get; set; }
        public DbSet<PhoneEntry> PhoneEntries { get; set; }
    }
}

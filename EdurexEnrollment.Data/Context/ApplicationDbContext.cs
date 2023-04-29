using EdurexEnrollment.Core.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdurexEnrollment.Data.Context
{
  public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Users> users { get; set; }
        //public DbSet<ProgramOptions> ProgramOptions { get; set; }
        public DbSet<Programs> Programs { get; set; }
        //public DbSet<ProgramCategory> ProgramCategory { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Banks> Banks { get; set; }
        public DbSet<AffiliateUserProgram> AffiliateUserProgram { get; set; }
        public DbSet<AffiliateUserAccount> AffiliateUserAccount { get; set; }
        public DbSet<MarketerCode> MarketerCode { get; set; }
        public DbSet<Marketers> Marketers { get; set; }
        public DbSet<ProgramCategory> ProgramCategory { get; set; }

        public DbSet<Courses> Courses { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Institutions> Institutions { get; set; }
        public DbSet<ProgramOptions> ProgramOptions { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<UserChoices> UserChoices { get; set; }
        public DbSet<UserPaymentHistory> UserPaymentHistory { get; set; }
        public DbSet<UserSubjects> UserSubjects { get; set; }
        public DbSet<UserProgramOption> UserProgramOption { get; set; }
        public DbSet<UserReferred> UserReferred { get; set; }
        public DbSet<UserDiscount> UserDiscount { get; set; }
        public DbSet<DiscountUsageHistory> DiscountUsageHistory { get; set; }

    }
}

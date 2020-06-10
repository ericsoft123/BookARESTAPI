using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MywebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MywebApi.Data
{
    public class BookDbContext : IdentityDbContext<ApplicationUser>
    {
        

        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {

        }

        public DbSet<Plan> plan { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plan>().HasData(
                new Plan
                {
                    id = 1,
                    planId = "plan-1",
                    name = "Basic",
                    text = "Aorem ipsum dolor sit amet,Borem ipsum dolor sit amet,Corem ipsum dolor sit amet",
                    price = 30.0

                },
                new Plan
                {
                    id = 2,
                    planId = "plan-2",
                    name = "Standard",
                    text = "Aorem ipsum dolor sit amet,Borem ipsum dolor sit amet,Corem ipsum dolor sit amet",
                    price = 60.0
                },
                new Plan
                {
                    id = 3,
                    planId = "plan-3",
                    name = "Ultimate",
                    text = "Aorem ipsum dolor sit amet,Borem ipsum dolor sit amet,Corem ipsum dolor sit amet",
                    price = 80.0
                }
            );
            base.OnModelCreating(modelBuilder);

            // modelBuilder.Seed();
        }
    }
}

using MasterMealMind.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMealMind.Repository.DAL
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {            

        }

        public DbSet<Grocerie> Groceries { get; set;}
        public DbSet<Recipe> Recipes { get; set;}

    }
}

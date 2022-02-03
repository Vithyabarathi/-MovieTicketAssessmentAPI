using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieTicketAPI.Models
{
    public class MovieAPIDbContext : DbContext
    {
        public MovieAPIDbContext(DbContextOptions<MovieAPIDbContext> options) : base(options)
        {



        }
        public DbSet<MovieAPI> movieapi { get; set; }
        public DbSet<UserCredentials> usercredentials { get; set; }
    }
}

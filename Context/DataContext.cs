using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HashSaltLogin.Models;
using Microsoft.EntityFrameworkCore;

namespace HashSaltLogin.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }
    }
}
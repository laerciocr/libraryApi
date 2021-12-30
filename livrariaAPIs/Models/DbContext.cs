﻿using Microsoft.EntityFrameworkCore;

namespace livrariaAPIs.Models
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> option) : base(option)
        {
        }

        public DbSet<Product> allProducts { get; set; }
    }
}

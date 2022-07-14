using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using S05E02.MVCLibrary.Models;

namespace S05E02.MVCLibrary.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext (DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<S05E02.MVCLibrary.Models.Book>? Book { get; set; }
    }
}

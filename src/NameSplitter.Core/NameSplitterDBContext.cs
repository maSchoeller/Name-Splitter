
using Microsoft.EntityFrameworkCore;

using NameSplitter.Core.Models;

namespace NameSplitter.Core
{
    public class NameSplitterDBContext : DbContext
    {

        public NameSplitterDBContext(DbContextOptions options) 
            : base(options)
        {

        }

        public DbSet<Title> Titles { get; set; } = null!;
        public DbSet<Salutation> Salutations { get; set; } = null!;
        public DbSet<NameFillingWord> NameFillingWords { get; set; } = null!;
    }
}

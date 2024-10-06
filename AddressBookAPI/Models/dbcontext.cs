using Microsoft.EntityFrameworkCore;

namespace AddressBookAPI.Models
{
    public class dbcontext : DbContext
    {
        public dbcontext(DbContextOptions<dbcontext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasData(
                new Contact() { Id = 1, FirstName = "Jack", LastName = "Jackson", Phone = "111-111-1111", Address = "111 Main Street, Minneapolis, MN 55001"},
                new Contact() { Id = 2, FirstName = "John", LastName = "Johnson", Phone = "222-222-2222", Address = "222 Main Street, Minneapolis, MN 55001"},
                new Contact() { Id = 3, FirstName = "Mary", LastName = "Erickson", Phone = "333-333-3333", Address = "333 Main Street, Minneapolis, MN 55001"}
            );
        }
    }
}

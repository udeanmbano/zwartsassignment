using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ZwartsJWTApi.Authentication
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //added to do list and items to application db context
        public DbSet<ToDoList> toDoLists { get; set; }
        public DbSet<ToDoListItems> toDoListItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ToDoListItems>(table =>
            {
                table.HasKey(x => x.ToDoListItemId);

                table.HasOne(x => x.toDoList)
                .WithMany(x => x.toDoListItems)
                .HasForeignKey(x => x.ToDoListId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
            });
            base.OnModelCreating(builder);
        }

    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoCore.Models;

namespace ToDoInfrastructure.Configurations
{
    public class ToDoListConfiguration : IEntityTypeConfiguration<ToDoList>
    {
        public void Configure(EntityTypeBuilder<ToDoList> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id)
                .HasDefaultValueSql("NEWID()");
            builder.Property(l => l.Title)
                .HasMaxLength(20);
        }
    }
}

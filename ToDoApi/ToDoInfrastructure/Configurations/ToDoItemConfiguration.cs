using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoCore.Models;

namespace ToDoInfrastructure.Configurations
{
    public class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
    {
        public void Configure(EntityTypeBuilder<ToDoItem> builder)
        {
            builder.HasOne(i => i.ToDoList)
                .WithMany(l => l.ItemsList)
                .HasForeignKey(i => i.ListId);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                .HasDefaultValueSql("NEWID()");
            builder.Property(i => i.Content)
                .IsRequired()
                .HasMaxLength(100);

        }
    }
}

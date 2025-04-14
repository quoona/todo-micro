using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rabbit.Domain.Entities;

namespace Rabbit.Infrastructure.EntityConfigurations;

public class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.HasKey(x => x.TodoId);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(255);
        builder.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now);
        builder.Property(x => x.IsCompleted).HasDefaultValue(false);
        builder.Property(x => x.GuidId).HasDefaultValue(Guid.NewGuid());
    }
}
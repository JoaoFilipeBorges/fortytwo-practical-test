using Fortytwo.PracticalTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fortytwo.PracticalTest.Infrastructure.Persistence.Configuration;

public class TodosConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.ToTable(TableNames.TodoTableName);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.Description);
        builder.Property(x => x.AssigneeId);
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.CreatedOn);
        builder.Property(x => x.UpdatedBy);
        builder.Property(x => x.UpdatedOn);
        
        builder.HasOne(x => x.Assignee).WithMany().HasForeignKey(x => x.AssigneeId);
        builder.HasOne(x => x.Author).WithMany().HasForeignKey(x => x.CreatedBy);
    }
}
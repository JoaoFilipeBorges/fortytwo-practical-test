using Fortytwo.PracticalTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fortytwo.PracticalTest.Infrastructure.Persistence.Configuration;

public class UsersConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.UserTableName);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.UserName).IsRequired();
        builder.Property(x => x.ProfilePicUrl).IsRequired();
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.CreatedOn);
        builder.Property(x => x.UpdatedBy);
        builder.Property(x => x.UpdatedOn);
        
        builder.HasIndex(x => x.UserName).IsUnique();
        
    }
}
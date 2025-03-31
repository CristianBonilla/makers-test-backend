using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Makers.Dev.Contracts.SeedData;
using Makers.Dev.Domain.Entities.Auth;

namespace Makers.Dev.Infrastructure.Contexts.MakersDev.Config;

class RoleConfig(ISeedData? seedData = null) : IEntityTypeConfiguration<RoleEntity>
{
  public void Configure(EntityTypeBuilder<RoleEntity> builder)
  {
    builder.ToTable("Role", "dbo")
      .HasKey(key => key.RoleId);
    builder.Property(property => property.RoleId)
      .HasDefaultValueSql("NEWID()");
    builder.Property(property => property.Name)
      .HasMaxLength(30)
      .IsUnicode(false)
      .IsRequired();
    builder.Property(property => property.DisplayName)
      .HasMaxLength(50)
      .IsUnicode(false)
      .IsRequired();
    builder.Property(property => property.Created)
      .HasDefaultValueSql("GETUTCDATE()");
    builder.Property(property => property.Version)
      .IsRowVersion();
    builder.HasMany(many => many.Users)
      .WithOne(one => one.Role)
      .HasForeignKey(key => key.RoleId)
      .OnDelete(DeleteBehavior.Cascade);
    builder.HasIndex(index => new { index.Name, index.DisplayName })
      .IsUnique();
    if (seedData is not null)
      builder.HasData(seedData.Auth.Roles.GetAll());
  }
}

class UserConfig(ISeedData? seedData = null) : IEntityTypeConfiguration<UserEntity>
{
  public void Configure(EntityTypeBuilder<UserEntity> builder)
  {
    builder.ToTable("User", "dbo")
      .HasKey(key => key.UserId);
    builder.Property(property => property.UserId)
      .HasDefaultValueSql("NEWID()");
    builder.Property(property => property.DocumentNumber)
      .HasMaxLength(20)
      .IsUnicode(false)
      .IsRequired();
    builder.Property(property => property.Mobile)
      .HasMaxLength(20)
      .IsUnicode(false)
      .IsRequired();
    builder.Property(property => property.Username)
      .HasMaxLength(100)
      .IsUnicode(false)
      .IsRequired();
    builder.Property(property => property.Password)
      .HasColumnType("varchar(max)")
      .IsRequired();
    builder.Property(property => property.Email)
      .HasMaxLength(100)
      .IsUnicode(false)
      .IsRequired();
    builder.Property(property => property.Firstname)
      .HasMaxLength(50)
      .IsUnicode(false)
      .IsRequired();
    builder.Property(property => property.Lastname)
      .HasMaxLength(50)
      .IsUnicode(false)
      .IsRequired();
    builder.Property(property => property.IsActive)
      .IsRequired();
    builder.Property(property => property.Salt)
      .IsRequired();
    builder.Property(property => property.Created)
      .HasDefaultValueSql("GETUTCDATE()");
    builder.Property(property => property.Version)
      .IsRowVersion();
    builder.HasOne(one => one.Role)
      .WithMany(many => many.Users)
      .HasForeignKey(key => key.RoleId);
    builder.HasMany(many => many.BankLoans)
      .WithOne(one => one.User)
      .HasForeignKey(key => key.UserId)
      .OnDelete(DeleteBehavior.Cascade);
    builder.HasIndex(index => new { index.DocumentNumber, index.Mobile, index.Username, index.Email })
      .IsUnique();
    if (seedData is not null)
      builder.HasData(seedData.Auth.Users.GetAll());
  }
}

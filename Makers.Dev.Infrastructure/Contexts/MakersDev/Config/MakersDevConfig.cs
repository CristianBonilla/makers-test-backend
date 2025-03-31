using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Makers.Dev.Contracts.SeedData;
using Makers.Dev.Domain.Entities;

namespace Makers.Dev.Infrastructure.Contexts.MakersDev.Config;

class BankLoanConfig(ISeedData? seedData = null) : IEntityTypeConfiguration<BankLoanEntity>
{
  public void Configure(EntityTypeBuilder<BankLoanEntity> builder)
  {
    builder.ToTable("BankLoan", "dbo")
      .HasKey(key => key.BankLoanId);
    builder.Property(property => property.BankLoanId)
      .HasDefaultValueSql("gen_random_uuid()");
    builder.Property(property => property.Status)
      .HasConversion(status => status.ToString(), status => Enum.Parse<BankLoanStatus>(status))
      .IsRequired();
    builder.Property(p => p.Amount)
      .HasPrecision(14, 2)
      .IsRequired();;
    builder.Property(property => property.PaymentTerm)
      .IsRequired();
    builder.Property(property => property.Created)
      .HasDefaultValueSql("now()");
    builder.Property(property => property.Version)
      .IsRowVersion();
    builder.HasOne(one => one.User)
      .WithMany(many => many.BankLoans)
      .HasForeignKey(key => key.UserId);
    builder.HasIndex(index => new { index.Status, index.Amount });
    if (seedData is not null)
      builder.HasData(seedData.Makers.BankLoans.GetAll());
  }
}

using Game.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Dal.Mappings
{
  public class AnswerMap : IEntityTypeConfiguration<Answer>
  {
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
      builder.ToTable("Answers");

      builder.HasKey(x => x.Id);

      builder.Property(x => x.Id).ValueGeneratedOnAdd();
      builder.Property(x => x.Text)
        .IsRequired()
        .HasMaxLength(1000);
    }
  }
}

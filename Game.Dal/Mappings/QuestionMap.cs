using Game.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Dal.Mappings
{
  public class QuestionMap : IEntityTypeConfiguration<Question>
  {
    public void Configure(EntityTypeBuilder<Question> builder)
    {
      builder.ToTable("Questions", t =>
      {
        t.HasCheckConstraint(
          "CK_CorrectAnswerInOptions",
          "CorrectAnswerId IN (SELECT Id FROM Answers WHERE QuestionId = Questions.Id)"
        );
      });

      builder.HasKey(x => x.Id);

      builder.Property(x => x.Id).ValueGeneratedOnAdd();
      builder.Property(x => x.Text)
        .IsRequired()
        .HasMaxLength(1000);

      builder.HasOne(x => x.CorrectAnswer)
        .WithMany()
        .HasForeignKey("CorrectAnswerId")
        .OnDelete(DeleteBehavior.Restrict);

      builder.HasMany(x => x.Options)
        .WithOne()
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}

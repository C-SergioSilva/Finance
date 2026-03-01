using Finance.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
        builder.Property(c => c.TypeCategory).IsRequired();

        // Relacionamento: Uma Categoria pertence a um Usuário
        builder.HasOne(c => c.User)
            .WithMany(u => u.Categories)
            .HasForeignKey(c => c.UserId);

        // Os campos da Base (CreateAt, Deleted) o EF já mapeia 
        // automaticamente se você os configurou no DbContext ou aqui.
        builder.Property(t => t.CreateAt)
                .IsRequired();

        builder.HasQueryFilter(t => !t.Deleted);
    }
}
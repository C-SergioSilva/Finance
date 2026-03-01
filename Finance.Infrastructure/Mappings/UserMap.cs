using Finance.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fincance.Infrastructure.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Nome da Tabela
            builder.ToTable("Users");

            // Chave Primária
            builder.HasKey(u => u.Id);

            // Propriedades
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255); // Maior para suportar senhas criptografadas (Hash)

            // Propriedades da EntidadeBase (Auditoria)
            builder.Property(u => u.CreateAt)
                .IsRequired();

            builder.Property(u => u.Deleted)
                .IsRequired()
                .HasDefaultValue(false);

            // Os campos da Base (CreateAt, Deleted) o EF já mapeia 
            // automaticamente se você os configurou no DbContext ou aqui.
            builder.Property(t => t.CreateAt)
                    .IsRequired();

            builder.HasQueryFilter(t => !t.Deleted);

            // Relacionamentos
            // Nota: Os relacionamentos inversos (User tem muitas Transactions/Categories)
            // já foram configurados nos mapas de Transaction e Category, 
            // então não precisamos repetir obrigatoriamente aqui, mas poderíamos.

            // --- RELACIONAMENTOS INVERSOS ---

            // Configurando: Um Usuário tem muitas Categorias
            builder.HasMany(u => u.Categories)      // O "Lado Muitos"
                .WithOne(c => c.User)               // O "Lado Um"
                .HasForeignKey(c => c.UserId)       // A FK que está na tabela de Categorias
                .OnDelete(DeleteBehavior.Restrict); // Regra de segurança

            // Configurando: Um Usuário tem muitas Transações
            builder.HasMany(u => u.Transactions)    // O "Lado Muitos"
                .WithOne(t => t.User)               // O "Lado Um"
                .HasForeignKey(t => t.UserId)       // A FK que está na tabela de Transações
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
using Finance.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fincance.Infrastructure.Mappings
{
    public class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            // Nome da Tabela no Banco
            builder.ToTable("Transactions");

            // Chave Primária
            builder.HasKey(t => t.Id);

            // Propriedades
            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.Date)
                .IsRequired();

            builder.Property(t => t.CreateAt)
                .IsRequired();

            builder.HasQueryFilter(t => !t.Deleted);

            // Mapeamento das Foreign Keys (O Triângulo que você desenhou!)

            // Relacionamento com User
            builder.HasOne(t => t.User)           // Uma Transação tem um Usuário
                .WithMany(u => u.Transactions)    // Um Usuário tem muitas Transações
                .HasForeignKey(t => t.UserId)     // A chave estrangeira é UserId
                .OnDelete(DeleteBehavior.Restrict); // Se deletar o User, não deleta tudo automático (segurança)

            // Relacionamento com Category
            builder.HasOne(t => t.Category)       // Uma Transação tem uma Categoria
                .WithMany(c => c.Transactions)    // Uma Categoria tem muitas Transações
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
using FluxoDeCaixa.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoDeCaixa.Repository.Context
{
    public partial class FluxoDeCaixaContext : DbContext
    {
        public FluxoDeCaixaContext()
        {

        }

        public FluxoDeCaixaContext(DbContextOptions<FluxoDeCaixaContext> options) : base(options)
        {
        }

        public virtual DbSet<Lancamento> Lancamento { get; set; }

        public virtual DbSet<TipoLancamento> TipoLancamento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lancamento>(entity =>
            {
                entity.Property(e => e.Data).HasColumnType("date");
                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Observacao)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Valor).HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.TipoLancamento).WithMany(p => p.Lancamento)
                    .HasForeignKey(d => d.TipoLancamentoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lancamento_TipoLancamento");
            });

            modelBuilder.Entity<TipoLancamento>(entity =>
            {
                entity.Property(e => e.TipoLancamentoId).ValueGeneratedNever();
                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}

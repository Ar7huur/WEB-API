using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Models;

namespace WebAPI.Context {
    public class DataContext :DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }

        public DbSet<Venda> Vendas { get; set; }

       /* public DbSet<Usuario> Usuarios { get; set; }

        public void Configure(EntityTypeBuilder<Usuario> builder) {
            builder.ToTable("Usuario");
            builder.HasKey(p => p.UID);
            builder.Property(p => p.Login)
                .HasColumnType("varchar(20)")
                            .IsRequired();
            builder.Property(p => p.Senha)
                .HasColumnType("varchar(30)")
            .IsRequired();
        }*/



                


        public void Configure(EntityTypeBuilder<Departamento> builder) {
            builder.ToTable("Departamento");
            builder.HasKey(p => p.ID);
            builder.Property(p => p.Nome)
                .HasColumnType("varchar(100)");
        }
        public void Configure(EntityTypeBuilder<Venda> builder) {
            builder.ToTable("VendasRecorde");
            builder.HasKey(p => p.VendaID);
            builder.Property(p => p.VendaData)
                .HasColumnType("varchar(10)");
            builder.Property(p => p.VendaValor)
             .HasColumnType("decimal(10,2)");
        }

        public void Configure(EntityTypeBuilder<Vendedor> builder) {
            builder.ToTable("Sallers");
            builder.HasKey(p => p.VendedorID);
            builder.Property(p => p.VendedorNome)
                .HasColumnType("varchar(10)");
            builder.Property(p => p.VendedorEmail)
             .HasColumnType("varchar(20)");
            builder.Property(p => p.VendedorSalario)
             .HasColumnType("decimal(10,2)");
            

        }


    }
}

using CargasBrasilDB.Domin.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CargasBrasilDB.Mapper.Types
{
    public class DriverMap : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.ToTable("Driver");
            builder.Property(i => i.Id).HasColumnName("id");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Nome)
               .HasColumnName("Nome")
               .IsRequired().HasColumnType("VARCHAR(50)");

            builder.Property(i => i.Cpf)
               .HasColumnName("Cpf")
               .IsRequired().HasColumnType("VARCHAR(20)");

            builder.Property(i => i.Email)
           .HasColumnName("Email")
           .IsRequired().HasColumnType("VARCHAR(80)");

            builder.Property(i => i.Telefone)
            .HasColumnName("Telefone")
            .IsRequired().HasColumnType("VARCHAR(50)");

            builder.Property(i => i.Cep)
            .HasColumnName("Cep")
            .IsRequired().HasColumnType("VARCHAR(50)");

            builder.Property(i => i.Endereco)
            .HasColumnName("Endereco")
            .IsRequired().HasColumnType("VARCHAR(50)");

            builder.Property(i => i.Numero)
            .HasColumnName("Numero")
            .IsRequired().HasColumnType("VARCHAR(50)");

            builder.Property(i => i.FotoHabilitacao)
            .HasColumnName("FotoHabilitacao")
            .IsRequired().HasColumnType("LONGTEXT");

            builder.Property(i => i.FotoDocumentoVeiculo)
            .HasColumnName("FotoDocumentoVeiculo")
            .IsRequired().HasColumnType("LONGTEXT");

            builder.Property(i => i.Estado)
            .HasColumnName("Estado")
            .IsRequired().HasColumnType("VARCHAR(50)");

            builder.Property(i => i.Cidade)
            .HasColumnName("Cidade")
            .IsRequired().HasColumnType("VARCHAR(50)");

            builder.Property(i => i.CadastroAprovado)
           .HasColumnName("CadastroAprovado")
           .IsRequired().HasColumnType("VARCHAR(50)");

            builder.Property(i => i.ModeloVeiculo)
            .HasColumnName("ModeloVeiculo")
            .IsRequired().HasColumnType("VARCHAR(50)");

            builder.Property(i => i.PlacaVeiculo)
            .HasColumnName("PlacaVeiculo")
            .IsRequired().HasColumnType("VARCHAR(50)");

            builder.Property(i => i.Senha)
            .HasColumnName("Senha")
            .IsRequired().HasColumnType("Varchar(80)");


        }
    }
}

﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI.Context;

#nullable disable

namespace WebAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230604210130_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebAPI.Models.Departamento", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Departamentos");
                });

            modelBuilder.Entity("WebAPI.Models.Venda", b =>
                {
                    b.Property<int>("VendaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VendaID"));

                    b.Property<string>("VendaData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("VendaValor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("VendaID");

                    b.ToTable("Vendas");
                });

            modelBuilder.Entity("WebAPI.Models.Vendedor", b =>
                {
                    b.Property<int>("VendedorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VendedorID"));

                    b.Property<string>("VendedorEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VendedorNome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("VendedorSalario")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("VendedorID");

                    b.ToTable("Vendedores");
                });
#pragma warning restore 612, 618
        }
    }
}

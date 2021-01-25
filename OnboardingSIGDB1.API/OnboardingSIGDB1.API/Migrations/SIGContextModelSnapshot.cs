﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnboardingSIGDB1.Data.Context;

namespace OnboardingSIGDB1.API.Migrations
{
    [DbContext(typeof(SIGContext))]
    partial class SIGContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnboardingSIGDB1.Domain.Entities.Cargo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.ToTable("Cargos");
                });

            modelBuilder.Entity("OnboardingSIGDB1.Domain.Entities.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasMaxLength(14);

                    b.Property<DateTime?>("DataFundacao");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("OnboardingSIGDB1.Domain.Entities.Funcionario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.Property<DateTime?>("DataContratacao");

                    b.Property<int?>("EmpresaId");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Funcionarios");
                });

            modelBuilder.Entity("OnboardingSIGDB1.Domain.Entities.FuncionarioCargo", b =>
                {
                    b.Property<int>("FuncionarioId");

                    b.Property<int>("CargoId");

                    b.Property<DateTime>("DataVinculo");

                    b.HasKey("FuncionarioId", "CargoId");

                    b.HasIndex("CargoId");

                    b.ToTable("FuncionariosCargos");
                });

            modelBuilder.Entity("OnboardingSIGDB1.Domain.Entities.Funcionario", b =>
                {
                    b.HasOne("OnboardingSIGDB1.Domain.Entities.Empresa")
                        .WithMany("Funcionarios")
                        .HasForeignKey("EmpresaId");
                });

            modelBuilder.Entity("OnboardingSIGDB1.Domain.Entities.FuncionarioCargo", b =>
                {
                    b.HasOne("OnboardingSIGDB1.Domain.Entities.Cargo", "Cargo")
                        .WithMany("FuncionariosCargos")
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OnboardingSIGDB1.Domain.Entities.Funcionario", "Funcionario")
                        .WithMany("FuncionariosCargos")
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}

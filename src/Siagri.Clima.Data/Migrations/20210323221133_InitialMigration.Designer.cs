﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Siagri.Clima.Data.Context;

namespace Siagri.Clima.Data.Migrations
{
    [DbContext(typeof(SiagriContext))]
    [Migration("20210323221133_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Siagri.Clima.Business.Entities.Localidade", b =>
                {
                    b.Property<Guid>("LocalidadeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Latitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Longitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TemperaturaAtual")
                        .HasColumnType("int");

                    b.HasKey("LocalidadeId");

                    b.HasIndex("LocalidadeId");

                    b.ToTable("Localidades");
                });
#pragma warning restore 612, 618
        }
    }
}

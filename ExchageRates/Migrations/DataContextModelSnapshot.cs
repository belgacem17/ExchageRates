﻿// <auto-generated />
using System;
using ExchageRates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExchageRates.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("ExchageRates.Models.Exchange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateExchange")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExchangeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ExchangeRates")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Exchange");
                });
#pragma warning restore 612, 618
        }
    }
}
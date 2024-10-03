﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ThreeLayer.Data.Context;

#nullable disable

namespace ThreeLayer.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("default")
                .UseCollation("ci_ai_collation")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ThreeLayer.Business.Models.BrazilianPerson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("birth_date");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamptz")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("CreatedByUserId")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("created_by_user_id");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean")
                        .HasColumnName("deleted");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("first_name");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamptz")
                        .HasColumnName("last_modified_at");

                    b.Property<string>("LastModifiedByUserId")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("last_modified_by_user_id");

                    b.Property<long>("RowVersion")
                        .HasColumnType("bigint")
                        .HasColumnName("row_version");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("second_name");

                    b.HasKey("Id")
                        .HasName("pk_brazilian_peoples");

                    b.ToTable("brazilian_peoples", "default");
                });
#pragma warning restore 612, 618
        }
    }
}

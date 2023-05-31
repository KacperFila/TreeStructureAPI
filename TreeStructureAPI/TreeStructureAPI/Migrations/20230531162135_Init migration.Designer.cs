﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TreeStructureAPI.Models;

#nullable disable

namespace TreeStructureAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230531162135_Init migration")]
    partial class Initmigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.4.23259.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TreeStructureAPI.Models.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ParentItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParentItemId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("TreeStructureAPI.Models.Item", b =>
                {
                    b.HasOne("TreeStructureAPI.Models.Item", "ParentItem")
                        .WithMany("ChildItems")
                        .HasForeignKey("ParentItemId");

                    b.Navigation("ParentItem");
                });

            modelBuilder.Entity("TreeStructureAPI.Models.Item", b =>
                {
                    b.Navigation("ChildItems");
                });
#pragma warning restore 612, 618
        }
    }
}
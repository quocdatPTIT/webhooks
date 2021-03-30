﻿// <auto-generated />
using AirlineWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AirlineWeb.Migrations
{
    [DbContext(typeof(AirlineDbContext))]
    [Migration("20210329162537_WebhookInitialMigration")]
    partial class WebhookInitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("AirlineWeb.Model.WebhookSubscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Secret")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebhookPublisher")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebhookType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebhookURI")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WebhookSubscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RapidFarmApi.Database;

#nullable disable

namespace RapidFarmApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RapidFarmApi.Database.Entities.PlantScript", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CurrentInterval")
                        .HasColumnType("integer");

                    b.Property<string>("IntervalsJson")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ScriptName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Scripts");
                });

            modelBuilder.Entity("RapidFarmApi.Database.Entities.State", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("CurrentLight")
                        .HasColumnType("real");

                    b.Property<float>("CurrentTemperature")
                        .HasColumnType("real");

                    b.Property<float>("CurrentWettness")
                        .HasColumnType("real");

                    b.Property<DateTime>("LastWateringTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("WaterPresence")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("StateList");
                });

            modelBuilder.Entity("RapidFarmApi.Database.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TelegramId")
                        .HasColumnType("text");

                    b.Property<bool>("UseTelegramNotification")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}

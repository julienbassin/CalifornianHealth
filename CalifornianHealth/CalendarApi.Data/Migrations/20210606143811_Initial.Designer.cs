﻿// <auto-generated />
using System;
using CalendarApi.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CalendarApi.Data.Migrations
{
    [DbContext(typeof(CalendarDBContext))]
    [Migration("20210606143811_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CalendarApi.Domain.Models.Entities.AppointmentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ConsultantId")
                        .HasColumnType("int");

                    b.Property<int?>("PatientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SelectedDate")
                        .HasColumnType("date")
                        .HasColumnName("StartTime");

                    b.Property<int?>("TimeSlotId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("TimeSlotId");

                    b.HasIndex("ConsultantId", "TimeSlotId", "SelectedDate")
                        .IsUnique()
                        .HasFilter("[ConsultantId] IS NOT NULL AND [TimeSlotId] IS NOT NULL");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("CalendarApi.Domain.Models.Entities.ConsultantModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Speciality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Consultant");
                });

            modelBuilder.Entity("CalendarApi.Domain.Models.Entities.PatientModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Postcode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Patient");
                });

            modelBuilder.Entity("CalendarApi.Domain.Models.Entities.TimeSlotModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ConsultantId")
                        .HasColumnType("int");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ConsultantId");

                    b.ToTable("TimeSlot");
                });

            modelBuilder.Entity("CalendarApi.Domain.Models.Entities.AppointmentModel", b =>
                {
                    b.HasOne("CalendarApi.Domain.Models.Entities.ConsultantModel", "Consultant")
                        .WithMany("Appointments")
                        .HasForeignKey("ConsultantId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("CalendarApi.Domain.Models.Entities.PatientModel", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("CalendarApi.Domain.Models.Entities.TimeSlotModel", "TimeSlot")
                        .WithMany("Appointments")
                        .HasForeignKey("TimeSlotId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Consultant");

                    b.Navigation("Patient");

                    b.Navigation("TimeSlot");
                });

            modelBuilder.Entity("CalendarApi.Domain.Models.Entities.TimeSlotModel", b =>
                {
                    b.HasOne("CalendarApi.Domain.Models.Entities.ConsultantModel", "Consultant")
                        .WithMany("TimeSlots")
                        .HasForeignKey("ConsultantId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Consultant");
                });

            modelBuilder.Entity("CalendarApi.Domain.Models.Entities.ConsultantModel", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("TimeSlots");
                });

            modelBuilder.Entity("CalendarApi.Domain.Models.Entities.PatientModel", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("CalendarApi.Domain.Models.Entities.TimeSlotModel", b =>
                {
                    b.Navigation("Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}

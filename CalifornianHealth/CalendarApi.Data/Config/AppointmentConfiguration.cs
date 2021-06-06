using CalendarApi.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApi.Data.Config
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<AppointmentModel>
    {
        public void Configure(EntityTypeBuilder<AppointmentModel> builder)
        {

            builder.HasKey(a => a.Id);

            builder
                .Property(a => a.SelectedDate)
                .HasColumnName("StartTime")
                .HasColumnType("date")
                .IsRequired();

            builder.HasIndex(a => new { a.ConsultantId, a.TimeSlotId, a.SelectedDate }).IsUnique();

            builder
                .HasOne(a => a.Consultant)
                .WithMany(a => a.Appointments)
                .HasForeignKey(a => a.ConsultantId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(a => a.Patient)
                .WithMany(a => a.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(a => a.TimeSlot)
                .WithMany(a => a.Appointments)
                .HasForeignKey(a => a.TimeSlotId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Appointment");

        }
    }
}

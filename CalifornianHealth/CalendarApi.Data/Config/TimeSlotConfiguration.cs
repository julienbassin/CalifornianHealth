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
    public class TimeSlotConfiguration : IEntityTypeConfiguration<TimeSlotModel>
    {
        public void Configure(EntityTypeBuilder<TimeSlotModel> builder)
        {
            builder.HasKey(ts => ts.Id);

            builder.Property(ts => ts.Time)
                .IsRequired();

            builder.Property(ts => ts.DayOfWeek)
                .IsRequired();

            builder.HasIndex(ts => ts.ConsultantId);

            builder.HasOne(a => a.Consultant)
                .WithMany(a => a.TimeSlots)
                .HasForeignKey(ts => ts.ConsultantId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("TimeSlot");
        }
    }
}

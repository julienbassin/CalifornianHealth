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
    public class ConsultantConfiguration : IEntityTypeConfiguration<ConsultantModel>
    {
        public void Configure(EntityTypeBuilder<ConsultantModel> builder)
        {

            builder.HasKey(a => a.Id);

            builder.Property(c => c.Firstname)
                .IsRequired();

            builder.Property(c => c.Lastname)
                .IsRequired();

            builder.Property(c => c.Speciality)
                .IsRequired();

            builder.ToTable("Consultant");

        }
    }
}

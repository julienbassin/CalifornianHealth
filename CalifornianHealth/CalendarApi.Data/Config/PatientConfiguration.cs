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
    public class PatientConfiguration : IEntityTypeConfiguration<PatientModel>
    {
        public void Configure(EntityTypeBuilder<PatientModel> builder)
        {

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Firstname)
                .IsRequired();

            builder.Property(p => p.Lastname)
                .IsRequired();

            builder.Property(p => p.Address1);

            builder.Property(p => p.Address2);

            builder.Property(p => p.City);

            builder.Property(p => p.Postcode);

            builder.ToTable("Patient");

        }
    }
}

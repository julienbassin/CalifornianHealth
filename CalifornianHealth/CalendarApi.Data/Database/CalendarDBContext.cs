using CalendarApi.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApi.Data.Database
{
    public class CalendarDBContext : DbContext
    {
        public CalendarDBContext(DbContextOptions<CalendarDBContext> options) : base(options) { }

        public DbSet<PatientModel> Patients { get; set; }
        public DbSet<AppointmentModel> Appointments { get; set; }
        public DbSet<ConsultantModel> Consultants { get; set; }
        public DbSet<TimeSlotModel> TimeSlots { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

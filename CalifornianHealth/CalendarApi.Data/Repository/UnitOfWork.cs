using CalendarApi.Data.Database;
using CalendarApi.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApi.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly CalendarDBContext _context;
        public IRepository<PatientModel> _patientRepository;
        public IRepository<ConsultantModel> _consultantRepository;
        public IRepository<TimeSlotModel> _timeslotRepository;
        public IRepository<AppointmentModel> _appointmentRepository;
        public UnitOfWork(CalendarDBContext context)
        {
            _context = context;
        }

        public IRepository<PatientModel> patientRepository =>
            _patientRepository ??= new Repository<PatientModel>(_context);

        public IRepository<ConsultantModel> consultantRepository =>
            _consultantRepository ??= new Repository<ConsultantModel>(_context);

        public IRepository<TimeSlotModel> timeslotRepository =>
            _timeslotRepository ??= new Repository<TimeSlotModel>(_context);

        public IRepository<AppointmentModel> appointmentRepository =>
            _appointmentRepository ??= new Repository<AppointmentModel>(_context);

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task RollBackAsync()
        {
            await _context.DisposeAsync();
        }
    }
}

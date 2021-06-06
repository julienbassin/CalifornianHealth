using CalendarApi.Domain.Models.Entities;
using System.Threading.Tasks;

namespace CalendarApi.Data.Repository
{
    public interface IUnitOfWork
    {
        IRepository<AppointmentModel> appointmentRepository { get; }
        IRepository<ConsultantModel> consultantRepository { get; }
        IRepository<PatientModel> patientRepository { get; }
        IRepository<TimeSlotModel> timeslotRepository { get; }

        Task CommitAsync();
        Task RollBackAsync();
    }
}
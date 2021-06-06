using CalendarApi.Domain.Models.DTOs;
using System.Threading.Tasks;

namespace CalendarApi.Service.v1.Services
{
    public interface IAppointmentUpdateService
    {
        Task SaveAppointment(AppointmentDTO appointment);
    }
}
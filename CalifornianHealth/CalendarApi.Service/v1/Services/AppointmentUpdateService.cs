using CalendarApi.Data.Interfaces;
using CalendarApi.Domain.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApi.Service.v1.Services
{
    public class AppointmentUpdateService : IAppointmentUpdateService
    {
        IAppointmentDataHandler _appointmentDataHandler;
        public AppointmentUpdateService(IAppointmentDataHandler appointmentDataHandler)
        {
            _appointmentDataHandler = appointmentDataHandler;
        }
        public Task SaveAppointment(AppointmentDTO appointment)
        {
            _appointmentDataHandler.SaveAppointment(appointment);
            return Task.CompletedTask;
        }
    }
}

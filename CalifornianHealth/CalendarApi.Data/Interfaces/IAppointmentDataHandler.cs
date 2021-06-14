using CalendarApi.Domain.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApi.Data.Interfaces
{
    public interface IAppointmentDataHandler
    {
        Task SaveAppointment(AppointmentDTO appointmentModel);
    }
}

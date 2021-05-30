using CalendarApi.Domain.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.Messaging.Send.Sender.v1
{
    public interface IAppointmentUpdateSender
    {
        void SendBooking(AppointmentDTO appointmentDTO);
    }
}

using BookingApi.Messaging.Send.Sender.v1;
using CalendarApi.Domain.Models.DTOs;
using CalendarApi.Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookingApi.Service.v1.Command
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, AppointmentDTO>
    {
        IAppointmentUpdateSender _appointmentUpdateSender;
        public CreateAppointmentCommandHandler(IAppointmentUpdateSender appointmentUpdateSender)
        {
            _appointmentUpdateSender = appointmentUpdateSender;
        }

        public Task<AppointmentDTO> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            _appointmentUpdateSender.Send(request.Appointment);
            return Task.FromResult(request.Appointment);
        }
    }
}

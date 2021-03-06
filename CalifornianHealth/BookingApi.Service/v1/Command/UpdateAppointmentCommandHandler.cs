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
    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, AppointmentDTO>
    {
        // Implement IUnitOfWork
        public UpdateAppointmentCommandHandler()
        {

        }

        public Task<AppointmentDTO> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

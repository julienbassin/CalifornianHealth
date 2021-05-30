using CalendarApi.Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookingApi.Service.v1.Query
{
    public class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentsQuery, List<AppointmentModel>>
    {
        // Implement IUnitOfWork 
        public GetAppointmentsQueryHandler()
        {

        }

        Task<List<AppointmentModel>> IRequestHandler<GetAppointmentsQuery, List<AppointmentModel>>.Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

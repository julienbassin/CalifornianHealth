using CalendarApi.Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.Service.v1.Query
{
    public class GetAppointmentByIdQuery : IRequest<AppointmentModel>
    {
        public int Id { get; set; }
    }
}

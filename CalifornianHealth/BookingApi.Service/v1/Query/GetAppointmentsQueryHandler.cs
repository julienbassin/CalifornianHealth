using CalendarApi.Data.Repository;
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
        public readonly IUnitOfWork _unitOfWork;
        public GetAppointmentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<AppointmentModel>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.appointmentRepository.GetAllAsync();
        }
    }
}

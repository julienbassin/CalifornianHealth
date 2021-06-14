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
    public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentModel>
    {
        public readonly IUnitOfWork _unitOfWork;
        public GetAppointmentByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<AppointmentModel> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork.appointmentRepository.GetByIdAsync(request.Id);
        }
    }
}

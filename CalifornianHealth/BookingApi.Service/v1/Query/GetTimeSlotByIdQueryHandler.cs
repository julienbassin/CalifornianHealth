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
    public class GetTimeSlotByIdQueryHandler : IRequestHandler<GetTimeSlotByIdQuery, TimeSlotModel>
    {
        public readonly IUnitOfWork _unitOfWork;
        public GetTimeSlotByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<TimeSlotModel> Handle(GetTimeSlotByIdQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork.timeslotRepository.GetByIdAsync(request.TimeSlotId);
        }
    }
}

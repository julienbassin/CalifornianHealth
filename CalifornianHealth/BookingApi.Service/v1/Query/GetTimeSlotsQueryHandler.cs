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
    public class GetTimeSlotsQueryHandler : IRequestHandler<GetTimeSlotsQuery, List<TimeSlotModel>>
    {
        public readonly IUnitOfWork _unitOfWork;
        public GetTimeSlotsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<List<TimeSlotModel>> Handle(GetTimeSlotsQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork.timeslotRepository.GetAllAsync();
        }
    }
}

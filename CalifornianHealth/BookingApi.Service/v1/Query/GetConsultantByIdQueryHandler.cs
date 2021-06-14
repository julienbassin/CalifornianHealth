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
    public class GetConsultantByIdQueryHandler : IRequestHandler<GetConsultantByIdQuery, ConsultantModel>
    {
        public readonly IUnitOfWork _unitOfWork;
        public GetConsultantByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<ConsultantModel> Handle(GetConsultantByIdQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork.consultantRepository.GetByIdAsync(request.ConsultantId);
        }
    }
}

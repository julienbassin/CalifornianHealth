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
    public class GetConsultantsQueryHandler : IRequestHandler<GetConsultantsQuery, List<ConsultantModel>>
    {
        public readonly IUnitOfWork _unitOfWork;
        public GetConsultantsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<List<ConsultantModel>> Handle(GetConsultantsQuery request, CancellationToken cancellationToken)
        {
            return  _unitOfWork.consultantRepository.GetAllAsync();
        }
    }
}

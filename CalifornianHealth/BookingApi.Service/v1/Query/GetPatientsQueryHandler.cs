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
    public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, List<PatientModel>>
    {
        public IUnitOfWork _unitOfWork;
        public GetPatientsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<List<PatientModel>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork.patientRepository.GetAllAsync();
        }
    }
}

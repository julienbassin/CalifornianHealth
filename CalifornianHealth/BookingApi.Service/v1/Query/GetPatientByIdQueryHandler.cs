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
    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, PatientModel>
    {
        public readonly IUnitOfWork _unitOfWork;
        public GetPatientByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<PatientModel> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork.patientRepository.GetByIdAsync(request.PatientId);
        }
    }
}

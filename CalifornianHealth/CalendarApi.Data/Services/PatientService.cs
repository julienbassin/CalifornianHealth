using AutoMapper;
using CalendarApi.Data.Repository;
using CalendarApi.Data.Services.Interfaces;
using CalendarApi.Domain.Models.DTOs;
using CalendarApi.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApi.Data.Services
{
    public class PatientService : IPatientService
    {
        public readonly IUnitOfWork _unitOfWork;
        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IEnumerable<PatientModel>> GetAllAsync()
        {
            var result = await _unitOfWork.patientRepository.GetAllAsync();
            if (result == null)
            {
                throw new ArgumentException(nameof(result));
            }

            return result;
        }

        public async Task<PatientModel> GetPatientByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }
            var result = await _unitOfWork.patientRepository.GetByIdAsync(id);
            if (result == null)
            {
                throw new ArgumentException(nameof(result));
            }

            return result;
        }
    }
}

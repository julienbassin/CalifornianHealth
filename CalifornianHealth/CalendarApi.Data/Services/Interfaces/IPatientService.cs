using CalendarApi.Domain.Models.DTOs;
using CalendarApi.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApi.Data.Services.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientModel>> GetAllAsync();
        Task<PatientModel> GetPatientByIdAsync(int id);
    }
}

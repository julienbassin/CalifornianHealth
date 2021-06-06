using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApi.Domain.Models.DTOs
{
    public class AppointmentDTO
    {
        public AppointmentDTO()
        {
            Consultants = new List<ConsultantDTO>();
            Patients = new List<PatientDTO>();
        }
        public int SelectedConsultantId { get; set; }
        public int SelectedPatientId { get; set; }

        public DateTime SelectedDate { get; set; }
        public string SelectedTime { get; set; }

        public List<ConsultantDTO> Consultants { get; private set; }

        public List<PatientDTO> Patients { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApi.Domain.Models.DTOs
{
    public class AppointmentDTO
    {
        public int Id;
        public int SelectedConsultantId { get; set; }
        public int SelectedPatientId { get; set; }

        public DateTime SelectedDate { get; set; }
        public string SelectedTime { get; set; }
    }
}

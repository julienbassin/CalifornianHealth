using CalendarApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApi.Domain.Models.Entities
{
    public class TimeSlotModel : IEntityBase
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public int DayOfWeek { get; set; }
        public int? ConsultantId { get; set; }
        public ConsultantModel Consultant { get; set; }
        public virtual ICollection<AppointmentModel> Appointments { get; set; }
    }
}

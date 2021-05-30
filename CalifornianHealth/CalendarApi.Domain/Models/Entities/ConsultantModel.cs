using CalendarApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApi.Domain.Models.Entities
{
    public class ConsultantModel : IEntityBase
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Speciality { get; set; }
        public virtual ICollection<AppointmentModel> Appointments { get; set; }
        public virtual ICollection<TimeSlotModel> TimeSlots { get; set; }
    }
}

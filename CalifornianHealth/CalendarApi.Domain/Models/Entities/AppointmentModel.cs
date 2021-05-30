using CalendarApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApi.Domain.Models.Entities
{
    public class AppointmentModel : IEntityBase
    {
        public int Id { get; set; }

        public DateTime SelectedDate { get; set; }

        public int? ConsultantId { get; set; }

        public int? PatientId { get; set; }

        public int? TimeSlotId { get; set; }
        public virtual PatientModel Patient { get; set; }
        public virtual ConsultantModel Consultant { get; set; }
        public virtual TimeSlotModel TimeSlot { get; set; }
    }
}

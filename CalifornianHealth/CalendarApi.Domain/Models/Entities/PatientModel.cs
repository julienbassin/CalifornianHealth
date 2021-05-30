using CalendarApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApi.Domain.Models.Entities
{
    public class PatientModel : IEntityBase
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string Postcode { get; set; }

        public ICollection<AppointmentModel> Appointments { get; set; }
    }
}

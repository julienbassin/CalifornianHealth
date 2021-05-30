using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApi.Domain.Models.DTOs
{
    public class TimeSlotDTO
    {
        public int Id { get; set; }
        public string Time { get; set; }

        // essayer de convertir dayofweek into string
        public DayOfWeek Day { get; set; }
    }
}

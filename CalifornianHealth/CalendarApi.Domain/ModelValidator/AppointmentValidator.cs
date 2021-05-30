using CalendarApi.Domain.Models.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApi.Domain.ModelValidator
{
    public class AppointmentValidator : ValidatorBase<AppointmentDTO>
    {
        public AppointmentValidator()
        {
            RuleFor(ap => ap.SelectedDate).NotNull();
            RuleFor(ap => ap.SelectedConsultantId).NotEmpty();
            RuleFor(ap => ap.SelectedPatientId).NotEmpty();
            RuleFor(ap => ap.SelectedTime).NotEmpty();
        }
    }
}

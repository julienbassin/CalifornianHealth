using AutoMapper;
using CalendarApi.Domain.Models.DTOs;
using CalendarApi.Domain.Models.Entities;

namespace CalendarApi.Domain.Models.Profiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<AppointmentModel, AppointmentDTO>().ReverseMap();
        }
    }
}

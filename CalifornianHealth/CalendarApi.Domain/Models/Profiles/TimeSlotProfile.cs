using AutoMapper;
using CalendarApi.Domain.Models.DTOs;
using CalendarApi.Domain.Models.Entities;

namespace CalendarApi.Domain.Models.Profiles
{
    public class TimeSlotProfile : Profile
    {
        public TimeSlotProfile()
        {
            CreateMap<TimeSlotModel, TimeSlotDTO>().ReverseMap();
        }
    }
}

using AutoMapper;
using CalendarApi.Domain.Models.DTOs;
using CalendarApi.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApi.Domain.Models.Profiles
{
    public class ConsultantProfile : Profile
    {
        public ConsultantProfile()
        {
            CreateMap<ConsultantModel, ConsultantDTO>().ReverseMap();
        }
    }
}

using CalendarApi.Data.Database;
using CalendarApi.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApi.Data.Test.Infrastructure
{
    public class DatabaseInitializer
    {
        public static void Initialize(CalendarDBContext context)
        {
            if (context.Consultants.Any())
            {
                return;
            }

            Seed(context);
        }

        private static void Seed(CalendarDBContext context)
        {

            ConsultantModel[] consultants =
                {
                    new ConsultantModel
                    {
                        Id = 1,
                        Firstname = "Kobe",
                        Lastname = "Bryant",
                        Speciality = "Heart"
                    },
                    new ConsultantModel
                    {
                        Id = 2,
                        Firstname = "Kyrie",
                        Lastname = "irving",
                        Speciality = "Knee"
                    },
                    new ConsultantModel
                    {
                        Id = 3,
                        Firstname = "Melo",
                        Lastname = "Ball",
                        Speciality = "CoronaVirus"
                    }
                };

            context.Consultants.AddRange(consultants);
            context.SaveChanges();

            PatientModel[] patients =
            {
                new PatientModel
                {
                    Firstname = "Franck",
                    Lastname = "Bassin",
                    Address1 = "address1",
                    Address2 = "address2"
                },
                new PatientModel
                {
                    Firstname = "Jonathan",
                    Lastname = "James",
                    Address1 = "address3",
                    Address2 = "address4"
                }
            };

            context.Patients.AddRange(patients);
            context.SaveChanges();


            for (int i = 1; i <= 5; i++)
            {
                foreach (var consultant in context.Consultants)
                {
                    TimeSlotModel[] timeSlots =
                    {
                            new TimeSlotModel { Time = "08:00", DayOfWeek = i, ConsultantId = consultant.Id },
                            new TimeSlotModel { Time = "08:30", DayOfWeek = i, ConsultantId = consultant.Id },
                            new TimeSlotModel { Time = "09:00", DayOfWeek = i, ConsultantId = consultant.Id },
                            new TimeSlotModel { Time = "09:30", DayOfWeek = i, ConsultantId = consultant.Id },
                            new TimeSlotModel { Time = "10:00", DayOfWeek = i, ConsultantId = consultant.Id },
                            new TimeSlotModel { Time = "10:30", DayOfWeek = i, ConsultantId = consultant.Id },
                            new TimeSlotModel { Time = "11:00", DayOfWeek = i, ConsultantId = consultant.Id },
                            new TimeSlotModel { Time = "11:30", DayOfWeek = i, ConsultantId = consultant.Id },
                            new TimeSlotModel { Time = "13:00", DayOfWeek = i, ConsultantId = consultant.Id },
                            new TimeSlotModel { Time = "13:30", DayOfWeek = i, ConsultantId = consultant.Id },
                            new TimeSlotModel { Time = "14:00", DayOfWeek = i, ConsultantId = consultant.Id },
                            new TimeSlotModel { Time = "14:30", DayOfWeek = i, ConsultantId = consultant.Id },
                            new TimeSlotModel { Time = "15:00", DayOfWeek = i, ConsultantId = consultant.Id },
                            new TimeSlotModel { Time = "15:30", DayOfWeek = i, ConsultantId = consultant.Id },
                            new TimeSlotModel { Time = "16:00", DayOfWeek = i, ConsultantId = consultant.Id },
                            new TimeSlotModel { Time = "16:30", DayOfWeek = i, ConsultantId = consultant.Id },
                            new TimeSlotModel { Time = "17:00", DayOfWeek = i, ConsultantId = consultant.Id },
                            new TimeSlotModel { Time = "17:30", DayOfWeek = i, ConsultantId = consultant.Id },
                            new TimeSlotModel { Time = "18:00", DayOfWeek = i, ConsultantId = consultant.Id },
                            new TimeSlotModel { Time = "18:30", DayOfWeek = i, ConsultantId = consultant.Id }
                        };

                    context.TimeSlots.AddRange(timeSlots);
                }
            }
            context.SaveChanges();
        }
    }
}

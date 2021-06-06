using CalendarApi.Data.Database;
using CalendarApi.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApi.Data.Config
{
    public class SeedData
    {
        public static void Seed(CalendarDBContext dbContext)
        {
            if (!dbContext.Consultants.Any())
            {
                ConsultantModel[] consultants =
                {
                    new ConsultantModel
                    {
                        Firstname = "Blake",
                        Lastname = "Griffin",
                        Speciality = "Heart"
                    },
                    new ConsultantModel
                    {
                        Firstname = "Stephen",
                        Lastname = "Curry",
                        Speciality = "Knee"
                    },
                    new ConsultantModel
                    {
                        Firstname = "Kevin",
                        Lastname = "Durant",
                        Speciality = "CoronaVirus"
                    }
                };

                dbContext.Consultants.AddRange(consultants);
                dbContext.SaveChanges();
            }

            if (!dbContext.Patients.Any())
            {
                PatientModel[] patients =
                {
                    new PatientModel
                    {
                        Firstname = "Julien",
                        Lastname = "Bassin",
                        Address1 = "address1",
                        Address2 = "address2"
                    },
                    new PatientModel
                    {
                        Firstname = "Flavien",
                        Lastname = "Michael",
                        Address1 = "address3",
                        Address2 = "address4"
                    }
                };

                dbContext.Patients.AddRange(patients);
                dbContext.SaveChanges();
            }

            if (!dbContext.TimeSlots.Any())
            {
                for (int i = 1; i <= 5; i++)
                {
                    foreach (var consultant in dbContext.Consultants)
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

                        dbContext.TimeSlots.AddRange(timeSlots);
                    }
                }
                dbContext.SaveChanges();
            }
        }
    }
}

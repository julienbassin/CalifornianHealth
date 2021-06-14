using CalendarApi.Data.Interfaces;
using CalendarApi.Data.Repository;
using CalendarApi.Domain.Models.DTOs;
using CalendarApi.Domain.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApi.Data.Config
{
    public class AppointmentDataHandler : IAppointmentDataHandler
    {
        private readonly string _dbConnectionString;
        private readonly IList<TimeSlotModel> _timeSlots;
        public AppointmentDataHandler(IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
        {
            _dbConnectionString = configuration.GetConnectionString("CalendarDbConnection");
            _timeSlots = new List<TimeSlotModel>();
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var timeSlotRepository = scope.ServiceProvider.GetService<IRepository<TimeSlotModel>>();

                foreach (var timeSlot in timeSlotRepository.GetAllAsync().GetAwaiter().GetResult())
                {
                    _timeSlots.Add(timeSlot);
                }
            }
        }

        public async Task SaveAppointment(AppointmentDTO appointmentModel)
        {
            var selectedtime = appointmentModel.SelectedTime.ToString("hh:mm");

            int? timeSlotId = _timeSlots.SingleOrDefault(ts => ts.Time == selectedtime
                                                           && ts.ConsultantId == appointmentModel.SelectedConsultantId
                                                           && ts.DayOfWeek == (int)appointmentModel.SelectedDate.DayOfWeek)?.Id;

            if (!timeSlotId.HasValue)
            {
                throw new Exception($"Timeslot {appointmentModel.SelectedTime} doesn't exist.");
            }

            string query = @"INSERT INTO dbo.Appointment 
                             (ConsultantId, PatientId, TimeSlotId, SelectedDate) 
                              VALUES (@ConsultantId, @PatientId, @TimeSlotId, @SelectedDate)";

            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ConsultantId", SqlDbType.Int).Value = appointmentModel.SelectedConsultantId;
                    command.Parameters.Add("@PatientId", SqlDbType.Int).Value = appointmentModel.SelectedPatientId;

                    command.Parameters.Add("@TimeSlotId", SqlDbType.Int).Value = timeSlotId.Value;
                    command.Parameters.Add("@SelectedDate", SqlDbType.Date).Value = appointmentModel.SelectedDate;

                    try
                    {
                        await connection.OpenAsync().ConfigureAwait(false);
                        await command.ExecuteNonQueryAsync();
                        await connection.CloseAsync();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2601) // duplicate error
                        {
                            throw new Exception($"Timeslot {appointmentModel.SelectedTime} has been booked already, please select another one.");
                        }

                        throw new Exception(ex.Message);
                    }
                }
            }
        }
    }
}

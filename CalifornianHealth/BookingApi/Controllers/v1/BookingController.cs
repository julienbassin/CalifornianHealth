using AutoMapper;
using BookingApi.Service.v1.Command;
using BookingApi.Service.v1.Query;
using CalendarApi.Domain.Models.DTOs;
using CalendarApi.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApi.Controllers.v1
{
    [Route("v1/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public List<AppointmentModel> _Appointments;
        public BookingController( IMediator mediator)
        {
            _mediator = mediator;
            _Appointments = new List<AppointmentModel>();
        }

        /// <summary>
        /// Action to see all existing appointments.
        /// </summary>
        /// <returns>Returns a list of all customers</returns>
        /// <response code="200">Returned if the appointments were loaded</response>
        /// <response code="400">Returned if the appointments couldn't be loaded</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<List<AppointmentModel>>> Appointments()
        {
            try
            {
                var allAppointments = await _mediator.Send(new GetAppointmentsQuery());
                
                foreach (var currentAppointment in allAppointments)
                {
                    var consultant = await _mediator.Send(new GetConsultantByIdQuery
                    {
                        ConsultantId = (int)currentAppointment.ConsultantId                        
                    });

                    var patient = await _mediator.Send(new GetPatientByIdQuery 
                    {                     
                        PatientId = (int)currentAppointment.PatientId
                    });

                    var timeslot = await _mediator.Send(new GetTimeSlotByIdQuery 
                    { 
                        TimeSlotId = (int)currentAppointment.TimeSlotId
                    
                    });

                    _Appointments.Add(new AppointmentModel 
                    { 
                         Id = currentAppointment.Id,
                         SelectedDate = currentAppointment.SelectedDate,
                         ConsultantId = currentAppointment.ConsultantId,
                         Consultant = consultant,
                         PatientId = currentAppointment.PatientId,
                         Patient = patient,
                         TimeSlotId = currentAppointment.TimeSlotId,
                         TimeSlot = timeslot                         
                    });
                }
                return _Appointments;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<AppointmentDTO>> SendBooking (AppointmentDTO appointmentDTO)
        {
            try
            {

                return await _mediator.Send(new CreateAppointmentCommand
                {
                    Appointment = appointmentDTO
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpGet("Consultants")]
        public async Task<ActionResult<List<ConsultantModel>>> GetConsultants()
        {
            try
            {
                var consultants = await _mediator.Send(new GetConsultantsQuery());
                return consultants;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpGet("Patients")]
        public async Task<ActionResult<List<PatientModel>>> GetPatients()
        {
            try
            {
                var patients = await _mediator.Send(new GetPatientsQuery());
                return patients;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

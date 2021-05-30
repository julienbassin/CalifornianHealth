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
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public BookingController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
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
                return await _mediator.Send(new GetAppointmentsQuery());
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
        public async Task<ActionResult<AppointmentModel>> SaveAppointment(AppointmentDTO appointmentDTO)
        {
            try
            {
                return await _mediator.Send(new CreateAppointmentCommand
                {
                    Appointment = _mapper.Map<AppointmentModel>(appointmentDTO)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

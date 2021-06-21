using BookingApi.Controllers.v1;
using BookingApi.Service.v1.Command;
using BookingApi.Service.v1.Query;
using CalendarApi.Data.Repository;
using CalendarApi.Domain.Models.DTOs;
using CalendarApi.Domain.Models.Entities;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CalendarApi.Test.Controllers.v1
{
    public class BookingControllerTests
    {
        private readonly BookingController _testee;
        private readonly IMediator _mediator;


        private readonly IUnitOfWork _unitOfWork;
        private readonly GetAppointmentByIdQueryHandler _testee2;

        public BookingControllerTests()
        {
            _mediator = A.Fake<IMediator>();
            _testee = new BookingController(_mediator);

            var appointmentDTO = new AppointmentDTO
            {
                SelectedConsultantId = 2,
                SelectedPatientId = 2,
                SelectedTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0),
                SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1)
            };

            var consultants = new List<ConsultantModel>
            {
                 new ConsultantModel
                {
                    Firstname = "Kobe",
                    Lastname = "Bryant",
                    Speciality = "heart"
                },

                 new ConsultantModel
                 {
                     Firstname = "thierry",
                     Lastname = "henry",
                     Speciality = "Back"
                 }
            };

            var patients = new List<PatientModel>
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

            A.CallTo(() => _mediator.Send(A<CreateAppointmentCommand>._, A<CancellationToken>._)).Returns(appointmentDTO);
            A.CallTo(() => _mediator.Send(A<GetConsultantsQuery>._, A<CancellationToken>._)).Returns<List<ConsultantModel>>(consultants);
            A.CallTo(() => _mediator.Send(A<GetPatientsQuery>._, A<CancellationToken>._)).Returns<List<PatientModel>>(patients);
        }

        [Theory]
        [InlineData("CreateAppointment: appointment is null")]
        public async void Appointment_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            AppointmentDTO _appointmentDTO = new AppointmentDTO { };

            A.CallTo(() => _mediator.Send(A<CreateAppointmentCommand>._, default)).Throws(new ArgumentException(exceptionMessage));

            var result = await _testee.SendBooking(_appointmentDTO);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Fact]
        public async void Appointment_ShouldReturnAppointment()
        {
            var result = await _testee.Appointments();

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<List<AppointmentModel>>();
        }

        [Fact]
        public async void Appointments_WhenNoAppointmentsWereFound_ShouldReturnEmptyList()
        {
            A.CallTo(() => _mediator.Send(A<GetAppointmentsQuery>._, A<CancellationToken>._)).Returns(new List<AppointmentModel>());

            var result = await _testee.Appointments();

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<List<AppointmentModel>>();
            result.Value.Count.Should().Be(0);
        }

        [Fact]
        public async void Patients_WhenNoPatientsWereFound_ShouldReturnEmptyList()
        {
            A.CallTo(() => _mediator.Send(A<GetPatientsQuery>._, A<CancellationToken>._)).Returns(new List<PatientModel>());
            var result = await _testee.GetPatients();
            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<List<PatientModel>>();
            result.Value.Count.Should().Be(0);
        }

        [Fact]
        public async void Consultants_WhenNoConsultantsWereFound_ShouldReturnEmptyList()
        {
            A.CallTo(() => _mediator.Send(A<GetConsultantsQuery>._, A<CancellationToken>._)).Returns(new List<ConsultantModel>());
            var result = await _testee.GetConsultants();
            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<List<ConsultantModel>>();
            result.Value.Count.Should().Be(0);
        }

        //[Fact]
        //public async Task Handle_WithValidId_ShouldReturnAppointment()
        //{
        //    var appointment = new AppointmentModel
        //    {
        //        Id = 1,
        //        PatientId = 2,
        //        TimeSlotId = 2,
        //        SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1)
        //    };


        //    A.CallTo(() => _unitOfWork.appointmentRepository.GetByIdAsync(appointment.Id)).Returns(Task.FromResult(appointment));
        //    var result = await _testee2.Handle(new GetAppointmentByIdQuery { Id = appointment.Id }, default);
        //    result.Id.Should().Be(1);
        //}

    }
}

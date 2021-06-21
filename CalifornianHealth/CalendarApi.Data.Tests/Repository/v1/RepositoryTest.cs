using CalendarApi.Data.Database;
using CalendarApi.Data.Repository;
using CalendarApi.Data.Services;
using CalendarApi.Data.Services.Interfaces;
using CalendarApi.Data.Test.Infrastructure;
using CalendarApi.Domain.Models.Entities;
using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CalendarApi.Data.Test.Repository.v1
{
    public class RepositoryTest : DatabaseTest
    {
        //DBContext
        private readonly CalendarDBContext _ctx;

        // Moq repository
        private readonly Repository<PatientModel> _testee;
        private readonly Repository<PatientModel> _testeeFake;
        private readonly PatientModel _newPatient;

        public RepositoryTest()
        {
            _ctx = A.Fake<CalendarDBContext>();
            _testeeFake = new Repository<PatientModel>(_ctx);
            _testee = new Repository<PatientModel>(_ctx);
            _newPatient = new PatientModel
            {
                Id = 1,
                Firstname = "Franck",
                Lastname = "toto",
                Address1 = "address1",
                Address2 = "address2"
            };
        }  


        [Fact]
        public void AddAsync_WhenEntityIsNull_NotBeNull()
        {
            _testee.Invoking(x => x.AddAsync(new PatientModel { })).Should().NotBeNull();
        }

        [Fact]
        public void GetAll_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _ctx.Set<PatientModel>()).Throws<Exception>();

            _testeeFake.Invoking(x => x.GetAllAsync()).Should().Throw<Exception>().WithMessage("Exception of type 'System.Exception' was thrown.");
        }


        [Fact]
        public async void CreatePatientAsync_WhenPatientIsNotNull_ShouldReturnPatient()
        {
            var result = await _testee.AddAsync(_newPatient);

            result.Should().BeOfType<PatientModel>();
        }
    }

}

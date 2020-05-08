using JamPlace.DomainLayer.Exceptions;
using JamPlace.DomainLayer.Interfaces.Models;
using JamPlace.DomainLayer.Interfaces.Repositories;
using JamPlace.DomainLayer.Interfaces.Services;
using JamPlace.DomainLayer.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JamPlace.DomainLayer.Tests
{
    public class JamEventServiceTest
    {
        private readonly IJamEventService _jamEventService;
        private readonly Mock<IJamEventRepository> _jamEventRepositoryMock;
        private readonly Mock<IJamUserRepository> _jamUserRepositoryMock;
        public JamEventServiceTest()
        {
            _jamEventRepositoryMock = new Mock<IJamEventRepository>();
            _jamUserRepositoryMock = new Mock<IJamUserRepository>();
            _jamEventService = new JamEventService(_jamEventRepositoryMock.Object, _jamUserRepositoryMock.Object);
        }

        [Theory]
        [InlineData(-9999)]
        [InlineData(0)]
        [InlineData(286)]
        public void LeaveEvent_Throw_Event_Not_Exsist_Exception_When_Event_With_Id_Not_Found(int eventId)
        {
            _jamEventRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns((IJamEvent)null);
            Assert.Throws<EventNotExsistsException>(() => _jamEventService.LeaveEvent(eventId, string.Empty));
        }
    }
}

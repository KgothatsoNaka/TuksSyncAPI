using Microsoft.AspNetCore.Mvc;
using TuksSyncAPI.Controllers;
using TuksSyncAPI.Models;
using TuksSyncAPI.Repositories;
using Moq;
using Xunit;

namespace TuksSyncAPI.Tests
{
    public class EventInfoContTests
    {
        [Fact]
        public async Task GetEventInfos_ReturnsOkResult()
        {
            // Arrange
            var mockRepository = new Mock<IEventInfoRes>();
            var controller = new EventInfoCont(mockRepository.Object);

            var mockEventInfos = new List<EventInfo>
            {
                new EventInfo { Id = 1, Title = "Event 1" , Location = "Location 1", TicketPrice = 100, },
                new EventInfo { Id = 2, Title = "Event 2" , Location = "Location 2", TicketPrice = 200, }
            };

            mockRepository.Setup(repo => repo.GetEventInfo()).ReturnsAsync(mockEventInfos);

            var controllers = new EventInfoCont(mockRepository.Object);

            // Act
            var result = await controller.GetEventInfos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<EventInfo>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task GetEventInfoById_ReturnsOk_WhenEventExists()
        {
            // Arrange
            var mockRepository = new Mock<IEventInfoRes>();
            var controller = new EventInfoCont(mockRepository.Object);

            var mockEventInfo = new EventInfo { Id = 1, Title = "Event 1", Location = "Location 1", TicketPrice = 100 };

            mockRepository.Setup(repo => repo.GetEventInfoById(1)).ReturnsAsync(mockEventInfo);

            // Act
            var result = await controller.GetEventInfoById(1);

            // Assert
            var okResult = Assert.IsType<ActionResult<EventInfo>>(result);
            var returnValue = Assert.IsType<EventInfo>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetEventInfoById_ReturnsNotFound_WhenEventDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IEventInfoRes>();
            var controller = new EventInfoCont(mockRepository.Object);

            mockRepository.Setup(repo => repo.GetEventInfoById(99)).ReturnsAsync(null as EventInfo);

            // Act
            var result = await controller.GetEventInfoById(99);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
    
}

using Microsoft.AspNetCore.Mvc;
using Moq;
using PVDNOTE.Backend.Core.Entities;
using PVDNOTE.Backend.Infrastructure.Data;
using Xunit;
using Backend.Tests.UnitTests.Utils;
using Microsoft.EntityFrameworkCore;

namespace Backend.Tests.UnitTests.Controllers
{
    public class PageControllerTests
    {
        private readonly Mock<DBContext> mockContext;
        private readonly PageController controller;

        public PageControllerTests()
        {
            var options = new DbContextOptionsBuilder<DBContext>().Options;
            mockContext = new Mock<DBContext>(options);
            controller = new PageController(mockContext.Object);
        }

        [Fact]
        public async Task CreatePage_ReturnsOkWithId_WhenSuccessful()
        {
            var pages = new List<Pages>();
            var blocks = new List<Block>();
            
            var mockPagesSet = DbSetMockHelper.CreateMockDbSet(pages);
            var mockBlocksSet = DbSetMockHelper.CreateMockDbSet(blocks);
            
            mockContext.SetupGet(x => x.Pages).Returns(mockPagesSet.Object);
            mockContext.SetupGet(x => x.Blocks).Returns(mockBlocksSet.Object);
            mockContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

            var dto = new CreatePageDTO { Title = "Test", WorkSpaceId = 1 };
            
            var result = await controller.CreatePage(dto);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            Assert.True(okResult.Value.GetType().GetProperty("Id") != null);
        }

        [Fact]
        public async Task GetPage_ReturnsNotFound_WhenPageNotExists()
        {
            var mockSet = DbSetMockHelper.CreateMockDbSet(new List<Pages>());
            mockContext.SetupGet(x => x.Pages).Returns(mockSet.Object);
            
            var result = await controller.GetPage(1);
            
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
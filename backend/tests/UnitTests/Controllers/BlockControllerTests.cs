using Microsoft.AspNetCore.Mvc;
using Moq;
using PVDNOTE.Backend.Core.Entities;
using PVDNOTE.Backend.DTO;
using PVDNOTE.Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Backend.Tests.UnitTests.Utils;

namespace Backend.Tests.UnitTests.Controllers
{
    public class BlockControllerTests
    {
        private readonly Mock<DBContext> mockContext;
        private readonly BlockController controller;

        public BlockControllerTests()
        {
            var options = new DbContextOptionsBuilder<DBContext>().Options;
            mockContext = new Mock<DBContext>(options);
            controller = new BlockController(mockContext.Object);
        }

        [Fact]
        public async Task CreateBlock_ReturnsOkWithId_WhenSuccessful()
        {
            var blocks = new List<Block>();
            var mockSet = DbSetMockHelper.CreateMockDbSet(blocks);
            
            mockContext.SetupGet(x => x.Blocks).Returns(mockSet.Object);
            mockContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

            var dto = new CreateBlockDTO 
            { 
                Content = "Test", 
                Type = "text", 
                Order = 0, 
                PageId = 1 
            };
            
            var result = await controller.CreateBlock(dto);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            Assert.True(okResult.Value.GetType().GetProperty("Id") != null);
        }

        [Fact]
        public async Task UpdateBlock_ReturnsNotFound_WhenBlockNotExists()
        {
            var mockSet = DbSetMockHelper.CreateMockDbSet(new List<Block>());
            mockContext.SetupGet(x => x.Blocks).Returns(mockSet.Object);

            var dto = new UpdateBlockDTO { Id = 1, Content = "Updated" };
            
            var result = await controller.UpdateBlock(dto);
            
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Moq;
using PVDNOTE.Backend.Core.Entities;
using PVDNOTE.Backend.Infrastructure.Data;
using Xunit;
using Backend.Tests.UnitTests.Utils;
using Microsoft.EntityFrameworkCore;

namespace Backend.Tests.UnitTests.Controllers
{
    public class WorkSpaceControllerTests
    {
        private readonly Mock<DBContext> mockContext;
        private readonly WorkSpaceController controller;

        public WorkSpaceControllerTests()
        {
            var options = new DbContextOptionsBuilder<DBContext>().Options;
            mockContext = new Mock<DBContext>(options);
            controller = new WorkSpaceController(mockContext.Object);
        }

        [Fact]
        public async Task CreateWorkspace_ReturnsOkWithId_WhenSuccessful()
        {
            var workspaces = new List<WorkSpace>();
            var userWorkspaces = new List<UserWorkSpace>();
            
            var mockWorkSpacesSet = DbSetMockHelper.CreateMockDbSet(workspaces);
            var mockUserWorkSpacesSet = DbSetMockHelper.CreateMockDbSet(userWorkspaces);
            
            mockContext.SetupGet(x => x.WorkSpaces).Returns(mockWorkSpacesSet.Object);
            mockContext.SetupGet(x => x.UserWorkSpaces).Returns(mockUserWorkSpacesSet.Object);
            mockContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

            var dto = new CreateWorkSpaceDTO { UserId = 1, Name = "Test" };
            
            var result = await controller.CreateWorkspace(dto);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            Assert.True(okResult.Value.GetType().GetProperty("ID") != null);
        }

        [Fact]
        public async Task GetWorkspace_ReturnsNotFound_WhenWorkspaceNotExists()
        {
            var mockSet = DbSetMockHelper.CreateMockDbSet(new List<WorkSpace>());
            mockContext.SetupGet(x => x.WorkSpaces).Returns(mockSet.Object);
            
            var result = await controller.GetWorkspace(1);
            
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using PVDNOTE.Backend.Core.Entities;
using PVDNOTE.Backend.Infrastructure.Data;
using Xunit;

namespace Backend.Tests.IntegrationTests
{
    public class UserWorkSpaceTests : IAsyncLifetime
    {
        private readonly DBContext context;
        private User testUser;
        private WorkSpace initialWorkSpace;

        public UserWorkSpaceTests()
        {
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: $"UserWorkSpaceTests_{Guid.NewGuid()}")
                .Options;

            context = new DBContext(options);
        }

        public async Task InitializeAsync()
        {
            testUser = new User 
            { 
                Login = "testuser@example.com", 
                Password = "hashed_password" 
            };
            
            initialWorkSpace = new WorkSpace 
            { 
                Name = "Initial Workspace" 
            };
            
            context.UserWorkSpaces.Add(new UserWorkSpace
            {
                User = testUser,
                WorkSpace = initialWorkSpace
            });

            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task Should_Add_New_WorkSpace_For_User()
        {
            var newWorkSpace = new WorkSpace 
            { 
                Name = "New Workspace" 
            };
            
            context.UserWorkSpaces.Add(new UserWorkSpace
            {
                User = testUser,
                WorkSpace = newWorkSpace
            });
            await context.SaveChangesAsync();
            
            var userWorkspaces = await context.UserWorkSpaces
                .Include(uw => uw.WorkSpace)
                .Where(uw => uw.UserId == testUser.Id)
                .ToListAsync();

            Assert.Equal(2, userWorkspaces.Count);
            Assert.Contains(userWorkspaces, uw => uw.WorkSpace.Name == "New Workspace");
        }

        [Fact]
        public async Task Should_Retrieve_All_User_Workspaces()
        {
            var workspaces = await context.UserWorkSpaces
                .Where(uw => uw.UserId == testUser.Id)
                .Include(uw => uw.WorkSpace)
                .Select(uw => uw.WorkSpace)
                .ToListAsync();

            Assert.Single(workspaces);
            Assert.Equal("Initial Workspace", workspaces[0].Name);
        }

        public async Task DisposeAsync()
        {
            await context.DisposeAsync();
        }
    }
}
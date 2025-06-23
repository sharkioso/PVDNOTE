using Microsoft.EntityFrameworkCore;
using PVDNOTE.Backend.Core.Entities;
using PVDNOTE.Backend.Infrastructure.Data;
using Xunit;

namespace Backend.Tests.IntegrationTests
{
    public class BackendIntegrationTests : IAsyncLifetime
    {
        protected readonly DBContext context;
        private User? testUser;
        private WorkSpace? testWorkSpace;
        private Pages? testPage;
        private Block? testBlock;

        protected BackendIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: $"TestDB_{Guid.NewGuid()}")
                .Options;

            context = new DBContext(options);
        }

        public async Task InitializeAsync()
        {
            testUser = new User 
            { 
                Login = "testuser@example.com", 
                Password = "Test123!"
            };

            testWorkSpace = new WorkSpace 
            { 
                Name = "Test Workspace" 
            };

            testPage = new Pages
            {
                Title = "Test Page",
                WorkSpace = testWorkSpace
            };

            testBlock = new Block
            {
                Content = "Initial Content",
                Type = "text",
                Order = 0,
                Access = "public",
                Page = testPage
            };

            context.Users.Add(testUser);
            context.WorkSpaces.Add(testWorkSpace);
            context.Pages.Add(testPage);
            context.Blocks.Add(testBlock);

            await context.SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            await context.DisposeAsync();
        }
    }
}
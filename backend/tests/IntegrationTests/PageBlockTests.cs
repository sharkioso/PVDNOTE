using Microsoft.EntityFrameworkCore;
using PVDNOTE.Backend.Core.Entities;
using PVDNOTE.Backend.Infrastructure.Data;
using Xunit;

namespace Backend.Tests.IntegrationTests
{
    public class PageBlockTests : IAsyncLifetime
    {
        private readonly DBContext context;
        private Pages testPage;
        private const string ExpectedContent = "Test Content";

        public PageBlockTests()
        {
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "PageBlockTests_" + Guid.NewGuid())
                .Options;

            context = new DBContext(options);
        }

        public async Task InitializeAsync()
        {
            var workspace = new WorkSpace { Name = "Test Workspace" };
            testPage = new Pages { Title = "Test Page", WorkSpace = workspace };
            
            context.WorkSpaces.Add(workspace);
            context.Pages.Add(testPage);
            
            context.Blocks.Add(new Block 
            { 
                Content = ExpectedContent,
                Type = "text",
                Order = 0,
                Access = "public",
                Page = testPage
            });

            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task Should_Create_Page_With_Blocks()
        {
            var page = await context.Pages
                .Include(p => p.Blocks)
                .FirstAsync();
            
            Assert.Equal("Test Page", page.Title);
            Assert.Single(page.Blocks);
            Assert.Equal(ExpectedContent, page.Blocks[0].Content);
        }

        [Fact]
        public async Task Should_Add_New_Block_To_Page()
        {
            // Arrange
            var newBlock = new Block
            {
                Content = "New Block Content",
                Type = "code",
                Order = 1,
                Access = "private",
                Page = testPage
            };
            
            context.Blocks.Add(newBlock);
            await context.SaveChangesAsync();
            
            var blocks = await context.Blocks
                .Where(b => b.PageId == testPage.Id)
                .OrderBy(b => b.Order)
                .ToListAsync();

            Assert.Equal(2, blocks.Count);
            Assert.Equal("New Block Content", blocks[1].Content);
        }

        public async Task DisposeAsync()
        {
            await context.DisposeAsync();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using PVDNOTE.Backend.Infrastructure.Data;
using Xunit;

namespace Backend.Tests.UnitTests.Infrastructure
{
    public class DBContextTests
    {
        [Fact]
        public void DBContext_CanBeCreated()
        {
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;
            
            using var context = new DBContext(options);
            Assert.NotNull(context);
        }

        [Fact]
        public void DBContext_HasAllDbSets()
        {
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;
            
            using var context = new DBContext(options);
            
            Assert.NotNull(context.Users);
            Assert.NotNull(context.WorkSpaces);
            Assert.NotNull(context.UserWorkSpaces);
            Assert.NotNull(context.Pages);
            Assert.NotNull(context.Blocks);
        }
    }
}
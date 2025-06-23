using PVDNOTE.Backend.Infrastructure.Data;
using Xunit;

namespace Backend.Tests.UnitTests.Infrastructure
{
    public class DBContextFactoryTests
    {
        [Fact]
        public void CreateDbContext_ReturnsDBContext()
        {
            var factory = new DBContextFactory();
            
            var context = factory.CreateDbContext(null);
            
            Assert.NotNull(context);
            Assert.IsType<DBContext>(context);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Backend.Tests.IntegrationTests;

public class CascadeDeleteTests : BackendIntegrationTests
{
    [Fact]
    public async Task Should_Cascade_Delete_WorkSpace_With_Pages_And_Blocks()
    {
        var workspace = await context.WorkSpaces.FirstAsync();
        
        context.WorkSpaces.Remove(workspace);
        await context.SaveChangesAsync();
        
        Assert.Empty(context.WorkSpaces);
        Assert.Empty(context.Pages);
        Assert.Empty(context.Blocks);
        Assert.NotEmpty(context.Users);
    }
}
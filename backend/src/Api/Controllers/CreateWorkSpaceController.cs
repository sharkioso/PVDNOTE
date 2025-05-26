
using Microsoft.AspNetCore.Mvc;
using PVDNOTE.Backend.Core.Entities;
using PVDNOTE.Backend.Infrastructure.Data;

public class CreateWorkSpaceController : ControllerBase
{
    private readonly DBContext context;

    public CreateWorkSpaceController(DBContext context)
    {
        this.context = context;
    }


    [HttpPost]
    public async Task<IActionResult> CreateWorkspace([FromBody] CreateWorkSpaceDTO dto)
    {
        var workspace = new WorkSpace
        {
            Name = dto.Name
        };

        context.WorkSpaces.Add(workspace);
        await context.SaveChangesAsync();

        var userWorkSpace = new UserWorkSpace
        {
            UserId = dto.UserId,
            WorkSpaceId = workspace.ID
        };

        context.UserWorkSpaces.Add(userWorkSpace);
        await context.SaveChangesAsync();

        return Ok(new { workspace.ID });
    }
}
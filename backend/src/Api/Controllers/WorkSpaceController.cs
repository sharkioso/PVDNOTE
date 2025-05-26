using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PVDNOTE.Backend.Core.Entities;
using PVDNOTE.Backend.Infrastructure.Data;

[ApiController]
[Route("api/[controller]")]
public class WorkSpaceController : ControllerBase
{
    private readonly DBContext context;

    public WorkSpaceController(DBContext context)
    {
        this.context = context;
    }


    [HttpPost("create")]
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



    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserWorkspaces(int userId)
    {
        var workspaces = await context.UserWorkSpaces
            .Where(uw => uw.UserId == userId)
            .Include(uw => uw.WorkSpace)
            .Select(uw => new
            {
                uw.WorkSpace.ID,
                uw.WorkSpace.Name,
                accsessLevel = "owner"

            })
            .ToListAsync();

        return Ok(workspaces);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWorkspace(int id)
    {
        var workspace = await context.WorkSpaces
            .Include(w => w.Pages)
            .FirstOrDefaultAsync(w => w.ID == id);

        if (workspace == null) return NotFound();

        return Ok(new
        {
            workspace.ID,
            workspace.Name,
            Pages = workspace.Pages.Select(p => new
            {
                p.Id,
                p.Title
            })
        });
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PVDNOTE.Backend.Infrastructure.Data;

public class GetWorkSpaceController : ControllerBase
{
    private readonly DBContext context;

    public GetWorkSpaceController(DBContext Context)
    {
        context = Context;
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
                uw.WorkSpace.Name

            })
            .ToListAsync();

        return Ok(workspaces);
    }

    

}
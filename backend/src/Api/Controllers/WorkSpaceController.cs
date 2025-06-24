using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PVDNOTE.Backend.Core.Entities;
using PVDNOTE.Backend.Infrastructure.Data;



// бизнес логику надо чуть позже вынести из контроллеров
[ApiController]
[Route("api/[controller]")]
public class WorkSpaceController : ControllerBase
{
    private readonly DBContext context;
    private readonly WorkSpaceService workSpaceService;

    public WorkSpaceController(DBContext context)
    {
        this.context = context;
        workSpaceService = new WorkSpaceService(context);
    }


    [HttpPost("create")]
    public async Task<IActionResult> CreateWorkspace([FromBody] CreateWorkSpaceDTO dto)
    {
        var workspace = await workSpaceService.CreateWorkspaceService(dto);
        return Ok(new { workspace.ID });
    }



    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserWorkspaces(int userId)
    {
        var workspace = await workSpaceService.GetUserWorkspacesService(userId);
        return Ok(workspace);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWorkspace(int id)
    {   try
        {
            var workspace = await workSpaceService.GetWorkspaceService(id);
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
        catch (ApplicationException)
        {
            return NotFound();
        }

        
    }
}
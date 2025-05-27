using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PVDNOTE.Backend.Infrastructure.Data;
using PVDNOTE.Backend.Core.Entities;


// бизнес логику надо чуть позже вынести из контроллеров
[ApiController]
[Route("api/[controller]")]
public class PageController : ControllerBase
{
    private readonly DBContext context;

    public PageController(DBContext context)
    {
        this.context = context;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePage([FromBody] CreatePageDTO dto)
    {
        var page = new Pages
        {
            Title = dto.Title,
            WorkSpaceId = dto.WorkSpaceId
        };
        context.Pages.Add(page);
        await context.SaveChangesAsync();

        var firstBlock = new Block
        {
            Content = "",
            Type = "text",
            Order = 0,
            PageId = page.Id
        };

        context.Blocks.Add(firstBlock);
        await context.SaveChangesAsync();

        return Ok (new{page.Id});
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPage(int id)
    {
        var page = await context.Pages
            .Include(p => p.Blocks)
            .FirstOrDefaultAsync(p => p.Id == id);


        if (page == null) return NotFound();

        return Ok(new
        {
            page.Id,
            page.Title,
            Block = page.Blocks.OrderBy(b => b.Order).Select(b => new
            {
                b.Id,
                b.Content,
                b.Type,
                b.Order
            })
        });
    }
}
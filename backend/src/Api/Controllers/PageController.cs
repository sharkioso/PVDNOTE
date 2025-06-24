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
    private readonly PageService pageService;

    public PageController(DBContext context)
    {
        this.context = context;
        pageService = new PageService(context);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePage([FromBody] CreatePageDTO dto)
    {
        var page = await pageService.CreatePageService(dto);


        return Ok(new { page.Id });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPage(int id)
    {
        var page = await pageService.GetPageService(id);

        if (page == null) return NotFound();

        return Ok(new
        {
            page.Id,
            page.Title,
            Blocks = page.Blocks.Select(b => new
            {
                b.Id,
                b.Content,
                b.Type,
                b.PageId
            }).ToList() 
        });
    }
}
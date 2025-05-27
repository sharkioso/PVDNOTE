using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PVDNOTE.Backend.Core.Entities;
using PVDNOTE.Backend.DTO;
using PVDNOTE.Backend.Infrastructure.Data;

[ApiController]
[Route("api/[controller]")]
public class BlockController : ControllerBase
{
    private readonly DBContext context;

    public BlockController(DBContext context)
    {
        this.context = context;
    }

    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetBlocks(int id)
    // {
    //     var block = await context.Blocks
    //         .Include(b => b.Content)
    //         .FirstOrDefaultAsync(b => b.Id == id);

    //     if (block == null) return NotFound();

    //     return Ok(new
    //     {
    //         block.Id,
    //         block.
    //     })       

    // }

    [HttpPost("create")]
    public async Task<IActionResult> CreateBlock([FromBody] CreateBlockDTO dto)
    {
        var block = new Block
        {
            Content = dto.Content,
            Type = dto.Type,
            Order = dto.Order,
            PageId = dto.PageId
        };

        context.Blocks.Add(block);
        await context.SaveChangesAsync();

        return Ok(new { block.Id });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateBlock([FromBody] UpdateBlockDTO dto)
    {
        var block = await context.Blocks.FindAsync(dto.Id);
        if (block == null) return NotFound();

        block.Content = dto.Content;
        await context.SaveChangesAsync();

        return Ok();
    }
}
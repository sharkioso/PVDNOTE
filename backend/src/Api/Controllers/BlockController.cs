using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PVDNOTE.Backend.Core.Entities;
using PVDNOTE.Backend.Api.DTO;
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


    [HttpPost("create")]
    public async Task<IActionResult> CreateBlock([FromBody] CreateBlockDTO dto)
    {
        
        var block = new Block
        {
            Content = dto.Content,
            Type = dto.Type,
            PageId = dto.PageId
        };

        context.Blocks.Add(block);
        await context.SaveChangesAsync();

        return Ok(new { block.Id });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateBlock([FromBody] UpdateBlockDTO dto)
    {
        
Console.WriteLine($"Updating block {dto.Id}: Content='{dto.Content}'");
        var block = await context.Blocks.FindAsync(dto.Id);
        if (block == null) return NotFound();

        block.Content = dto.Content;
        await context.SaveChangesAsync();

        return Ok();
    }
}
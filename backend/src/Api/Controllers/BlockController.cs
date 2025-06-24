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
    private readonly BlockService blockService;

    public BlockController(DBContext context)
    {
        this.context = context;
        blockService = new BlockService(context);
    }


    [HttpPost("create")]
    public async Task<IActionResult> CreateBlock([FromBody] CreateBlockDTO dto)
    {
        var block = await blockService.CreateBlockService(dto);
        return Ok(new { block.Id });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateBlock([FromBody] UpdateBlockDTO dto)
    {
        try
        {
            await blockService.UpdateBlockService(dto);
            return Ok();
        }
        catch (ApplicationException)
        {
            return NotFound();
        }
        
    }
}
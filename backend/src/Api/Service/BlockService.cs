using PVDNOTE.Backend.Api.DTO;
using PVDNOTE.Backend.Core.Entities;
using PVDNOTE.Backend.Infrastructure.Data;

public class BlockService
{
    private readonly DBContext context;

    public BlockService(DBContext context)
    {
        this.context = context;
    }

    public async Task<Block> CreateBlockService(CreateBlockDTO dto)
    {

        var block = new Block
        {
            Content = dto.Content,
            Type = dto.Type,
            PageId = dto.PageId
        };

        context.Blocks.Add(block);
        await context.SaveChangesAsync();

        return block;
    }
    
    public async Task UpdateBlockService( UpdateBlockDTO dto)
    {
        
        var block = await context.Blocks.FindAsync(dto.Id);
        if (block == null)
            throw new ApplicationException("Block not found");

        block.Content = dto.Content;
        await context.SaveChangesAsync();
    }


}
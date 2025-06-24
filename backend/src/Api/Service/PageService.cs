
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PVDNOTE.Backend.Core.Entities;
using PVDNOTE.Backend.Infrastructure.Data;

public class PageService
{
    private readonly DBContext context;

    public PageService(DBContext dbContext)
    {
        context = dbContext;
    }

    public async Task<Pages> CreatePageService(CreatePageDTO dto)
    {
        var page = new Pages
        {
            Title = dto.Title,
            WorkSpaceId = dto.WorkSpaceId
        };
        context.Pages.Add(page);
        await context.SaveChangesAsync();


        return page;
    }

    public async Task<Pages> GetPageService(int id)
    {
        var page = await context.Pages
            .Include(p => p.Blocks)
            .FirstOrDefaultAsync(p => p.Id == id);

        return page;
    }

}
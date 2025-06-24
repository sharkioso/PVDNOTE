using Microsoft.EntityFrameworkCore;
using PVDNOTE.Backend.Core.Entities;
using PVDNOTE.Backend.Infrastructure.Data;

public class WorkSpaceService
{
    private readonly DBContext context;

    public WorkSpaceService(DBContext dBContext)
    {
        this.context = dBContext;

    }

    public async Task<WorkSpace> CreateWorkspaceService(CreateWorkSpaceDTO dto)
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

        return workspace;
    }

    public async Task<List<object>> GetUserWorkspacesService(int userId)
    {
        return await context.UserWorkSpaces
            .Where(uw => uw.UserId == userId)
            .Include(uw => uw.WorkSpace)
            .Select(uw => new
            {
                uw.WorkSpace.ID,
                uw.WorkSpace.Name,
                accsessLevel = "owner"

            })
            .ToListAsync<object>();

    }
    

    public async Task<WorkSpace> GetWorkspaceService(int id)
    {
        var workspace = await context.WorkSpaces
            .Include(w => w.Pages)
            .FirstOrDefaultAsync(w => w.ID == id);

        if (workspace == null)
            throw new ApplicationException("Workspace not found");

        return workspace;
    }

}
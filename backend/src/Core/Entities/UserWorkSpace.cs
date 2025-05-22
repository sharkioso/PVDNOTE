namespace PVDNOTE.Backend.Core.Entities;

public class UserWorkSpace
{

    public int UserId { get; set; }
    public int WorkSpaceId { get; set; }

    public User User { get; set; }
    public WorkSpace WorkSpace { get; set; }


}
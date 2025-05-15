namespace PVDNOTE.Backend.Core.Entities;

public class WorkSpace
{
    public int ID { get; set; }
    public List<UserWorkSpace> UserWorkSpaces { get; set; }
    
}
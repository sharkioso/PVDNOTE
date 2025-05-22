namespace PVDNOTE.Backend.Core.Entities;

public class Page
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int WorkSpaceId { get; set; }
    public List<Block> Blocks { get; set; }
    
    public WorkSpace WorkSpace{ get; set; }

}
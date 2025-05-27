namespace PVDNOTE.Backend.DTO;

public class CreateBlockDTO
{
    public string Content { get; set; }
    public string Type { get; set; }
    public int Order { get; set; }
    public int PageId { get; set; }
}
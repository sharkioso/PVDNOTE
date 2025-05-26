using System.ComponentModel.DataAnnotations.Schema;

namespace PVDNOTE.Backend.Core.Entities;

public class Block
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int PageId { get; set; }
    public string Content { get; set; }
    public string Access { get; set; }
    public string Type { get; set; }
    public int Order{ get; set; }

    public Pages Page { get; set; }
}
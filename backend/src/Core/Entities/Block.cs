using System.ComponentModel.DataAnnotations.Schema;

namespace PVDNOTE.Backend.Core.Entities;

public class Block
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int PageId { get; set; }
    public string Context { get; set; }
    public string Access { get; set; }

    public Page Page{ get; set; }
}
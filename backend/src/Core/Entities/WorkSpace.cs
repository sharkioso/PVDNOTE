using System.ComponentModel.DataAnnotations.Schema;

namespace PVDNOTE.Backend.Core.Entities;

public class WorkSpace
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    public string Name{ get; set; }
    public List<UserWorkSpace> UserWorkSpaces { get; set; }
    public List<Pages> Pages { get; set; }
    
}
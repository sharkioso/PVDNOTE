using System.ComponentModel.DataAnnotations.Schema;

namespace PVDNOTE.Backend.Core.Entities;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public List<UserWorkSpace> UserWorkSpaces { get; set; }

}
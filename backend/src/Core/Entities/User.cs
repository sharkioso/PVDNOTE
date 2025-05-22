namespace PVDNOTE.Backend.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public List<UserWorkSpace> UserWorkSpaces { get; set; }

}
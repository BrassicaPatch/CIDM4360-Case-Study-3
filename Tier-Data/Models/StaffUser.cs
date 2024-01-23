namespace Mailroom.DataTier.Models;

public class StaffUser {
    public int ID {get; set;}
    public string UserName {get; set;} = null!;
    public string Password {get; set;} = null!;
}
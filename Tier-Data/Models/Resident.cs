namespace Mailroom.DataTier.Models;

public class Resident {
    public int ID {get; set;}
    public string FullName {get; set;} = null!;
    public string Email {get; set;} = null!;
    public int UnitNumber {get; set;}
    public List<Package> Packages {get; set;} = new();
}
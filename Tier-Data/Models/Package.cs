namespace Mailroom.DataTier.Models;

public class Package {
    public int ID {get; set;}
    public string TrackingNumber {get; set;} = null!;
    public string FullName {get; set;} = null!;
    public string FullAddress {get; set;} = null!;
    public DateTime DeliveryDate {get; set;}
    public Service Service {get; set;} = Service.Unassigned;
    public Status Status {get; set;} = Status.AwaitingStatus;
}

public enum Service {
    Unassigned,
    FedEx,
    USPS,
    UPS,
    Other
}

public enum Status {
    AwaitingStatus,
    Pending, 
    Unknown, 
    PickedUp, 
    Returned
}
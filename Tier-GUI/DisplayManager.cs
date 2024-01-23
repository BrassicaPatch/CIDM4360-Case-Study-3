using Mailroom.DataTier.Models;

namespace Mailroom.GUITier;

public static class DisplayManager {

    public static View View = View.Main;

    public static void DrawLoginUsername(bool error = false){
        Console.Clear();
        if(error) Write("Ivalid username or password. Please try again.");
        else Write("Welcome!");
        Write("Please input Username: ");
    }

    public static void DrawLoginPassword(){
        Console.Clear();
        Write("Please input Password: ");
    }

    public static void DrawMainPage(bool error = false, string errorMsg = "", List<Package>? Packages = null, List<Resident>? residents = null, Resident? resident = null, Package? newPackage = null, PackageView packageView=PackageView.Default){
        Console.Clear();
        DrawBanner();
        Console.WriteLine();

        if(View==View.Main) DrawMain();
        else if(View==View.AddPackage) DrawAddPackage(newPackage, packageView);
        else if(View==View.Packages) DrawPackages(Packages);
        else if(View==View.AllResidents) DrawAllResidents(residents);
        else if(View==View.Resident) DrawResident(resident);

        if(error){
            if(!String.IsNullOrEmpty(errorMsg)){
                Write($"\n{errorMsg}");
                Write($"\nInput:");
            }
            else {
                Write($"\nInvalid Input");
                Write($"\nInput:");
            }
        }
        else Write($"\nInput:");
    }

    static void DrawMain(){
        Write("Listed below are the commands as they would be entered into the console, with brackets distinquishing options for input");
        Console.WriteLine(); 
        LWriteText("Home - Return to this screen.");
        LWriteText("AddPackage - To add new package to system.");
        LWriteText("ViewPackages - View list of all packages and their status.");
        LWriteText("UpdatePackageStatus {PackageID} {Await, Pending, Unkown, PickedUp, Returned} - Update package status.");
        LWriteText("ViewAllResidents - View all residents and their information.");
        LWriteText("ViewResident {ResidentID} - View Resident Package History.");
        LWriteText("AssignPackage {PackageID} {ResidentID} - Assigns package to resident, updating status, and sending email notification.");
        LWriteText("Quit - Quits the application.");
    }

    static void DrawAddPackage(Package? package, PackageView view){
        if(package==null) {
            View=View.Main;
            DrawMainPage(error:true, errorMsg:"Package could not be found");
            return;
        }

        Write("Add new package to system:");
        LWrite($"Package Tracking Number: {package.TrackingNumber ?? "None"}");
        LWrite($"Package Receiver Full Name: {package.FullName ?? "None"}");
        LWrite($"Package Delivery Address: {package.FullAddress ?? "None"}");
        LWrite($"Package Delivery Date: {package.DeliveryDate.ToShortDateString()}");
        LWrite($"Package Delivery Service: {package.Service}");
        LWrite($"Package Status: {package.Status}");
        Console.WriteLine();

        if(view==PackageView.Default){
            LWriteText("1) Set Package Tracking Number");
            LWriteText("2) Set Package Receiver Name");
            LWriteText("3) Set Package Delivery Address");
            LWriteText("4) Set Package Delivery Service");
            LWriteText("5) Complete Package Submission");
            LWriteText("6) Cancel");
        }
        else if(view==PackageView.TrackingNumber) LWriteText("Enter Tracking Number");
        else if(view==PackageView.Name) LWriteText("Enter Full Receiver Name");
        else if(view==PackageView.Address) LWriteText("Enter Full Delivery Address");
        else if(view==PackageView.Service) LWriteText("Enter Delivery Service");
    }



    static void DrawPackages(List<Package>? packages){
        if(packages==null || packages.Count()==0) {
            View=View.Main;
            DrawMainPage(true, "No packages could be found to display.");
            return;
        }
        foreach(var p in packages){
            PackageWriter(p);
        }
    }

    static void PackageWriter(Package p){
        DrawBar();
        Console.WriteLine();
        LWrite($"[{p.ID}] {p.TrackingNumber}");
        LWriteText($"Shipped to: {p.FullName} {p.FullAddress}");
        LWriteText($"Deliverd on: {p.DeliveryDate.ToShortDateString()}");
        LWriteText($"Shipping Service: {p.Service}");
        LWriteText($"Status: {p.Status}");
    }

    static void DrawAllResidents(List<Resident>? residents){
        if(residents==null || residents.Count()==0) {
            View=View.Main;
            DrawMainPage(true, "No residents could be found to display.");
            return;
        }
        foreach(var r in residents){
            LWrite($"[{r.ID}] {r.FullName} - Email: {r.Email} - Unit: {r.UnitNumber} - Packages: {r.Packages.Count()} - Pending: {r.Packages.Where(p=>p.Status==Status.Pending).Count()}");
        }
    }

    static void DrawResident(Resident? resident) {
        if(resident==null){
            View=View.Main;
            DrawMainPage(true, "Resident could not be found");
            return;
        }
        LWrite($"{resident.FullName}");
        LWriteText($"Email: {resident.Email}");
        LWriteText($"Unit Number: {resident.UnitNumber}");
        LWriteText($"Number of Packages: {resident.Packages.Count()}");
        LWriteText($"Number of Pending Packages: {resident.Packages.Where(p=>p.Status==Status.Pending).Count()}");
        Console.WriteLine();
        foreach(var p in resident.Packages.OrderBy(p=>p.DeliveryDate)){
            PackageWriter(p);
        }
    }

    static void DrawBanner(){
        Write("##     ##    ###    #### ##       ##     ##    ###     ######  ######## ######## ########  ");
        Write("###   ###   ## ##    ##  ##       ###   ###   ## ##   ##    ##    ##    ##       ##     ## ");
        Write("#### ####  ##   ##   ##  ##       #### ####  ##   ##  ##          ##    ##       ##     ## ");
        Write("## ### ## ##     ##  ##  ##       ## ### ## ##     ##  ######     ##    ######   ########  ");
        Write("##     ## #########  ##  ##       ##     ## #########       ##    ##    ##       ##   ##   ");
        Write("##     ## ##     ##  ##  ##       ##     ## ##     ## ##    ##    ##    ##       ##    ##  ");
        Write("##     ## ##     ## #### ######## ##     ## ##     ##  ######     ##    ######## ##     ## ");
        Console.WriteLine();
        DrawBar();
        Console.WriteLine();
    }

    static void DrawBar(){
        string bar = "";
        for(int i=0; i<(((Console.WindowWidth/3)*2)-(Console.WindowWidth/3)); i++){
            bar+="#";
        }
        Write(bar);
    }

    static void Write(string s){
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (s.Length / 2)) + "}", s));
    }

    static void LWrite(string s){
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 3) + (s.Length)) + "}", s));
    }

    static void LWriteText(string s){
        int rightBound = (Console.WindowWidth/3)*2;
        Console.WriteLine();
        var words = s.Split(' ');

        for(int i=0; i<words.Count();){
            string line = $"\t";
            for(int j = i; j<words.Count();){
                if((Console.WindowWidth / 3)+line.Count()+$"{words[i] }".Count() > rightBound) break;
                line += $"{words[i]} ";
                i++;
                j++;
            }
            LWrite(line);
        }
    }

    public static void DrawQuit(){
        Console.Clear();
        Console.WriteLine("Application Quit");
    }
}

public enum View {
    Main,
    AddPackage,
    Packages,
    AllResidents,
    Resident
}

public enum PackageView {
    Default,
    TrackingNumber,
    Name,
    Address,
    Service
}
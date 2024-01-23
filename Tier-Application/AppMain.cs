using Mailroom.DataTier;
using Mailroom.DataTier.Models;
using Mailroom.GUITier;

namespace Mailroom.AppTier;

public static class AppMain {

    public static void LoginProcedure(){
        bool error=false;

        username:
        DisplayManager.DrawLoginUsername(error);
        error=false;
        var username = Console.ReadLine();
        if(String.IsNullOrEmpty(username)) goto username;

        password:
        DisplayManager.DrawLoginPassword();
        var password = Console.ReadLine();
        if(String.IsNullOrEmpty(password)) goto password;

        var success = DataMain.LogIn(username, password);
        if(!success){
            error=true;
            goto username;
        }

        StartSystem();
    }

    static void StartSystem(){
        DisplayManager.DrawMainPage();
        InputListener();
    }

    static void InputListener(){
        var listening = true;

        while(listening) {
            var rawInput = Console.ReadLine();
            if(String.IsNullOrEmpty(rawInput)) continue;

            var inputs = rawInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var front = inputs[0]; 

            if(front.Equals("Home",StringComparison.InvariantCultureIgnoreCase) && inputs.Count()==1) {
                DisplayManager.View=View.Main;
                DisplayManager.DrawMainPage();
            }
            else if(front.Equals("AddPackage", StringComparison.InvariantCultureIgnoreCase) && inputs.Count()==1) AddPackage();
            else if(front.Equals("ViewPackages", StringComparison.InvariantCultureIgnoreCase) && inputs.Count()==1) ViewPackages();
            else if(front.Equals("UpdatePackageStatus", StringComparison.InvariantCultureIgnoreCase) && inputs.Count()==3) UpdatePackageStatus(inputs);
            else if(front.Equals("ViewAllResidents", StringComparison.InvariantCultureIgnoreCase) && inputs.Count()==1) ViewAllResidents();
            else if(front.Equals("ViewResident", StringComparison.InvariantCultureIgnoreCase) && inputs.Count()==2) ViewResident(inputs);
            else if(front.Equals("AssignPackage", StringComparison.InvariantCultureIgnoreCase) && inputs.Count()==3) AssignPackage(inputs);
            else if(front.Equals("Quit", StringComparison.InvariantCultureIgnoreCase) && inputs.Count()==1) {
                DisplayManager.DrawQuit();
                listening=false;
                return;
            }
            else {
                DisplayManager.DrawMainPage(error:true);
            }
        }
    }

    static void AddPackage(){
        var _newPackage = new Package{DeliveryDate=DateTime.Now, Status=Status.AwaitingStatus};
        DisplayManager.View=View.AddPackage;
        DisplayManager.DrawMainPage(newPackage:_newPackage);

        bool done = false;
        while(!done){
            var input = Console.ReadLine();
            if(input=="1"){
                DisplayManager.DrawMainPage(newPackage:_newPackage, packageView:PackageView.TrackingNumber);
                var trackingNumber = Console.ReadLine();
                if(String.IsNullOrEmpty(trackingNumber)) DisplayManager.DrawMainPage(error:true, errorMsg:"Invalid Tracking Number Input", newPackage:_newPackage);
                else {
                    _newPackage.TrackingNumber=trackingNumber;
                    DisplayManager.DrawMainPage(newPackage:_newPackage);
                }
            }
            else if(input=="2"){
                DisplayManager.DrawMainPage(newPackage:_newPackage, packageView:PackageView.Name);
                var name = Console.ReadLine();
                if(String.IsNullOrEmpty(name)) DisplayManager.DrawMainPage(error:true, errorMsg:"Invalid name input", newPackage:_newPackage);
                else {
                    _newPackage.FullName=name;
                    DisplayManager.DrawMainPage(newPackage:_newPackage);
                }
            }
            else if(input=="3"){
                DisplayManager.DrawMainPage(newPackage:_newPackage, packageView:PackageView.Address);
                var address = Console.ReadLine();
                if(String.IsNullOrEmpty(address)) DisplayManager.DrawMainPage(error:true, errorMsg:"Invalid address input", newPackage:_newPackage);
                else {
                    _newPackage.FullAddress=address;
                    DisplayManager.DrawMainPage(newPackage:_newPackage);
                }
            }
            else if(input=="4"){ //Service
                DisplayManager.DrawMainPage(newPackage:_newPackage, packageView:PackageView.Service);
                Service service;
                if(Enum.TryParse<Service>(Console.ReadLine(), out service)){
                    _newPackage.Service=service;
                    DisplayManager.DrawMainPage(newPackage:_newPackage);
                }
                else DisplayManager.DrawMainPage(error:true, errorMsg:"Invalid Service Input", newPackage:_newPackage);
            }
            else if(input=="5"){ //Complete
                if(String.IsNullOrEmpty(_newPackage.FullName) || String.IsNullOrEmpty(_newPackage.FullAddress) || String.IsNullOrEmpty(_newPackage.TrackingNumber) || _newPackage.Service==Service.Unassigned) DisplayManager.DrawMainPage(error:true, errorMsg:"Package information has not been completed", newPackage:_newPackage);
                else {
                    DataMain.CreatePackage(_newPackage);
                    _newPackage = new();
                    DisplayManager.View=View.Main;
                    done=true;
                    DisplayManager.DrawMainPage();
                    return;
                }
            }
            else if(input=="6"){//Cancel
                _newPackage = new();
                DisplayManager.View=View.Main;
                done=true;
                DisplayManager.DrawMainPage();
                return;
            }
            else DisplayManager.DrawMainPage(error:true, newPackage:_newPackage);
        }
    }

    static void ViewPackages(){
        DisplayManager.View=View.Packages;
        DisplayManager.DrawMainPage(Packages:DataMain.GetAllPackages());
    }

    static void UpdatePackageStatus(string[] inputs){
        int packageID;
        Status status;
        bool success;

        success=Int32.TryParse(inputs[1], out packageID);
        if(!success) {
            DisplayManager.DrawMainPage(error:true);
        }
        success=Enum.TryParse<Status>(inputs[2], out status);
        if(!success) {
            DisplayManager.DrawMainPage(error:true);
        }

        DataMain.UpdatePackageStatus(packageID, status);
        DisplayManager.DrawMainPage();
    }

    static void ViewAllResidents(){
        DisplayManager.View=View.AllResidents;
        DisplayManager.DrawMainPage(residents:DataMain.GetAllResidents());
    }

    static void ViewResident(string[] inputs){
        int residentID;
        if(!Int32.TryParse(inputs[1], out residentID)) DisplayManager.DrawMainPage(error:true);

        DisplayManager.View=View.Resident;
        DisplayManager.DrawMainPage(resident:DataMain.GetResident(residentID));
    }

    static void AssignPackage(string[] inputs){
        int packageID;
        int residentID;

        if(!Int32.TryParse(inputs[1], out packageID)){
            DisplayManager.DrawMainPage(error:true);
            return;
        }
        if(!Int32.TryParse(inputs[2], out residentID)){
            DisplayManager.DrawMainPage(error:true);
            return;
        }

        var success=DataMain.AssignPackage(packageID, residentID);
        if(!success.Item1) DisplayManager.DrawMainPage(error:true, errorMsg:success.Item2 ?? "");
        else DisplayManager.DrawMainPage();
    }
}
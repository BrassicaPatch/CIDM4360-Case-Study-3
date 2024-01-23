using Mailroom.DataTier.Models;
using Microsoft.EntityFrameworkCore;

namespace Mailroom.DataTier;

public static class DataMain {

    public static void InitializeDataTier() {
        using(var db = new AppDbContext()){
            if(db.Staff.Count()==0) Seeder.SeedData();
        }
    }

    public static bool LogIn(string username, string password) {
        if(String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password)) return false;
        using(var db = new AppDbContext()){
            var user = db.Staff.FirstOrDefault(u=>u.UserName==username && u.Password==password);
            if(user==default) return false;
            else return true;
        }
    }

    public static void CreatePackage(Package package){
        using(var db = new AppDbContext()){
            db.Packages.Add(package);
            db.SaveChanges();
        }

    }

    public static List<Package>? GetAllPackages(){
        using(var db = new AppDbContext()){
            return db.Packages.ToList();
        }
    }

    public static void UpdatePackageStatus(int packageID, Status status){
        using(var db = new AppDbContext()){
            var package=db.Packages.FirstOrDefault(p=>p.ID==packageID);
            if(package==default) return;
            else package.Status=status;
            db.SaveChanges();
        }
    }

    public static List<Resident>? GetAllResidents(){
        using(var db = new AppDbContext()){
            return db.Residents.Include(r=>r.Packages).ToList();
        }
    }

    public static Resident? GetResident(int id){
        using(var db = new AppDbContext()){
            return db.Residents.Include(r=>r.Packages).First(r=>r.ID==id);
        }
    }

    public static (bool, string?) AssignPackage(int packageID, int residentID){
        using(var db = new AppDbContext()){
            var package=db.Packages.Find(packageID);
            var resident=db.Residents.Find(residentID);
            if(package==null) return (false, "Invalid Package ID");
            else if(resident==null) return (false, "Invalid Resident ID");
            else {
                package.Status=Status.Pending;
                resident.Packages.Add(package);
                db.SaveChanges();
                EmailService.SendPackageNotificationEmail(resident, package);
                return(true, null);
            }

        }
    }
} 
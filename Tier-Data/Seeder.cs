using Mailroom.DataTier.Models;
using Microsoft.EntityFrameworkCore;

namespace Mailroom.DataTier;

public static class Seeder {
    public static void SeedData(){
        AddStaff();
        AddResidents();
    }

    static void AddStaff(){
        using(var db = new AppDbContext()){
            db.Staff.AddRange(new List<StaffUser> {
                new StaffUser{UserName="alice", Password="alice123"},
                new StaffUser{UserName="bob", Password="bob123"}
            });
            db.SaveChanges();
        }
    }

    static void AddResidents(){
        using(var db = new AppDbContext()){
            db.Database.ExecuteSqlRaw(@"INSERT INTO Residents VALUES
                (1,'Kittie Mousdall','crpierce3@buffs.wtamu.edu',101),
                (2,'Claudette Rait','crpierce3@buffs.wtamu.edu',102),
                (3,'Eliza Himsworth','crpierce3@buffs.wtamu.edu',103),
                (4,'Emmit Gann','crpierce3@buffs.wtamu.edu',104),
                (5,'Aurlie Pedycan','crpierce3@buffs.wtamu.edu',105),
                (6,'Keriann Kettlesting','crpierce3@buffs.wtamu.edu',106),
                (7,'Fiorenze Iacovuzzi','crpierce3@buffs.wtamu.edu',107),
                (8,'Darlene Gravie','crpierce3@buffs.wtamu.edu',108),
                (9,'Tomasine Challener','crpierce3@buffs.wtamu.edu',109),
                (10,'Dotti Marple','crpierce3@buffs.wtamu.edu',110),
                (11,'Gabriel Tofanelli','crpierce3@buffs.wtamu.edu',201),
                (12,'Aldo Welldrake','crpierce3@buffs.wtamu.edu',202),
                (13,'Ezmeralda Laydon','crpierce3@buffs.wtamu.edu',203),
                (14,'Kale Lendrem','crpierce3@buffs.wtamu.edu',204),
                (15,'Lenard Cubbit','crpierce3@buffs.wtamu.edu',205),
                (16,'Dedie Caddies','crpierce3@buffs.wtamu.edu',206),
                (17,'Nancy Janosevic','crpierce3@buffs.wtamu.edu',207),
                (18,'Layne Whiterod','crpierce3@buffs.wtamu.edu',208),
                (19,'Averell Labusch','crpierce3@buffs.wtamu.edu',209),
                (20,'Gordan Raddon','crpierce3@buffs.wtamu.edu',210),
                (21,'Deloria Johnes','crpierce3@buffs.wtamu.edu',301),
                (22,'Emmett MacIllrick','crpierce3@buffs.wtamu.edu',302),
                (23,'Sanderson Simoncelli','crpierce3@buffs.wtamu.edu',303),
                (24,'Had Hapke','crpierce3@buffs.wtamu.edu',304),
                (25,'Bellina Rodenburgh','crpierce3@buffs.wtamu.edu',305),
                (26,'Portie Searle','crpierce3@buffs.wtamu.edu',306),
                (27,'Ellsworth Richichi','crpierce3@buffs.wtamu.edu',307),
                (28,'Orlando Mattholie','crpierce3@buffs.wtamu.edu',308),
                (29,'Noby Phelp','crpierce3@buffs.wtamu.edu',309),
                (30,'Wilow Caush','crpierce3@buffs.wtamu.edu',310),
                (31,'Hesther Wincom','crpierce3@buffs.wtamu.edu',401),
                (32,'Ferdie Jzhakov','crpierce3@buffs.wtamu.edu',402),
                (33,'Cornelia Burlingham','crpierce3@buffs.wtamu.edu',403),
                (34,'Rochella Childers','crpierce3@buffs.wtamu.edu',404),
                (35,'Jennie Christensen','crpierce3@buffs.wtamu.edu',405),
                (36,'Kalie Cropper','crpierce3@buffs.wtamu.edu',406),
                (37,'Corinne Garrison','crpierce3@buffs.wtamu.edu',407),
                (38,'Maybelle Pigne','crpierce3@buffs.wtamu.edu',408),
                (39,'Wald Kuddyhy','crpierce3@buffs.wtamu.edu',409),
                (40,'Blancha Ambrosoni','crpierce3@buffs.wtamu.edu',410),
                (41,'Gussy Moiser','crpierce3@buffs.wtamu.edu',501),
                (42,'Margette Symcock','crpierce3@buffs.wtamu.edu',502),
                (43,'Cad Stearnes','crpierce3@buffs.wtamu.edu',503),
                (44,'Juliann Sumner','crpierce3@buffs.wtamu.edu',504),
                (45,'Esdras Bresland','crpierce3@buffs.wtamu.edu',505),
                (46,'Alisha Laspee','crpierce3@buffs.wtamu.edu',506),
                (47,'Yvon Jirusek','crpierce3@buffs.wtamu.edu',507),
                (48,'Parrnell Halbeard','crpierce3@buffs.wtamu.edu',508),
                (49,'Korrie Milesap','crpierce3@buffs.wtamu.edu',509),
                (50,'Vivyan Petzold','crpierce3@buffs.wtamu.edu',510),
                (51,'Angie Darben','crpierce3@buffs.wtamu.edu',101),
                (52,'Eachelle Texton','crpierce3@buffs.wtamu.edu',102),
                (53,'Lion Imlaw','crpierce3@buffs.wtamu.edu',103),
                (54,'Delmore Cowhig','crpierce3@buffs.wtamu.edu',104),
                (55,'Shaine Van Kruis','crpierce3@buffs.wtamu.edu',105),
                (56,'Yehudi Jones','crpierce3@buffs.wtamu.edu',106),
                (57,'Hamlen Gerrad','crpierce3@buffs.wtamu.edu',107),
                (58,'Elisabetta Francescozzi','crpierce3@buffs.wtamu.edu',108),
                (59,'Gusti Chinn','crpierce3@buffs.wtamu.edu',109),
                (60,'Candace Hurlston','crpierce3@buffs.wtamu.edu',110),
                (61,'Odey Butter','crpierce3@buffs.wtamu.edu',201),
                (62,'Viva Bolletti','crpierce3@buffs.wtamu.edu',202),
                (63,'Tallie Jubert','crpierce3@buffs.wtamu.edu',203),
                (64,'Mary Vearnals','crpierce3@buffs.wtamu.edu',204),
                (65,'Lona Dunbavin','crpierce3@buffs.wtamu.edu',205),
                (66,'Osmond Bamlett','crpierce3@buffs.wtamu.edu',206),
                (67,'Nomi Sollom','crpierce3@buffs.wtamu.edu',207),
                (68,'Hildy Campana','crpierce3@buffs.wtamu.edu',208),
                (69,'Emmanuel Getcliffe','crpierce3@buffs.wtamu.edu',209),
                (70,'Danette Danieli','crpierce3@buffs.wtamu.edu',210),
                (71,'Jan Witt','crpierce3@buffs.wtamu.edu',301),
                (72,'Woodie Kertess','crpierce3@buffs.wtamu.edu',302),
                (73,'Corine Cleevely','crpierce3@buffs.wtamu.edu',303),
                (74,'Inez Mew','crpierce3@buffs.wtamu.edu',304),
                (75,'Kathie Odd','crpierce3@buffs.wtamu.edu',305),
                (76,'Mitch Friedlos','crpierce3@buffs.wtamu.edu',306),
                (77,'Bambi Gostick','crpierce3@buffs.wtamu.edu',307),
                (78,'Mellicent Roiz','crpierce3@buffs.wtamu.edu',308),
                (79,'Sukey Avon','crpierce3@buffs.wtamu.edu',309),
                (80,'Janina Kernan','crpierce3@buffs.wtamu.edu',310),
                (81,'Jaynell Pitfield','crpierce3@buffs.wtamu.edu',401),
                (82,'Ricki Hoppner','crpierce3@buffs.wtamu.edu',402),
                (83,'Rinaldo Stable','crpierce3@buffs.wtamu.edu',403),
                (84,'Tessy Tabour','crpierce3@buffs.wtamu.edu',404),
                (85,'Helen Ferencz','crpierce3@buffs.wtamu.edu',405),
                (86,'Korney Shakelade','crpierce3@buffs.wtamu.edu',406),
                (87,'Tulley Reiner','crpierce3@buffs.wtamu.edu',407),
                (88,'Myrle Mersh','crpierce3@buffs.wtamu.edu',408),
                (89,'Carina Nelthorp','crpierce3@buffs.wtamu.edu',409),
                (90,'Monte Trahmel','crpierce3@buffs.wtamu.edu',410),
                (91,'Nate Zavattero','crpierce3@buffs.wtamu.edu',501),
                (92,'Neddy Bucky','crpierce3@buffs.wtamu.edu',502),
                (93,'Allissa Collyns','crpierce3@buffs.wtamu.edu',503),
                (94,'Brianna Ruberry','crpierce3@buffs.wtamu.edu',504),
                (95,'Roxane Wellen','crpierce3@buffs.wtamu.edu',505),
                (96,'Ashbey Keddy','crpierce3@buffs.wtamu.edu',506),
                (97,'Elvin Mico','crpierce3@buffs.wtamu.edu',507),
                (98,'Nicolas Hanratty','crpierce3@buffs.wtamu.edu',508),
                (99,'Gary Jochens','crpierce3@buffs.wtamu.edu',509),
                (100,'Alexina Tarbard','crpierce3@buffs.wtamu.edu',510);"
            );
            //db.SaveChanges();
        }
    }
}
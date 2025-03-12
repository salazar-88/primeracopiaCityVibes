namespace CityVibes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusToUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id_Category = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id_Category);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id_Place = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        Id_Category = c.Int(),
                        Address = c.String(maxLength: 255),
                        City = c.String(maxLength: 100),
                        Latitude = c.Decimal(precision: 9, scale: 6),
                        Longitude = c.Decimal(precision: 9, scale: 6),
                        Rating = c.Decimal(precision: 3, scale: 2),
                        Id_Price_Range = c.Int(),
                        Creation_Date = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id_Place)
                .ForeignKey("dbo.Categories", t => t.Id_Category)
                .ForeignKey("dbo.Price_Ranges", t => t.Id_Price_Range)
                .Index(t => t.Id_Category)
                .Index(t => t.Id_Price_Range);
            
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        Id_Favorite = c.Int(nullable: false, identity: true),
                        Id_User = c.Int(),
                        Id_Place = c.Int(),
                        Creation_Date = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id_Favorite)
                .ForeignKey("dbo.Places", t => t.Id_Place)
                .ForeignKey("dbo.Users", t => t.Id_User)
                .Index(t => t.Id_User)
                .Index(t => t.Id_Place);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id_User = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 255),
                        Password = c.String(nullable: false, maxLength: 255),
                        Creation_Date = c.DateTime(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id_User);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id_Review = c.Int(nullable: false, identity: true),
                        Id_User = c.Int(),
                        Id_Place = c.Int(),
                        Rating = c.Int(),
                        Comment = c.String(),
                        Creation_Date = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id_Review)
                .ForeignKey("dbo.Places", t => t.Id_Place)
                .ForeignKey("dbo.Users", t => t.Id_User)
                .Index(t => t.Id_User)
                .Index(t => t.Id_Place);
            
            CreateTable(
                "dbo.Opening_Schedule",
                c => new
                    {
                        Id_Schedule = c.Int(nullable: false, identity: true),
                        Id_Place = c.Int(),
                        Day = c.String(nullable: false, maxLength: 50),
                        Opening_Time = c.Time(nullable: false, precision: 7),
                        Closing_Time = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id_Schedule)
                .ForeignKey("dbo.Places", t => t.Id_Place)
                .Index(t => t.Id_Place);
            
            CreateTable(
                "dbo.Price_Ranges",
                c => new
                    {
                        Id_Price_Range = c.Int(nullable: false, identity: true),
                        Range_Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id_Price_Range);
            
            CreateTable(
                "dbo.User_Categories",
                c => new
                    {
                        Id_Category = c.Int(nullable: false),
                        Id_User = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id_Category, t.Id_User })
                .ForeignKey("dbo.Categories", t => t.Id_Category, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Id_User, cascadeDelete: true)
                .Index(t => t.Id_Category)
                .Index(t => t.Id_User);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_Categories", "Id_User", "dbo.Users");
            DropForeignKey("dbo.User_Categories", "Id_Category", "dbo.Categories");
            DropForeignKey("dbo.Places", "Id_Price_Range", "dbo.Price_Ranges");
            DropForeignKey("dbo.Opening_Schedule", "Id_Place", "dbo.Places");
            DropForeignKey("dbo.Reviews", "Id_User", "dbo.Users");
            DropForeignKey("dbo.Reviews", "Id_Place", "dbo.Places");
            DropForeignKey("dbo.Favorites", "Id_User", "dbo.Users");
            DropForeignKey("dbo.Favorites", "Id_Place", "dbo.Places");
            DropForeignKey("dbo.Places", "Id_Category", "dbo.Categories");
            DropIndex("dbo.User_Categories", new[] { "Id_User" });
            DropIndex("dbo.User_Categories", new[] { "Id_Category" });
            DropIndex("dbo.Opening_Schedule", new[] { "Id_Place" });
            DropIndex("dbo.Reviews", new[] { "Id_Place" });
            DropIndex("dbo.Reviews", new[] { "Id_User" });
            DropIndex("dbo.Favorites", new[] { "Id_Place" });
            DropIndex("dbo.Favorites", new[] { "Id_User" });
            DropIndex("dbo.Places", new[] { "Id_Price_Range" });
            DropIndex("dbo.Places", new[] { "Id_Category" });
            DropTable("dbo.User_Categories");
            DropTable("dbo.Price_Ranges");
            DropTable("dbo.Opening_Schedule");
            DropTable("dbo.Reviews");
            DropTable("dbo.Users");
            DropTable("dbo.Favorites");
            DropTable("dbo.Places");
            DropTable("dbo.Categories");
        }
    }
}

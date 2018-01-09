namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserDays : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserDays",
                c => new
                    {
                        UserDayId = c.Int(nullable: false, identity: true),
                        Day_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserDayId)
                .ForeignKey("dbo.Days", t => t.Day_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Day_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDays", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserDays", "Day_Id", "dbo.Days");
            DropIndex("dbo.UserDays", new[] { "User_Id" });
            DropIndex("dbo.UserDays", new[] { "Day_Id" });
            DropTable("dbo.UserDays");
        }
    }
}

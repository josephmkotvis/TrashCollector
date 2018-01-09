namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initmigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Days", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Days", new[] { "User_Id" });
            DropColumn("dbo.Days", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Days", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Days", "User_Id");
            AddForeignKey("dbo.Days", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}

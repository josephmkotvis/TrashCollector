namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Movedpickupvariablestouserdaytable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDays", "HasPickUpRequested", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserDays", "WasPickedUp", c => c.Boolean(nullable: false));
            DropColumn("dbo.Days", "HasPickUp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Days", "HasPickUp", c => c.Boolean(nullable: false));
            DropColumn("dbo.UserDays", "WasPickedUp");
            DropColumn("dbo.UserDays", "HasPickUpRequested");
        }
    }
}

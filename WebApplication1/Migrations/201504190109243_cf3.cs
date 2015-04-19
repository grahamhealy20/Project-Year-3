namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cf3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TrackingStates", new[] { "Session_Id" });
            AlterColumn("dbo.TrackingStates", "Session_id", c => c.Int(nullable: false));
            CreateIndex("dbo.TrackingStates", "Session_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TrackingStates", new[] { "Session_Id" });
            AlterColumn("dbo.TrackingStates", "Session_id", c => c.Int());
            CreateIndex("dbo.TrackingStates", "Session_Id");
        }
    }
}

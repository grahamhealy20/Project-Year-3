namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TrackingStates", "Session_Id", "dbo.Sessions");
            DropIndex("dbo.TrackingStates", new[] { "Session_Id" });
            DropPrimaryKey("dbo.TrackingStates");
            AddColumn("dbo.Sessions", "Session_Id", c => c.Int(nullable: false));
            AddColumn("dbo.TrackingStates", "TrackingId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.TrackingStates", "stateType", c => c.String());
            AlterColumn("dbo.TrackingStates", "Session_Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.TrackingStates", "TrackingId");
            DropColumn("dbo.TrackingStates", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TrackingStates", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.TrackingStates");
            AlterColumn("dbo.TrackingStates", "Session_Id", c => c.Int());
            DropColumn("dbo.TrackingStates", "stateType");
            DropColumn("dbo.TrackingStates", "TrackingId");
            DropColumn("dbo.Sessions", "Session_Id");
            AddPrimaryKey("dbo.TrackingStates", "Id");
            CreateIndex("dbo.TrackingStates", "Session_Id");
            AddForeignKey("dbo.TrackingStates", "Session_Id", "dbo.Sessions", "Id");
        }
    }
}

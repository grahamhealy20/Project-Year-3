namespace TrackingRESTService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TrackingStates");
            DropColumn("dbo.TrackingStates", "Id");
            AddColumn("dbo.TrackingStates", "TrackingId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.TrackingStates", "TrackingId");         
        }
        
        public override void Down()
        {
            AddColumn("dbo.TrackingStates", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.TrackingStates");
            DropColumn("dbo.TrackingStates", "TrackingId");
            AddPrimaryKey("dbo.TrackingStates", "Id");
        }
    }
}

namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TrackingStates");
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        time = c.DateTime(nullable: false),
                        place = c.String(),
                        temp = c.String(),
                        noAlerts = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            //AddColumn("dbo.TrackingStates", "TrackingId", c => c.Int(nullable: false, identity: true));
            //AddColumn("dbo.TrackingStates", "Session_Id", c => c.Int());
            //AddColumn("dbo.TrackingStates", "stateType", c => c.String());
           // AddColumn("dbo.TrackingStates", "Session_Id1", c => c.Int());
            AddPrimaryKey("dbo.TrackingStates", "TrackingId");
            //CreateIndex("dbo.TrackingStates", "Session_Id1");
            //AddForeignKey("dbo.TrackingStates", "Session_Id", "dbo.Sessions", "Id");
           // DropColumn("dbo.TrackingStates", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TrackingStates", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.TrackingStates", "Session_Id1", "dbo.Sessions");
            DropIndex("dbo.TrackingStates", new[] { "Session_Id1" });
            DropPrimaryKey("dbo.TrackingStates");
            DropColumn("dbo.TrackingStates", "Session_Id1");
           // DropColumn("dbo.TrackingStates", "stateType");
            //DropColumn("dbo.TrackingStates", "Session_Id");
            //DropColumn("dbo.TrackingStates", "TrackingId");
            DropTable("dbo.Sessions");
            AddPrimaryKey("dbo.TrackingStates", "Id");
        }
    }
}

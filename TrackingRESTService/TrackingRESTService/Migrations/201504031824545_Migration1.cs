namespace TrackingRESTService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
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
            
            CreateTable(
                "dbo.TrackingStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        time = c.String(),
                        place = c.String(),
                        temp = c.String(),
                        noAlerts = c.Int(nullable: false),
                        Session_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sessions", t => t.Session_Id)
                .Index(t => t.Session_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrackingStates", "Session_Id", "dbo.Sessions");
            DropIndex("dbo.TrackingStates", new[] { "Session_Id" });
            DropTable("dbo.TrackingStates");
            DropTable("dbo.Sessions");
        }
    }
}

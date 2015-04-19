namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SessionCRUD : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Sessions",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            UserId = c.String(),
            //            time = c.DateTime(nullable: false),
            //            place = c.String(),
            //            temp = c.String(),
            //            noAlerts = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //AddColumn("dbo.TrackingStates", "Session_Id", c => c.Int());
            //CreateIndex("dbo.TrackingStates", "Session_Id");
            //AddForeignKey("dbo.TrackingStates", "Session_Id", "dbo.Sessions", "Id");
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.TrackingStates", "Session_Id", "dbo.Sessions");
            //DropIndex("dbo.TrackingStates", new[] { "Session_Id" });
            //DropColumn("dbo.TrackingStates", "Session_Id");
            //DropTable("dbo.Sessions");
        }
    }
}

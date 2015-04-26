namespace TrackingRESTService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cascade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TrackingStates", "Session_Id", "dbo.Sessions");
            AddForeignKey("dbo.TrackingStates", "Session_Id", "dbo.Sessions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrackingStates", "Session_Id", "dbo.Sessions");
            AddForeignKey("dbo.TrackingStates", "Session_Id", "dbo.Sessions", "Id");
        }
    }
}

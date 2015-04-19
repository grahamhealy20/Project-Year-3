namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cf1 : DbMigration
    {
        public override void Up()
        {
            //DropIndex("dbo.TrackingStates", new[] { "Session_Id" });
            //DropColumn("dbo.TrackingStates", "Id");
            //RenameColumn(table: "dbo.TrackingStates", name: "Session_Id", newName: "Id");
            //DropPrimaryKey("dbo.TrackingStates");
            //AlterColumn("dbo.TrackingStates", "Id", c => c.Int(nullable: false));
            //AlterColumn("dbo.TrackingStates", "Id", c => c.Int(nullable: false));
            //AddPrimaryKey("dbo.TrackingStates", "Id");
            //CreateIndex("dbo.TrackingStates", "Id");
            //DropColumn("dbo.Sessions", "Session_Id");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Sessions", "Session_Id", c => c.Int(nullable: false));
            //DropIndex("dbo.TrackingStates", new[] { "Id" });
            //DropPrimaryKey("dbo.TrackingStates");
            //AlterColumn("dbo.TrackingStates", "Id", c => c.Int());
            //AlterColumn("dbo.TrackingStates", "Id", c => c.Int(nullable: false, identity: true));
            //AddPrimaryKey("dbo.TrackingStates", "Id");
            //RenameColumn(table: "dbo.TrackingStates", name: "Id", newName: "Session_Id");
            //AddColumn("dbo.TrackingStates", "Id", c => c.Int(nullable: false, identity: true));
            //CreateIndex("dbo.TrackingStates", "Session_Id");
        }
    }
}

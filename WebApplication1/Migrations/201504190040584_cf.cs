namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "Session_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sessions", "Session_Id");
        }
    }
}

namespace qMine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refreshrate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServerCredentials", "RefreshRate", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServerCredentials", "RefreshRate");
        }
    }
}

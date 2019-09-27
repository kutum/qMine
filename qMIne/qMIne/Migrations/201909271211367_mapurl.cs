namespace qMine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mapurl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServerCredentials", "MapUrl", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServerCredentials", "MapUrl");
        }
    }
}

namespace qMIne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPortToServerCredential : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServerCredentials", "RconPort", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServerCredentials", "RconPort");
        }
    }
}

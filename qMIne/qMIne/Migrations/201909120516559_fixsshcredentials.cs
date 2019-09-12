namespace qMine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixsshcredentials : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServerCredentials", "SSHMinecraftServiceName", c => c.String(unicode: false));
            DropColumn("dbo.ServerCredentials", "SSHStartServer");
            DropColumn("dbo.ServerCredentials", "SSHStopServer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServerCredentials", "SSHStopServer", c => c.String(unicode: false));
            AddColumn("dbo.ServerCredentials", "SSHStartServer", c => c.String(unicode: false));
            DropColumn("dbo.ServerCredentials", "SSHMinecraftServiceName");
        }
    }
}

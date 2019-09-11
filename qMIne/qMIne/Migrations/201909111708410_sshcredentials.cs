namespace qMine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sshcredentials : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServerCredentials", "SSHLogin", c => c.String(unicode: false));
            AddColumn("dbo.ServerCredentials", "SSHPassword", c => c.String(unicode: false));
            AddColumn("dbo.ServerCredentials", "SSHPort", c => c.Int(nullable: false));
            AddColumn("dbo.ServerCredentials", "SSHStartServer", c => c.String(unicode: false));
            AddColumn("dbo.ServerCredentials", "SSHStopServer", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServerCredentials", "SSHStopServer");
            DropColumn("dbo.ServerCredentials", "SSHStartServer");
            DropColumn("dbo.ServerCredentials", "SSHPort");
            DropColumn("dbo.ServerCredentials", "SSHPassword");
            DropColumn("dbo.ServerCredentials", "SSHLogin");
        }
    }
}

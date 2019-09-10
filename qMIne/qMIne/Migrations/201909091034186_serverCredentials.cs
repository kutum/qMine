namespace qMine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class serverCredentials : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServerCredentials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        IP = c.String(unicode: false),
                        Port = c.Int(nullable: false),
                        Password = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ServerCredentials");
        }
    }
}

namespace myTwitterProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sam : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.status",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        statuses = c.String(),
                        like = c.String(),
                        time = c.String(),
                        date = c.String(),
                    })
                .PrimaryKey(t => t.StatusId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.status");
        }
    }
}

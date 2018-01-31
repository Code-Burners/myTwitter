namespace myTwitterProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sam : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.status", "time", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.status", "time");
        }
    }
}

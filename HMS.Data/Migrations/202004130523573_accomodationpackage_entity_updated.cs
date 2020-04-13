namespace HMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accomodationpackage_entity_updated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccomodationPackages", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccomodationPackages", "Description");
        }
    }
}

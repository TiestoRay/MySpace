namespace SEPInstance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpRoles", "Description", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpRoles", "Description");
        }
    }
}

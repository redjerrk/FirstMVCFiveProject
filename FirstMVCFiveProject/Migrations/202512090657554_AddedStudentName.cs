namespace FirstMVCFiveProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStudentName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Name");
        }
    }
}

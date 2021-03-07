namespace MyWebSite.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class dw : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Reklama", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Posts", "Reklama");
        }
    }
}
namespace MyWebSite.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class gg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "UserId", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Comments", "UserId");
        }
    }
}
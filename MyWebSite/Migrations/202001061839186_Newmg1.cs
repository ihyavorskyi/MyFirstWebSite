namespace MyWebSite.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Newmg1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "OwnerName", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Posts", "OwnerName");
        }
    }
}
using FluentMigrator;

namespace PrisonRP.Database.Migrations
{
    [Migration(20210108173800)]
    public class AddPlayerAccountTable : Migration
    {
        public override void Up()
        {
            Create.Table("PlayerAccount")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(25).Indexed()
                .WithColumn("Password").AsFixedLengthString(61);
        }

        public override void Down()
        {
            Delete.Table("PlayerAccount");
        }
    }
}

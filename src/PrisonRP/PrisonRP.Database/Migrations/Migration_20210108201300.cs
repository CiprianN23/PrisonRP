using FluentMigrator;

namespace PrisonRP.Database.Migrations
{
    [Migration(20210108201300)]
    public class Migration_20210108201300 : Migration
    {
        public override void Down()
        {
            Delete
                .Column("Gender")
                .Column("Age")
                .Column("LastSkin")
                .Column("ImprisonmentReason")
                .Column("Money")
                .Column("LastInterior")
                .Column("LastWorld")
                .Column("Health")
                .Column("Armour")
                .Column("FightStyle")
                .Column("LastPositionX")
                .Column("LastPositionY")
                .Column("LastPositionZ")
                .Column("LastPositionAngle")
                .FromTable("PlayerAccount");
        }

        public override void Up()
        {
            Alter.Table("PlayerAccount")
                .AddColumn("Gender").AsInt32().WithDefaultValue(1)
                .AddColumn("Age").AsInt32().WithDefaultValue(18)
                .AddColumn("LastSkin").AsInt32().WithDefaultValue(150)
                .AddColumn("ImprisonmentReason").AsString(21).WithDefaultValue("Nothing")
                .AddColumn("Money").AsInt32().WithDefaultValue(350)
                .AddColumn("LastInterior").AsInt32().WithDefaultValue(0)
                .AddColumn("LastWorld").AsInt32().WithDefaultValue(0)
                .AddColumn("Health").AsFloat().WithDefaultValue(100.0f)
                .AddColumn("Armour").AsFloat().WithDefaultValue(0.0f)
                .AddColumn("FightStyle").AsInt32().WithDefaultValue(4)
                .AddColumn("LastPositionX").AsFloat().WithDefaultValue(416.6970f)
                .AddColumn("LastPositionY").AsFloat().WithDefaultValue(1567.0526f)
                .AddColumn("LastPositionZ").AsFloat().WithDefaultValue(1001.0000f)
                .AddColumn("LastPositionAngle").AsFloat().WithDefaultValue(270.7746f);
        }
    }
}

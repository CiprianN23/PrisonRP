namespace PrisonRP.Database.Models
{
    public class PlayerAccount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Gender { get; set; }
        public int LastSkin { get; set; }
        public int Age { get; set; }
        public string ImprisonmentReason { get; set; }
        public int Money { get; set; }
        public int LastInterior { get; set; }
        public int LastWorld { get; set; }
        public float Health { get; set; }
        public float Armour { get; set; }
        public int FightStyle { get; set; }
        public float LastPositionX { get; set; }
        public float LastPositionY { get; set; }
        public float LastPositionZ { get; set; }
        public float LastPositionAngle { get; set; }
    }
}
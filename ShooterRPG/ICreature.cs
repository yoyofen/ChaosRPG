namespace ShooterRPG
{
    public interface ICreature
    {
        int Health { get; }

        int Armor { get; }

        int Damage { get; }

        int Speed { get; }

        int Range { get; }
    }
}
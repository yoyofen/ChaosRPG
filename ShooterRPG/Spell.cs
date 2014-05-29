namespace ShooterRPG
{
    public class Spell
    {
        public int Range { get; private set; }

        public int Damage { get; private set; }
        
        public int ManaCost { get; private set; }
        
        public Spell(int damage, int range, int manaCost)
        {
            this.Damage = damage;
            this.Range = range;
            this.ManaCost = manaCost;
        }
    }
}
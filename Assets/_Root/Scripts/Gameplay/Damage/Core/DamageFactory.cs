namespace JustMobyTest.Gameplay
{
    public class DamageFactory
    {
        public Damage Create(float value)
        {
            return new Damage(value);
        }
    }
}
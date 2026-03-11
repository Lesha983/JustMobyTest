namespace JustMobyTest.Gameplay
{
    public class Enemy : AEnemy
    {
        protected override void Die()
        {
            Despawn();
        }
    }
}
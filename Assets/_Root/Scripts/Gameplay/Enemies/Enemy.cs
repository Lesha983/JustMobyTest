namespace JustMobyTest.Gameplay
{
    using Wallet;
    using Zenject;

    public class Enemy : AEnemy
    {
        [Inject]
        private IWallet Wallet { get; set; }
        
        protected override void Die()
        {
            Wallet.AddPoints();
            Despawn();
        }
    }
}
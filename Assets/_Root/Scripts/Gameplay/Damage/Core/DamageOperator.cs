namespace JustMobyTest.Gameplay
{
    public class DamageOperator
    {
        public void Deal(Damage damage, IDamager damager, IDamageReceiver receiver)
        {
            receiver.Receive(damage);
        }
    }
}
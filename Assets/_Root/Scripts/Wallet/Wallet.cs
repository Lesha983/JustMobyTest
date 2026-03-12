namespace JustMobyTest.Wallet
{
    using System;
    using Save;
    using Zenject;

    //TMP
    public class Wallet : IWallet
    {
        [Inject]
        private SaveData SaveData { get; set; }
        
        public event Action OnUpdated;

        public int Points => SaveData.UpgradePoints;

        public void AddPoints(int value = 1)
        {
            SaveData.UpgradePoints += value;
            OnUpdated?.Invoke();
        }

        public void RemovePoints(int value = 1)
        {
            SaveData.UpgradePoints -= value;
            OnUpdated?.Invoke();
        }

        public bool HasEnoughPoints(int value = 1)
        {
            return SaveData.UpgradePoints >= value;
        }
    }
}
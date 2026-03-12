namespace JustMobyTest.Wallet
{
    using System;

    public interface IWallet
    {
        event Action OnUpdated;
        
        void AddPoints(int value = 1);
        void RemovePoints(int value = 1);
        bool HasEnoughPoints(int value = 1);
        int Points { get; }
    }
}
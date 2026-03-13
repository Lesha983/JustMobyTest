namespace JustMobyTest.Wallet
{
    public interface IBackPack
    {
        int Points { get; }
        void AddPoints(int value = 1);
        void RemovePoints(int value = 1);
        void Clear();
    }
}
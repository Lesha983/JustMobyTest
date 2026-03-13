namespace JustMobyTest.Wallet
{
    //TMP

    public class BackPack : IBackPack
    {
        private int _points;
        
        public int Points => _points;
        
        public void AddPoints(int value = 1)
        {
            _points += value;
        }

        public void RemovePoints(int value = 1)
        {
            _points -= value;
        }
        

        public void Clear()
        {
            _points = 0;
        }
    }
}
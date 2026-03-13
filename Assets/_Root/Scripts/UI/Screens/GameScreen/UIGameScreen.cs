namespace JustMobyTest.UI
{
    using Gameplay;
    using UnityEngine;
    using UnityEngine.UI;
    using Wallet;
    using Zenject;

    public class UIGameScreen : AUIScreen
    {
        [Inject]
        private IWallet Wallet { get; set; }
        [Inject]
        private IInstantiator Instantiator { get; set; }
        [Inject]
        private LevelService LevelService { get; set; }

        [SerializeField]
        private Button statsButton;
        [SerializeField]
        private UIUpgradeStatsPopup upgradeStatsPopupPrefab;
        
        private UIUpgradeStatsPopup _upgradeStatsPopup;

        private void OnEnable()
        {
            // Wallet.OnUpdated += CreateUpgradeStatsPopup;
            statsButton.onClick.AddListener(CreateUpgradeStatsPopup);
        }

        private void OnDisable()
        {
            // Wallet.OnUpdated -= CreateUpgradeStatsPopup;
            statsButton.onClick.RemoveListener(CreateUpgradeStatsPopup);
        }

        private void CreateUpgradeStatsPopup()
        {
            if(_upgradeStatsPopup != null)
                return;
            
            _upgradeStatsPopup = Instantiator.InstantiatePrefabForComponent<UIUpgradeStatsPopup>(upgradeStatsPopupPrefab, transform);
            _upgradeStatsPopup.Setup(DestroyUpgradeStatsPopup);
            LevelService.Pause();
        }

        private void DestroyUpgradeStatsPopup()
        {
            Destroy(_upgradeStatsPopup.gameObject);
            _upgradeStatsPopup = null;
            LevelService.Resume();
        }
    }
}
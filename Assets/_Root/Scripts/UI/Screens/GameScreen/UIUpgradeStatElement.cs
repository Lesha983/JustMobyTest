namespace JustMobyTest.UI
{
    using System;
    using Gameplay;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using Wallet;
    using Zenject;

    public class UIUpgradeStatElement : MonoBehaviour
    {
        [Inject]
        private PlayerStatsService StatsService { get; set; }
        [Inject]
        private IWallet Wallet { get; set; }
        
        [SerializeField]
        private GameObject frame;
        [SerializeField]
        private TMP_Text nameLabel;
        [SerializeField]
        private TMP_Text descriptionLabel;
        [SerializeField]
        private TMP_Text levelLabel;
        [SerializeField]
        private Button upgradeButton;
        
        private int _statLevel;
        private int _upgradesCount;
        private int _maxLevel;
        private const string _levelLabel = "Level: ";
        private Action _upgradeCallback;
        private IBackPack _backPack;

        public APlayerStats PlayerStat { get; private set; }

        public void Setup(IBackPack backPack, APlayerStats stat, Action upgradeCallback)
        {
            _backPack = backPack;
            PlayerStat = stat;
            frame.SetActive(false);
            nameLabel.text = stat.Name;
            descriptionLabel.text = stat.Description;
            _statLevel = StatsService.GetStatLevel(stat);
            _upgradesCount = 0;
            _maxLevel = stat.MaxLevel;
            levelLabel.text = $"{_levelLabel}{_statLevel + 1}";
            _upgradeCallback = upgradeCallback;
            UpdateView();
        }

        public void UpdateView()
        {
            var targetLevel = _statLevel + _upgradesCount;
            upgradeButton.interactable = targetLevel < _maxLevel && Wallet.Points - _backPack.Points > 0;
            levelLabel.text = $"{_levelLabel}{_statLevel + _upgradesCount + 1}";
        }

        public void Apply()
        {
            StatsService.UpgradeStats(PlayerStat, _upgradesCount);   
        }

        private void OnEnable()
        {
            upgradeButton.onClick.AddListener(Upgrade);
        }

        private void OnDisable()
        {
            upgradeButton.onClick.RemoveListener(Upgrade);
        }

        private void Upgrade()
        {
            _upgradesCount++;
            _backPack.AddPoints();
            // UpdateView();
            _upgradeCallback?.Invoke();
        }
    }
}
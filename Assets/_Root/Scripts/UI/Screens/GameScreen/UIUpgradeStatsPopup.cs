namespace JustMobyTest.UI
{
    using System;
    using System.Collections.Generic;
    using Gameplay;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using Wallet;
    using Zenject;
    using Random = UnityEngine.Random;

    public class UIUpgradeStatsPopup : MonoBehaviour
    {
        [Inject]
        private PlayerStatsCollection StatsCollection { get; set; }
        [Inject]
        private IInstantiator Instantiator { get; set; }
        [Inject]
        private PlayerStatsService StatsService { get; set; }
        [Inject]
        private IWallet Wallet { get; set; }
        
        [SerializeField]
        private TMP_Text pointsValueLabel;
        [SerializeField]
        private Button closeButton;
        [SerializeField]
        private Button upgradeButton;
        [SerializeField]
        private UIUpgradeStatElement elementPrefab;
        [SerializeField]
        private Transform elementsParent;

        private int _maxStats = 2;
        private List<UIUpgradeStatElement> _elements = new();
        private Action _closeCallback;
        private UIUpgradeStatElement _chosenElement;

        public void Setup(Action closeCallback)
        {
            _closeCallback = closeCallback;
            upgradeButton.gameObject.SetActive(false);
            upgradeButton.interactable = Wallet.HasEnoughPoints();
            pointsValueLabel.text = Wallet.Points.ToString();
        }

        private void OnEnable()
        {
            closeButton.onClick.AddListener(Close);
            upgradeButton.onClick.AddListener(Apply);
        }

        private void OnDisable()
        {
            closeButton.onClick.RemoveListener(Close);
            upgradeButton.onClick.RemoveListener(Apply);
        }

        private void Start()
        {
            CreateElements();
        }

        private void CreateElements()
        {
            var stats = new List<APlayerStats>();
            stats.AddRange(StatsCollection.Stats);
            Shuffle(stats);
            var elementsCount = Mathf.Min(stats.Count, _maxStats);
            for (var i = 0; i < elementsCount; i++)
            {
                var stat = stats[i];
                var element = Instantiator.InstantiatePrefabForComponent<UIUpgradeStatElement>(elementPrefab, elementsParent);
                element.Setup(stat, () => ChooseElement(element));
                _elements.Add(element);
            }
        }
        
        private void Shuffle<T>(List<T> list)
        {
            for (var i = list.Count - 1; i > 0; i--)
            {
                var j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        private void ChooseElement(UIUpgradeStatElement element)
        {
            foreach (var statElement in _elements)
            {
                if(statElement == element)
                    continue;
                statElement.UnChoose();
            }
            _chosenElement = element;
            upgradeButton.gameObject.SetActive(true);
        }

        private void Apply()
        {
            if(_chosenElement == null)
                return;
            
            StatsService.UpgradeStats(_chosenElement.PlayerStat);
            Wallet.RemovePoints();
            Close();
        }

        private void Close()
        {
            _closeCallback?.Invoke();
        }
    }
}
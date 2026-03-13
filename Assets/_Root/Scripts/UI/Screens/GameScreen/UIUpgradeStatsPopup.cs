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
        private Button applyButton;
        [SerializeField]
        private UIUpgradeStatElement elementPrefab;
        [SerializeField]
        private Transform elementsParent;

        // private int _maxStats = 3;
        private List<UIUpgradeStatElement> _elements = new();
        private Action _closeCallback;
        private IBackPack _backPack;

        public void Setup(Action closeCallback)
        {
            _closeCallback = closeCallback;
            applyButton.gameObject.SetActive(false);
            applyButton.interactable = Wallet.HasEnoughPoints();
            pointsValueLabel.text = Wallet.Points.ToString();
            _backPack = new BackPack();
        }

        private void OnEnable()
        {
            closeButton.onClick.AddListener(Close);
            applyButton.onClick.AddListener(Apply);
        }

        private void OnDisable()
        {
            closeButton.onClick.RemoveListener(Close);
            applyButton.onClick.RemoveListener(Apply);
        }

        private void Start()
        {
            CreateElements();
        }

        private void CreateElements()
        {
            var stats = StatsService.GetAvailableStats();
            if (stats.Count == 0)
            {
                Close();
                return;
            }
            
            // Shuffle(stats);
            // var elementsCount = Mathf.Min(stats.Count, _maxStats);
            for (var i = 0; i < stats.Count; i++)
            {
                var stat = stats[i];
                var element = Instantiator.InstantiatePrefabForComponent<UIUpgradeStatElement>(elementPrefab, elementsParent);
                element.Setup(_backPack, stat, UpdateView);
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

        private void UpdateView()
        {
            pointsValueLabel.text = (Wallet.Points - _backPack.Points).ToString();
            applyButton.gameObject.SetActive(true);
            foreach (var element in _elements)
            {
                element.UpdateView();
            }
        }

        private void Apply()
        {
            foreach (var element in _elements)
            {
                element.Apply();
            }
            
            Wallet.RemovePoints(_backPack.Points);
            _backPack.Clear();
            Close();
        }

        private void Close()
        {
            _closeCallback?.Invoke();
        }
    }
}
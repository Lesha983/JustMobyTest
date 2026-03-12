namespace JustMobyTest.UI
{
    using System;
    using Gameplay;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIUpgradeStatElement : MonoBehaviour
    {
        [SerializeField]
        private Button button;
        [SerializeField]
        private GameObject frame;
        [SerializeField]
        private TMP_Text nameLabel;
        [SerializeField]
        private TMP_Text descriptionLabel;
        
        private bool _isChosen;
        private Action _chooseCallback;

        public APlayerStats PlayerStat { get; private set; }

        public void Setup(APlayerStats stat, Action chooseCallback)
        {
            PlayerStat = stat;
            _chooseCallback = chooseCallback;
            frame.SetActive(false);
            nameLabel.text = stat.Name;
            descriptionLabel.text = stat.Description;
        }

        public void UnChoose()
        {
            frame.SetActive(false);
            _isChosen = false;
        }

        private void OnEnable()
        {
            button.onClick.AddListener(Choose);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(Choose);
        }
        
        private void Choose()
        {
            if (_isChosen) return;
            
            _chooseCallback?.Invoke();
            frame.SetActive(true);
            _isChosen = true;
        }
    }
}
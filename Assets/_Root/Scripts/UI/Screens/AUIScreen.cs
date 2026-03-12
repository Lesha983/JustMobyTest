namespace JustMobyTest.UI
{
    using System;
    using DG.Tweening;
    using UnityEngine;

    public abstract class AUIScreen : MonoBehaviour
    {
        public event Action OnShow;
        public event Action OnShown;
        public event Action OnHide;
        public event Action OnHidden;
        
        public bool IsShown { get; }
        
        private Sequence _sequence;

        public void Show()
        {
            OnShow?.Invoke();
            OnStartShow();
            _sequence?.Kill();
            _sequence = PlayShowAnimation().AppendCallback(() =>
            {
                OnShown?.Invoke();
            });
        }

        public void Close()
        {
            OnHide?.Invoke();
            OnStartClose();
            _sequence?.Kill();
            _sequence = PlayCloseAnimation().AppendCallback(() =>
            {
                OnHidden?.Invoke();
                Destroy(gameObject);
            });
        }

        protected virtual void OnStartShow()
        {
            
        }

        protected virtual Sequence PlayShowAnimation()
        {
            return DOTween.Sequence();
        }
        
        protected virtual void OnStartClose()
        {
            
        }
        
        protected virtual Sequence PlayCloseAnimation()
        {
            return DOTween.Sequence();
        }
    }
}